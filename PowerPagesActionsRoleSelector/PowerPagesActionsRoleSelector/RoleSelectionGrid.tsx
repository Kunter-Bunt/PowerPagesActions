import * as React from 'react';
import { Label } from '@fluentui/react';
import { DetailsList, DetailsListLayoutMode, Selection } from "office-ui-fabric-react/lib/DetailsList";
import { MarqueeSelection } from "office-ui-fabric-react/lib/MarqueeSelection";
import { Fabric } from "office-ui-fabric-react/lib/Fabric";

export interface IRoleSelectionGridProps {
  logicalname?: string;
  id?: string;
  data: IRecord[];
}

export interface IRecord {
  key: string;
  selected: boolean;
}

export class RoleSelectionGrid extends React.Component<IRoleSelectionGridProps> {
  public render(): React.ReactNode {
    let selection = new Selection({
      onSelectionChanged: () => {
          this.onRowSelection(selection.count);
      }
  });

    return (
      <Fabric>
      <MarqueeSelection selection= { selection } >
          <DetailsList
              items={ this.props.data }
              //columns = { this._columns }
              setKey ="set"
              layoutMode = { DetailsListLayoutMode.justified }
              selection = { selection }
              selectionPreservedOnEmptyClick = { true}
              ariaLabelForSelectionColumn ="Toggle selection"
              ariaLabelForSelectAllCheckbox ="Toggle selection for all items"
              checkButtonAriaLabel ="Row checkbox"
              //onItemInvoked = { this._onItemInvoked }
          />
      </MarqueeSelection>
  </Fabric>
    )
  }

  private onRowSelection = (rowIndex: number) => {
    let functionName: string = "onRowSelection";
    let selectedRowId: string;
    let selectedCardIndex: number;
    /*
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
    */
};
}
