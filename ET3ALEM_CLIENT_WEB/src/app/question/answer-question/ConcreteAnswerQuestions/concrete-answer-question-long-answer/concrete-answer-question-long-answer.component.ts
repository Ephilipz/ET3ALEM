import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { LongAnswer } from 'src/app/question/Models/long-answer';
import { AC_ConcreteAnswerQuestion } from '../ac-concrete-answer-question';
import {
  LongAnswerAttempt
} from "../../../Models/questionAttempt/long-answer-attempt";

@Component({
  selector: 'app-concrete-answer-question-long-answer',
  templateUrl: './concrete-answer-question-long-answer.component.html',
  styleUrls: ['./concrete-answer-question-long-answer.component.css']
})
export class ConcreteAnswerQuestionLongAnswerComponent extends AC_ConcreteAnswerQuestion implements OnInit {

  questionAttempt: LongAnswerAttempt;
  longAnswerFC = new FormControl(null);

  constructor() {
    super();
  }

  ngOnInit(): void {
  }

  getAnswers() {
    if (this.questionAttempt.LongAnswer)
      this.questionAttempt.LongAnswer.Answer = this.longAnswerFC.value;
    else
      this.questionAttempt.LongAnswer = new LongAnswer(0, this.longAnswerFC.value, this.questionAttempt.Id);
    return this.questionAttempt;
  }


}
