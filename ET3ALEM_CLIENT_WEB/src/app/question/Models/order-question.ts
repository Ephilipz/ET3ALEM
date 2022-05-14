import {Question} from "./question";
import {OrderedElement} from "./ordered-element";
import {Type} from "class-transformer";
import {QuestionType} from "./question-type.enum";

export class OrderQuestion extends Question
{
  @Type(() => OrderedElement)
  OrderedElements: Array<OrderedElement>;

  constructor(Id: number = 0, Body: string = '', OrderedElements = [new OrderedElement(), new OrderedElement()], Comment: string = null) {
    super(Id, Body, Comment);
    this.QuestionType = QuestionType.OrderQuestion;
    this.OrderedElements = OrderedElements;
  }
}
