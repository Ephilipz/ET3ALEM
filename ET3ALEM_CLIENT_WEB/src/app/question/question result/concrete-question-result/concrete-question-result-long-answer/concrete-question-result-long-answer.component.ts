import { Component, Input, OnInit } from '@angular/core';
import { AC_ConcreteQuestionResult } from '../ac-concrete-question-result';
import {
  LongAnswerAttempt
} from "../../../Models/questionAttempt/long-answer-attempt";

@Component({
  selector: 'app-concrete-question-result-long-answer',
  templateUrl: './concrete-question-result-long-answer.component.html',
  styleUrls: ['./concrete-question-result-long-answer.component.css']
})
export class ConcreteQuestionResultLongAnswerComponent extends AC_ConcreteQuestionResult implements OnInit {

  @Input() questionAttempt: LongAnswerAttempt;
  isCorrectAnswer = false;

  constructor() {
    super();
  }

  ngOnInit(): void {
    this.isCorrectAnswer = this.questionAttempt.Grade >= this.questionAttempt.QuizQuestion.Grade;
  }

}
