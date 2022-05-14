import { Component, OnInit } from '@angular/core';
import { LongAnswerQuestion } from 'src/app/question/Models/long-answer-question';
import { Question } from 'src/app/question/Models/question';
import { AC_ConcreteEditQuestion } from '../ac-concrete-question';

@Component({
  selector: 'app-concrete-edit-question-long-answer',
  templateUrl: './concrete-edit-question-long-answer.component.html',
  styleUrls: ['./concrete-edit-question-long-answer.component.css']
})
export class ConcreteEditQuestionLongAnswerComponent extends AC_ConcreteEditQuestion implements OnInit {

  inputQuestion: LongAnswerQuestion;

  constructor() {
    super();
  }

  ngOnInit(): void {
  }

  saveQuestion() {
    super.saveQuestion();
    return this.getQuestion();
  }

  getQuestion(): Question {
    return this.inputQuestion;
  }

  protected validate() {
  }


}
