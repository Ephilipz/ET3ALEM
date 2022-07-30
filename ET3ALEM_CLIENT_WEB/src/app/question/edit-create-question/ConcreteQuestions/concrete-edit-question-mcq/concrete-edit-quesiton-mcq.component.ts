import {CdkTextareaAutosize} from '@angular/cdk/text-field';
import {Component, Input, OnInit, ViewChild} from '@angular/core';
import {plainToClass} from 'class-transformer';
import {Choice} from 'src/app/question/Models/choice';
import {McqAnswerType, MultipleChoiceQuestion} from 'src/app/question/Models/mcq';
import {AC_ConcreteEditQuestion} from '../ac-concrete-question';

@Component({
  selector: 'app-concrete-edit-question-mcq',
  templateUrl: './concrete-edit-question-mcq.component.html',
  styleUrls: ['./concrete-edit-question-mcq.component.css']
})
export class ConcreteEditQuestionMCQComponent extends AC_ConcreteEditQuestion implements OnInit {

  @ViewChild('autosize') autosize: CdkTextareaAutosize;

  deletedChoices: Array<Choice> = [];

  @Input() inputQuestion: MultipleChoiceQuestion;

  constructor() {
    super();
  }

  ngOnInit(): void {
    this.inputQuestion = plainToClass(MultipleChoiceQuestion, this.inputQuestion);
  }

  public addChoice() {
    this.inputQuestion.Choices.push(new Choice());
  }

  public removeChoice(choice: Choice) {
    if (choice.Id > 0) {
      const copiedChoice = Object.assign({}, {...choice, Id: -1 * choice.Id})
      this.deletedChoices.push(copiedChoice);
    }

    let index = this.inputQuestion.Choices.findIndex(c => c.Id == choice.Id);
    if (index > -1) {
      this.inputQuestion.Choices.splice(index, 1);
    }
  }

  public correctAnswerCheckChange(choice: Choice, checked: boolean) {
    choice.IsAnswer = checked;
  }

  public saveQuestion() {
    super.saveQuestion();
    this.inputQuestion.McqAnswerType = this.inputQuestion.Choices.filter(c => c.IsAnswer).length > 1 ? McqAnswerType.MultipleChoice : McqAnswerType.SingleChoice;

    this.inputQuestion.Choices.forEach(choice => {
      choice.Id = Math.max(0, choice.Id);
    });

    this.inputQuestion.Choices = this.inputQuestion.Choices.concat(this.deletedChoices);
    return this.inputQuestion;
  }

  protected validate() {
    if (this.inputQuestion.Choices.find(c => !c.Body || c.Body.length == 0)) {
      throw 'MCQ choices cannot be empty';
    }
  }

}
