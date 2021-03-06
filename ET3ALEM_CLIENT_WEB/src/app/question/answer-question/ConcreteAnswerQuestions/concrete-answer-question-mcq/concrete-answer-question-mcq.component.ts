import { animate, state, style, transition, trigger } from '@angular/animations';
import { Component, Input, OnInit } from '@angular/core';
import { Choice } from 'src/app/question/Models/choice';
import { McqAnswerType, MultipleChoiceQuestion } from 'src/app/question/Models/mcq';
import { MCQAttempt } from 'src/app/question/Models/mcq-attempt';
import { QuizQuestion } from 'src/app/quiz/Model/quizQuestion';
import { AC_ConcreteAnswerQuestion } from '../ac-concrete-answer-question';

@Component({
  selector: 'app-concrete-answer-question-mcq',
  templateUrl: './concrete-answer-question-mcq.component.html',
  styleUrls: ['./concrete-answer-question-mcq.component.css'],
  animations: [
    trigger('selectionChange', [
      state('selected', style({
        backgroundColor: '#5F8EBA',
        color: 'white'
      })),
      state('deselected', style({
        backgroundColor: '#ebebeb',
        color: 'black'
      })),
      transition('selected <=> deselected', [
        animate('0.15s')
      ])
    ])
  ]
})

export class ConcreteAnswerQuestionMCQComponent extends AC_ConcreteAnswerQuestion implements OnInit {

  @Input() quizQuestion: QuizQuestion;
  question: MultipleChoiceQuestion;
  selectedChoices: Array<number> = [];
  selectedChoice: number = null;
  headerText: string = 'select one';

  constructor() {
    super();
  }

  ngOnInit(): void {
    this.question = <MultipleChoiceQuestion>this.quizQuestion.Question;
    if(this.question.McqAnswerType == McqAnswerType.MultipleChoice)
      this.headerText = 'select all that apply'
  }

  getAnswers() {
    return new MCQAttempt(0, this.quizQuestion, 0, this.getSelected())
  }

  selectChoice(choice: Choice) {
    switch (this.question.McqAnswerType) {
      case McqAnswerType.MultipleChoice:
        const index = this.selectedChoices.findIndex(id => choice.Id == id);
        if (index == -1)
          this.selectedChoices.push(choice.Id);
        else
          this.selectedChoices.splice(index, 1);
        break;
      case McqAnswerType.SingleChoice:
        if (this.selectedChoice == choice.Id)
          this.selectedChoice = null;
        else
          this.selectedChoice = choice.Id;
    }
  }

  isSelected(choice: Choice) {
    return this.selectedChoices.indexOf(choice.Id) != -1 || this.selectedChoice == choice.Id;
  }

  getSelected(){
    const choiceBodies = this.question.McqAnswerType == McqAnswerType.MultipleChoice ? this.selectedChoices : [this.selectedChoice];
    return choiceBodies.map(num => new Choice(num));
  }
}
