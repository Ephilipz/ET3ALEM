import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { LongAnswer } from 'src/app/question/Models/long-answer';
import { LongAnswerAttempt } from 'src/app/question/Models/long-answer-attempt';
import { AC_ConcreteAnswerQuestion } from '../ac-concrete-answer-question';

@Component({
  selector: 'app-concrete-answer-question-long-answer',
  templateUrl: './concrete-answer-question-long-answer.component.html',
  styleUrls: ['./concrete-answer-question-long-answer.component.css']
})
export class ConcreteAnswerQuestionLongAnswerComponent implements OnInit, AC_ConcreteAnswerQuestion {

  questionAttempt: LongAnswerAttempt;
  longAnswerFC = new FormControl(null);

  constructor() { }

  ngOnInit(): void {
  }

  getAnswers() {
    if (this.questionAttempt.Answer)
      this.questionAttempt.Answer.Answer = this.longAnswerFC.value;
    else
      this.questionAttempt.Answer = new LongAnswer(0, this.longAnswerFC.value, this.questionAttempt.Id);
    return this.questionAttempt;
  }


}
