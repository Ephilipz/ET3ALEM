import {Component, Input, OnInit} from '@angular/core';
import {CdkDragDrop, moveItemInArray} from "@angular/cdk/drag-drop";
import {AC_ConcreteAnswerQuestion} from "../ac-concrete-answer-question";
import {OrderAttempt} from "../../../Models/questionAttempt/order-attempt";
import {OrderQuestion} from "../../../Models/order-question";
import {OrderedElement} from "../../../Models/ordered-element";

@Component({
  selector: 'app-concrete-answer-question-order',
  templateUrl: './concrete-answer-question-order.component.html',
  styleUrls: ['./concrete-answer-question-order.component.css']
})
export class ConcreteAnswerQuestionOrderComponent extends AC_ConcreteAnswerQuestion implements OnInit {

  @Input() questionAttempt: OrderAttempt;
  orderedElements: Array<OrderedElement> = [];

  constructor() {
    super();
  }

  ngOnInit(): void {
    let orderedElementsFromQuestion = (this.questionAttempt.QuizQuestion.Question as OrderQuestion).OrderedElements;
    let copyOfOrderedElements = JSON.parse(JSON.stringify(orderedElementsFromQuestion));
    this.orderedElements = copyOfOrderedElements;

    if(this.questionAttempt.Answer)
    {
      let answeredIds = this.questionAttempt.Answer.split(',');
      this.orderedElements = copyOfOrderedElements
        .sort((a, b) => answeredIds.indexOf(String(a.Id)) - answeredIds.indexOf(String(b.Id)))
    }

  }

  drop($event: CdkDragDrop<OrderedElement[]>) {
    moveItemInArray(this.orderedElements, $event.previousIndex, $event.currentIndex);
  }

  getAnswers() {
    this.questionAttempt.Answer = this.orderedElements
      .map(element => element.Id).join(',');
    return this.questionAttempt;
  }

}
