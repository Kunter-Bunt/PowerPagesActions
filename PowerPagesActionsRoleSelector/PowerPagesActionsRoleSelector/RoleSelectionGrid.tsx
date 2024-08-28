import * as React from 'react';
import { DetailsList, DetailsListLayoutMode, IColumn, Selection } from "office-ui-fabric-react/lib/DetailsList";
import { MarqueeSelection } from "office-ui-fabric-react/lib/MarqueeSelection";
import { Fabric } from "office-ui-fabric-react/lib/Fabric";
import { IObjectWithKey } from '@uifabric/utilities/lib/selection/Selection.types';
import { Label } from 'office-ui-fabric-react/lib/Label';

export interface IRoleSelectionGridProps {
  getData: () => Promise<IRecord[]>;
  selectionChanged: (selection: Selection) => void;
}

export interface IRoleSelectionGridState {
  data: IRecord[];
  explanation: string;
}

export interface IRecord extends IObjectWithKey {
  key: string;
  name: string;
  site: string;
  selected: boolean;
}

export class RoleSelectionGrid extends React.Component<IRoleSelectionGridProps, IRoleSelectionGridState> {
  private columns: IColumn[];
  private selection: Selection;

  constructor(props: IRoleSelectionGridProps) {
    super(props);
    this.state = {
      data: [] as IRecord[],
      explanation: "Loading..."
    };
    this.props.getData().then((data) => {
      this.selection.setItems(data, true);
      data.forEach((item) => this.selection.setKeySelected(item.key, item.selected, false));
      this.setState({ data: data });

      this.updateExplanation(this.selection);
    });

    this.columns = [
      {
        key: 'name',
        name: 'Name',
        fieldName: 'name',
        minWidth: 100,
        maxWidth: 400,
        isResizable: true,
      },
      {
        key: 'site',
        name: 'Site',
        fieldName: 'site',
        minWidth: 100,
        maxWidth: 400,
        isResizable: true,
      }
    ];

    this.selection = new Selection({
      getKey: (item: IRecord) => item.key,
      onSelectionChanged: () => {
        this.updateExplanation(this.selection);
        this.props.selectionChanged(this.selection)
      }
    });
    
  }

  private updateExplanation(selection: Selection<IObjectWithKey>): void {
    const selectedItems = selection.getSelection();
    if (selectedItems.length === 0) {
      this.setState({ explanation: "Action is available to all users, including anonymous users (on any site)." });
      return;
    }

    let text = "Action is available to authenticated users (on any site)";
    const selectedRoles = selectedItems
      .map(item => (item as IRecord))
      .filter(item => item.name !== "Authenticated Users")
      .map(item => `${item.name} (${item.site})`);

    if (selectedRoles.length > 0) {
      text += " that have the role " + selectedRoles.join(" or ");
    }

    this.setState({ explanation: text });
  }

  public render(): React.ReactNode {
    return (
      <Fabric style={{ width: "100%" }}>
        <Label>{this.state.explanation}</Label>
        <MarqueeSelection selection={this.selection} >
          <DetailsList
            items={this.state.data}
            columns={this.columns}
            setKey="set"
            layoutMode={DetailsListLayoutMode.justified}
            selection={this.selection}
            selectionPreservedOnEmptyClick={true}
            ariaLabelForSelectionColumn="Toggle selection"
            ariaLabelForSelectAllCheckbox="Toggle selection for all items"
            checkButtonAriaLabel="Row checkbox"
          />
        </MarqueeSelection>
      </Fabric>
    )
  }
}
