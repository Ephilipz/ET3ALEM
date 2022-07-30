import {Question} from "./question";
import {OrderedElement} from "./ordered-element";
import {Type} from "class-transformer";
import {QuestionType} from "./question-type.enum";

export class OrderQuestion extends Question
{
  @Type(() => OrderedElement)
  OrderedElements: Array<OrderedElement>;
  CorrectOrderIds: String;

  constructor(Id: number = 0, Body: string = '', OrderedElements = [new OrderedElement(), new OrderedElement()], CorrectOrderIds: String = null, Comment: string = null) {
    super(Id, Body, Comment);
    this.QuestionType = QuestionType.OrderQuestion;
    this.OrderedElements = OrderedElements;
    this.CorrectOrderIds = CorrectOrderIds;
  }
}
