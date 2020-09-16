import { Component, OnInit, ViewChild, Input } from '@angular/core';
import { FormControl } from '@angular/forms';
import { RichTextEditorComponent } from 'src/app/Shared/modules/shared-components/rich-text-editor/rich-text-editor.component';
import { Question } from '../Models/question';
import { MultipleChoiceQuestion } from '../Models/mcq';
import { Choice } from '../Models/choice';
import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { TrueFalseQuestion } from '../Models/true-false-question';
import { QuestionType } from '../Models/question-type.enum';

@Component({
  selector: 'app-create-question',
  templateUrl: './create-question.component.html',
  styleUrls: ['./create-question.component.css']
})
export class CreateQuestionComponent implements OnInit {

  constructor() { }

  @ViewChild(RichTextEditorComponent) private richTextComponent: RichTextEditorComponent;
  @ViewChild('autosize') autosize: CdkTextareaAutosize;

  @Input('question') inputQuestion: Question;

  question: Question;

  questionTypes: Array<any> = [
    { value: 0, text: 'Multiple Choice' },
    { value: 1, text: 'True / False' }
  ];


  questionTypeFC: FormControl;
  questionContentFC: FormControl;

  choices: Array<Choice>;
  trueOrFalse: boolean;


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

  onQuestionTypeChange(event) {
    switch (event.value) {
      case 0:
        this.question = new MultipleChoiceQuestion(this.question.Id, this.question.Body);
        break;

      case 1:
        this.question = new TrueFalseQuestion(this.question.Id, this.question.Body);
        break;

      default:
        break;
    }
  }

  public async saveQuestion() {
    await this.richTextComponent.removeUnusedImages();
    this.question.Body = this.questionContentFC.value;

    if (this.question instanceof MultipleChoiceQuestion) {
      this.question.Choices = this.choices;
      return Promise.resolve(this.question);
    }

    else if (this.question instanceof TrueFalseQuestion) {
      this.question.Answer = this.trueOrFalse;
      return Promise.resolve(this.question);
    }

  }

}
