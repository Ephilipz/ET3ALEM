import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { plainToClass } from 'class-transformer';
import { ToastrService } from 'ngx-toastr';
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

  constructor(private toastrService: ToastrService) {
    super();
  }

  ngOnInit(): void {
    this.inputQuestion = plainToClass(MultipleChoiceQuestion, this.inputQuestion);
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
  }

  public correctAnswerCheckChange(choice: Choice, checked: boolean) {
    choice.IsAnswer = checked;
  }

  public saveQuestion(){
    super.saveQuestion();
    this.inputQuestion.McqAnswerType = this.inputQuestion.Choices.filter(c => c.IsAnswer).length > 1 ? McqAnswerType.MultipleChoice : McqAnswerType.SingleChoice;

    //set ids of current choices to 0 if they're new
    this.inputQuestion.Choices.forEach(choice => {
      choice.Id = Math.max(0, choice.Id);
    });

    //add the current choices and the deleted choices to the returned question
    this.inputQuestion.Choices = this.inputQuestion.Choices.concat(this.deletedChoices);
    return this.getQuestion();
  }

  protected validate() {
    if (this.inputQuestion.Choices.filter(c => c.IsAnswer).length == 0)
      this.toastrService.warning('MCQ should have at least one correct answer');
  }

}
