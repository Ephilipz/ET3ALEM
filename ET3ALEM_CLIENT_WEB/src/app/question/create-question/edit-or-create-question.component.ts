import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { RichTextEditorComponent } from 'src/app/Shared/modules/shared-components/rich-text-editor/rich-text-editor.component';
import { Question } from '../Models/question';
import { McqAnswerType, MultipleChoiceQuestion } from '../Models/mcq';
import { Choice } from '../Models/choice';
import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { TrueFalseQuestion } from '../Models/true-false-question';
import { QuestionType } from '../Models/question-type.enum';
import { mode } from 'src/app/quiz/components/edit-create-quiz/edit-or-create-quiz.component';

@Component({
  selector: 'app-create-question',
  templateUrl: './edit-or-create-question.component.html',
  styleUrls: ['./edit-or-create-question.component.css']
})
export class EditOrCreateQuestionComponent implements OnInit {

  constructor() { }

  @ViewChild('RichTextEditorComponent') private richTextComponent: RichTextEditorComponent;
  @ViewChild('autosize') autosize: CdkTextareaAutosize;

  @Input('question') inputQuestion: Question;

  question: Question;

  questionTypes: Array<any> = [
    { value: 0, text: 'True / False' },
    { value: 1, text: 'Multiple Choice' },
  ];


  questionTypeFC: FormControl;
  questionContentFC: FormControl;

  choices: Array<Choice>;
  deletedChoices: Array<Choice> = [];
  trueOrFalse: boolean = false;


  ngOnInit(): void {
    this.questionTypeFC = new FormControl(this.inputQuestion.QuestionType);
    this.questionContentFC = new FormControl(this.inputQuestion.Body);

    if (this.inputQuestion.QuestionType == QuestionType.MCQ) {
      this.question = new MultipleChoiceQuestion(this.inputQuestion.Id, this.inputQuestion.Body, (<MultipleChoiceQuestion>this.inputQuestion).Choices);
      this.choices = (<MultipleChoiceQuestion>this.question).Choices;
    }

    else if (this.inputQuestion.QuestionType == QuestionType.TrueFalse) {
      this.question = new TrueFalseQuestion(this.inputQuestion.Id, this.inputQuestion.Body, (<TrueFalseQuestion>this.inputQuestion).Answer);
      this.trueOrFalse = (<TrueFalseQuestion>this.question).Answer;
    }

  }

  addChoice() {
    this.choices.push(new Choice());
  }

  removeChoice(choice: Choice) {

    //check if choice existed in original question
    if (choice.Id > 0) {
      //add the deleted choice with a negative id to be deleted by the api
      this.deletedChoices.push(Object.assign({}, { ...choice, Id: -1 * choice.Id }));
    }

    //remove the choice from the current list
    let index = this.choices.findIndex(c => c.Id == choice.Id);
    if (index > -1) {
      this.choices.splice(index, 1);
    }
  }

  correctAnswerCheckChange(choice: Choice, checked: boolean) {
    choice.IsAnswer = checked;
  }

  onQuestionTypeChange(event) {
    switch (event.value) {
      case 1:
        this.question = new MultipleChoiceQuestion(this.question.Id);
        break;

      case 0:
        this.question = new TrueFalseQuestion(this.question.Id);
        break;

      default:
        break;
    }
  }

  public async saveQuestion(_mode: mode = mode.edit) {
    await this.richTextComponent.removeUnusedImages();
    this.question.Body = this.questionContentFC.value;

    if (_mode == mode.create) {
      this.question.Id = 0;
    }

    if (this.questionTypeFC.value == 1 && this.question instanceof MultipleChoiceQuestion) {

      //set ids of current choices to 0 if they're new
      this.choices.forEach(choice => {
        choice.Id = Math.max(0, choice.Id);
      });

      //add the current choices and the deleted choices to the returned question
      this.question.Choices = this.choices.concat(this.deletedChoices);

      //set single choice / multiple choice based on amount of checked answers
      this.question.McqAnswerType = this.choices.filter(choice => choice.IsAnswer).length > 1 ? McqAnswerType.MultipleChoice : McqAnswerType.SingleChoice;
      return Promise.resolve(this.question);
    }

    else if (this.questionTypeFC.value == 0 && this.question instanceof TrueFalseQuestion) {
      this.question.Answer = this.trueOrFalse;
      return Promise.resolve(this.question);
    }

  }

}