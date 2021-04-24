import { Component, OnInit } from '@angular/core';
import { Choice } from 'src/app/question/Models/choice';
import { MultipleChoiceQuestion } from 'src/app/question/Models/mcq';
import { MCQAttempt } from 'src/app/question/Models/mcq-attempt';
import { AC_ConcreteQuestionResult } from '../ac-concrete-question-result';

@Component({
  selector: 'app-concrete-question-result-mcq',
  templateUrl: './concrete-question-result-mcq.component.html',
  styleUrls: ['./concrete-question-result-mcq.component.css']
})
export class ConcreteQuestionResultMCQComponent implements OnInit, AC_ConcreteQuestionResult {

  questionAttempt: MCQAttempt;
  choices: Array<Choice> = [];

  constructor() { }

  ngOnInit(): void {
    if (this.questionAttempt) {
      this.choices = (<MultipleChoiceQuestion>this.questionAttempt.QuizQuestion.Question).Choices;
    }
  }

  isSelected(choice: Choice): boolean{
    return this.questionAttempt.Choices.findIndex(c => c.Id == choice.Id) != -1;
  }

}
