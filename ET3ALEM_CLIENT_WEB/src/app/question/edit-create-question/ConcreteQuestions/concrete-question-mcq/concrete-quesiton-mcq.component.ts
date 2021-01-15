import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { Choice } from 'src/app/question/Models/choice';
import { McqAnswerType, MultipleChoiceQuestion } from 'src/app/question/Models/mcq';
import { AC_ConcreteEditQuestion } from '../ac-concrete-question';

@Component({
  selector: 'app-concrete-question-mcq',
  templateUrl: './concrete-question-mcq.component.html',
  styleUrls: ['./concrete-question-mcq.component.css']
})
export class ConcreteQuestionMCQComponent extends AC_ConcreteEditQuestion implements OnInit {

  @ViewChild('autosize') autosize: CdkTextareaAutosize;

  deletedChoices: Array<Choice> = [];

  @Input() inputQuestion: MultipleChoiceQuestion;

  constructor() {
    super();
  }

  ngOnInit(): void {
    //if no choice is checked, check the first one
    this.checkCorrectAnswersCount();
  }

  public addChoice() {
    this.inputQuestion.Choices.push(new Choice());
  }

  public removeChoice(choice: Choice) {
    //check if choice existed in original question
    if (choice.Id > 0) {
      //add the deleted choice with a negative id to be deleted by the api
      this.deletedChoices.push(Object.assign({}, { ...choice, Id: -1 * choice.Id }));
    }

    //remove the choice from the current list
    let index = this.inputQuestion.Choices.findIndex(c => c.Id == choice.Id);
    if (index > -1) {
      this.inputQuestion.Choices.splice(index, 1);
    }

    //if it was the only checked answer, check the first choice in the list
    this.checkCorrectAnswersCount();
  }

  checkCorrectAnswersCount() {
    if (this.inputQuestion.Choices.filter(c => c.IsAnswer).length == 0)
      this.inputQuestion.Choices[0].IsAnswer = true;
  }

  public correctAnswerCheckChange(choice: Choice, checked: boolean) {
    choice.IsAnswer = checked;
  }

  public canUncheck(choice: Choice): boolean {
    if (this.inputQuestion.Choices.filter(c => c.IsAnswer && c.Id != choice.Id).length == 0)
      return false;
    return true;
  }
  public getQuestion() {
    super.getQuestion();
    this.inputQuestion.McqAnswerType = this.inputQuestion.Choices.filter(c => c.IsAnswer).length > 1 ? McqAnswerType.MultipleChoice : McqAnswerType.SingleChoice;

    //set ids of current choices to 0 if they're new
    this.inputQuestion.Choices.forEach(choice => {
      choice.Id = Math.max(0, choice.Id);
    });

    //add the current choices and the deleted choices to the returned question
    this.inputQuestion.Choices = this.inputQuestion.Choices.concat(this.deletedChoices);
    return this.inputQuestion;
  }

  protected validate(): boolean {
    if (this.inputQuestion.Choices.filter(c => c.IsAnswer).length == 0)
      return false;
    return true;
  }

}
