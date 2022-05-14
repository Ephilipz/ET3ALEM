import {Component, Input, OnInit} from '@angular/core';
import {AC_ConcreteEditQuestion} from "../ac-concrete-question";
import {plainToClass} from "class-transformer";
import {CdkDragDrop, moveItemInArray} from "@angular/cdk/drag-drop";
import {OrderedElement} from "../../../Models/ordered-element";
import {OrderQuestion} from "../../../Models/order-question";

@Component({
  selector: 'app-concrete-edit-question-order',
  templateUrl: './concrete-edit-question-order.component.html',
  styleUrls: ['./concrete-edit-question-order.component.css']
})
export class ConcreteEditQuestionOrderComponent extends AC_ConcreteEditQuestion implements OnInit {

  @Input() inputQuestion: OrderQuestion;
  private deletedOrderedElements: Array<OrderedElement> = [];

  constructor() {
    super();
  }

  ngOnInit(): void {
    this.inputQuestion = plainToClass(OrderQuestion, this.inputQuestion);
  }

  public addOrderElement() {
    this.inputQuestion.OrderedElements.push(new OrderedElement())
  }

  public removeOrderElement(orderElement: OrderedElement) {
    this.addElementToDeletedList(orderElement);
    this.removeElementFromInputQuestion(orderElement);
  }

  private removeElementFromInputQuestion(orderElement: OrderedElement) {
    let index = this.inputQuestion.OrderedElements.findIndex(c => c.Id == orderElement.Id);
    if (index > -1) {
      this.inputQuestion.OrderedElements.splice(index, 1);
    }
  }

  private addElementToDeletedList(orderElement: OrderedElement) {
    if (orderElement.Id <= 0) {
      return;
    }
    const copiedOrderedElement = Object.assign({}, {
      ...orderElement,
      Id: -1 * orderElement.Id
    })
    this.deletedOrderedElements.push(copiedOrderedElement);
  }

  public saveQuestion(): any {
    super.saveQuestion();

    this.inputQuestion.OrderedElements.forEach(element => {
      element.Id = Math.max(0, element.Id);
    });

    this.inputQuestion.OrderedElements = this.inputQuestion.OrderedElements.concat(this.deletedOrderedElements);
    return this.inputQuestion;
  }

  protected validate() {
  }

  canDeleteElement() {
    return this.inputQuestion.OrderedElements.length > 2;
  }

  drop($event: CdkDragDrop<OrderedElement[]>) {
    moveItemInArray(this.inputQuestion.OrderedElements, $event.previousIndex, $event.currentIndex);
  }
}
