import {GeneralHelper} from "../../Shared/Classes/helpers/GeneralHelper";

export class OrderedElement {
  Id: number;
  Text: String;

  constructor(Id: number = GeneralHelper.randomInteger(0,100)*-1,  Text: string = '') {
    this.Id = Id;
    this.Text = Text;
  }
}
