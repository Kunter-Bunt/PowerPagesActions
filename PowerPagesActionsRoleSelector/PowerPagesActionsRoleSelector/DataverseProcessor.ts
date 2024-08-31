import { IInputs, IOutputs } from "./generated/ManifestTypes";
import { Selection } from "office-ui-fabric-react/lib/DetailsList";
import { IRecord } from "./RoleSelectionGrid";

const AuthenticatedUsersKey = "AuthenticatedUsers";
export class DataverseProcessor {
    private context: ComponentFramework.Context<IInputs, ComponentFramework.IEventBag>;
    private webAPI: Xrm.WebApiOnline;
    private id: string;

    constructor(context: ComponentFramework.Context<IInputs>) {
        this.context = context;
        this.id = (Xrm.Utility.getPageContext().input as Xrm.EntityFormPageContext).entityId ?? "";
        this.webAPI = (context.webAPI as unknown as Xrm.WebApiOnline);
    }

    async GetAllRoles(): Promise<IRecord[]> {
        const filter = "mspp_anonymoususersrole eq false and mspp_authenticatedusersrole eq false";
        const select = "mspp_webroleid,mspp_name,_mspp_websiteid_value";
        const order = "_mspp_websiteid_value asc,mspp_name asc";
        const results = await this.webAPI.retrieveMultipleRecords("mspp_webrole", `?$filter=${filter}&$select=${select}&$orderby=${order}`);
        const selected = await this.GetCurrentlySelectedRoles();
        const mapped = results.entities.map((e) => {
            return {
                key: e.mspp_webroleid,
                name: e.mspp_name,
                site: e["_mspp_websiteid_value@OData.Community.Display.V1.FormattedValue"],
                selected: selected.some(r => r.powerpagecomponentid === e.mspp_webroleid)
            };
        });
        mapped.unshift({
            key: AuthenticatedUsersKey,
            name: "Authenticated Users",
            site: "All Sites",
            selected: this.context.parameters.isrestrictedtoauthenticated.raw ?? false
        });
        return mapped;
    }

    async GetCurrentlySelectedRoles(): Promise<IRole[]> { 
        const results = await this.webAPI.retrieveRecord("mwo_powerpagesactionconfiguration", this.id, "?$select=mwo_powerpagesactionconfigurationid&$expand=mwo_PPActionConfiguration_mspp_WebRole($select=powerpagecomponentid,name)")
        return results.mwo_PPActionConfiguration_mspp_WebRole as IRole[];
    }

    async ProcessSelectionChanged(selection: Selection): Promise<IOutputs> {
        const outputs: IOutputs = {
            isrestrictedtoauthenticated: false,
            isrestrictedtowebroles: false,
        };

        const selectedRoles = await this.GetCurrentlySelectedRoles();

        selection.getItems().forEach((item) => {
            const record = item as IRecord;
            const isSelected = selection.isKeySelected(record.key);
            console.log(`Item ${record.key}(${record.name}) is selected: ${isSelected}`);

            if (record.key === AuthenticatedUsersKey && isSelected) {
                outputs.isrestrictedtoauthenticated = true;
            }
            else if (isSelected) {
                outputs.isrestrictedtowebroles = true;
                this.AssociateRole(record.key, selectedRoles);
            }
            else {
                this.DisassociateRole(record.key, selectedRoles);
            }
        });

        return outputs;
    }

    DisassociateRole(key: string, selectedRoles: IRole[]) {
        if (!selectedRoles.some(r => r.powerpagecomponentid === key)) {
            return;
        }

        this.webAPI.execute(new DisassociateRequest(
            { id: this.id, entityType: "mwo_powerpagesactionconfiguration"},   
            key,
            "mwo_PPActionConfiguration_mspp_WebRole"
        ));
    }

    AssociateRole(key: string, selectedRoles: IRole[]) {
        if (selectedRoles.some(r => r.powerpagecomponentid === key)) {
            return;
        }

        this.webAPI.execute(new AssociateRequest(
            { id: this.id, entityType: "mwo_powerpagesactionconfiguration"},   
            [{ id: key, entityType: "mspp_webrole" }],
            "mwo_PPActionConfiguration_mspp_WebRole"
        ));
    }
} 

export class AssociateRequest {
    target: Xrm.LookupValue;
    relatedEntities: Xrm.LookupValue[];
    relationship: string;

    constructor(target: Xrm.LookupValue, relatedEntities: Xrm.LookupValue[], relationship: string) {
        this.target = target;
        this.relatedEntities = relatedEntities;
        this.relationship = relationship;
    }

    getMetadata = function() {
        return {
            boundParameter: null,
            parameterTypes: {},
            operationType: 2,
            operationName: "Associate"
        }
    }
}

export class DisassociateRequest {
    target: Xrm.LookupValue;
    relatedEntityId: string;
    relationship: string;

    constructor(target: Xrm.LookupValue, relatedEntityId: string, relationship: string) {
        this.target = target;
        this.relatedEntityId = relatedEntityId;
        this.relationship = relationship;
    }

    getMetadata = function() {
        return {
            boundParameter: null,
            parameterTypes: {},
            operationType: 2,
            operationName: "Disassociate"
        }
    }
}

export interface IRole {
    powerpagecomponentid: string;
    name: string;
}