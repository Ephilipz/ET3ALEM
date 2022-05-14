import { Component, OnInit } from '@angular/core';
import { Choice } from 'src/app/question/Models/choice';
import { MultipleChoiceQuestion } from 'src/app/question/Models/mcq';
import { AC_ConcreteQuestionResult } from '../ac-concrete-question-result';
import {MCQAttempt} from "../../../Models/questionAttempt/mcq-attempt";

@Component({
  selector: 'app-concrete-question-result-mcq',
  templateUrl: './concrete-question-result-mcq.component.html',
  styleUrls: ['./concrete-question-result-mcq.component.css']
})
export class ConcreteQuestionResultMCQComponent extends AC_ConcreteQuestionResult implements OnInit  {

  questionAttempt: MCQAttempt;
  choices: Array<Choice> = [];

  constructor() {
    super();
  }

  ngOnInit(): void {
    if (this.questionAttempt) {
      this.choices = (<MultipleChoiceQuestion>this.questionAttempt.QuizQuestion.Question).Choices;
    }
  }

  isSelected(choice: Choice): boolean{
    return this.questionAttempt.Choices.findIndex(c => c.Id == choice.Id) != -1;
  }

  isCorrectAnswer(choice: Choice) {
    return choice.IsAnswer || this.questionAttempt.Grade >= this.questionAttempt.QuizQuestion.Grade;
  }
}
