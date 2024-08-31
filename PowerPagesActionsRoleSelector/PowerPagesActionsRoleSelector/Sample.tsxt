import * as React from "react";

import {

    DetailsList,

    DetailsListLayoutMode,

    Selection

} from "office-ui-fabric-react/lib/DetailsList";

import { MarqueeSelection } from "office-ui-fabric-react/lib/MarqueeSelection";

import { Fabric } from "office-ui-fabric-react/lib/Fabric";

export interface IDetailsListBasicExampleItem {

    key: number;

    name: string;

    value: number;

}

export interface IDetailsListBasicExampleState {

    items: any;

}

export class DetailsListGrid extends React.Component<
    any,
    IDetailsListBasicExampleState
> {

    private _selection: Selection;

    private _allItems: any = this.props.pageRows;

    private _columns: any = this.props.mappedcolumns;

    private _pcfContext = this.props.pcfContext;

    private _allSelectedCards: any = [];

    constructor(props: {}) {

        super(props);

        this._selection = new Selection({

            onSelectionChanged: () => {

                // @ts-ignore

                this.onRowSelection(this._selection._anchoredIndex);

            }

        });

        // Populate with items for demos.

        this.state = {

            items: this._allItems

        };

    }

    public render(): JSX.Element {

        const { items } = this.state;

        return (
            <Fabric>
                <MarqueeSelection selection= { this._selection } >
                    <DetailsList
                        items={ items }
                        columns = { this._columns }
                        setKey ="set"
                        layoutMode = { DetailsListLayoutMode.justified }
                        selection = { this._selection }
                        selectionPreservedOnEmptyClick = { true}
                        ariaLabelForSelectionColumn ="Toggle selection"
                        ariaLabelForSelectAllCheckbox ="Toggle selection for all items"
                        checkButtonAriaLabel ="Row checkbox"
                        onItemInvoked = { this._onItemInvoked }
                    />
                </MarqueeSelection>
            </Fabric>
        );
    }

    /**
    
    * Function to change the ribbon bar of CRM.
    
    */

    private onRowSelection = (rowIndex: number) => {

        let functionName: string = "onRowSelection";

        let selectedRowId: string;

        let selectedCardIndex: number;

        try {

            selectedRowId = this.props.pageRows[rowIndex].key;

            // check if selected row is alrady seelected

            selectedCardIndex = this._allSelectedCards.findIndex((element: any) => {

                return element == selectedRowId;

            });

            // if card is already clicked remove card id

            if (selectedCardIndex >= 0) {

                this._allSelectedCards.splice(selectedCardIndex, 1);

            } else {

                // store all selected card in array

                this._allSelectedCards.push(selectedRowId);

            }

            // update ribbon bar

            this._pcfContext.parameters.sampleDataSet.setSelectedRecordIds(

                this._allSelectedCards

            );

        } catch (error) {

            console.log(functionName + "" + error);

        }

    };

    /**
    
    * Call function to open Entity record
    
    */

    private _onItemInvoked = (item: IDetailsListBasicExampleItem): void => {

        // function to open entity record

        this.openEntityRecord(item.key);

    };

    /**
    
    * Open selected entity record
    
    * @param event
    
    */

    private openEntityRecord(recordID: any): void {

        let functionName: string = "onCardDoubleClick";

        try {

            if (recordID != null || recordID != undefined) {

                let entityreference = this._pcfContext.parameters.sampleDataSet.records[

                    recordID

                ].getNamedReference();

                let entityFormOptions = {

                    entityName: entityreference.LogicalName,

                    entityId: entityreference.id

                };

                /** Using navigation method */

                this._pcfContext.navigation

                    .openForm(entityFormOptions)

                    .then((success: any) => {

                        console.log(success);

                    })

                    .catch((error: any) => {

                        console.log(error);

                    });

            }

        } catch (error) {

            console.log(functionName + "" + error);

        }

    }

}