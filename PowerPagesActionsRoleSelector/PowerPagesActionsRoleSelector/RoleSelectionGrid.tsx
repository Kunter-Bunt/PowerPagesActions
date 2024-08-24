import * as React from 'react';
import { DetailsList, DetailsListLayoutMode, IColumn, Selection } from "office-ui-fabric-react/lib/DetailsList";
import { MarqueeSelection } from "office-ui-fabric-react/lib/MarqueeSelection";
import { Fabric } from "office-ui-fabric-react/lib/Fabric";
import { IObjectWithKey } from '@uifabric/utilities/lib/selection/Selection.types';

export interface IRoleSelectionGridProps {
  getData: () => Promise<IRecord[]>;
  selectionChanged: (selection: Selection) => void;
}

export interface IRoleSelectionGridState {
  data: IRecord[];
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
      data: [] as IRecord[]
    };
    this.props.getData().then((d) => {
      this.selection.setItems(d, true);
      d.forEach((item) => this.selection.setKeySelected(item.key, item.selected, false));
      this.setState({ data: d })
    });

    this.columns = [
      {
        key: 'name',
        name: 'Name',
        fieldName: 'name',
        minWidth: 100,
        maxWidth: 200,
        isResizable: true
      },
      {
        key: 'site',
        name: 'Site',
        fieldName: 'site',
        minWidth: 100,
        maxWidth: 200,
        isResizable: true
      }
    ];

    this.selection = new Selection({
      getKey: (item: IRecord) => item.key,
      onSelectionChanged: () => this.props.selectionChanged(this.selection)
    });
    
  }

  public render(): React.ReactNode {
    return (
      <Fabric>
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
