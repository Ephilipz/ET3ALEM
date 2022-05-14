import {Component, OnInit} from '@angular/core';
import {AC_ConcreteQuestionResult} from "../ac-concrete-question-result";
import {OrderAttempt} from "../../../Models/questionAttempt/order-attempt";
import {OrderedElement} from "../../../Models/ordered-element";
import {Question} from "../../../Models/question";
import {OrderQuestion} from "../../../Models/order-question";

@Component({
  selector: 'app-concrete-question-result-order',
  templateUrl: './concrete-question-result-order.component.html',
  styleUrls: ['./concrete-question-result-order.component.css']
})
export class ConcreteQuestionResultOrderComponent extends AC_ConcreteQuestionResult implements OnInit {

  questionAttempt: OrderAttempt;
  correctOrder: Array<OrderedElement>;
  answeredOrder: Array<OrderedElement>;

  constructor() {
    super();
  }

  ngOnInit(): void {
    this.correctOrder = (this.questionAttempt.QuizQuestion.Question as OrderQuestion).OrderedElements;
    let answeredIds = this.questionAttempt.Answer.split(',');
    let copyOfOrderedElements = JSON.parse(JSON.stringify(this.correctOrder));
    this.answeredOrder = copyOfOrderedElements
      .sort((a, b) => answeredIds.indexOf(String(a.Id)) - answeredIds.indexOf(String(b.Id)))
  }




}
