import {
  Component,
  OnInit,
  ViewChild,
  Input,
  ComponentFactoryResolver,
  ViewContainerRef,
  ComponentFactory,
  ComponentRef,
  Type,
  OnDestroy
} from '@angular/core';
import {FormControl, Validators} from '@angular/forms';
import {RichTextEditorComponent} from 'src/app/Shared/modules/shared-components/rich-text-editor/rich-text-editor.component';
import {Question} from '../../Models/question';
import {McqAnswerType, MultipleChoiceQuestion} from '../../Models/mcq';
import {Choice} from '../../Models/choice';
import {CdkTextareaAutosize} from '@angular/cdk/text-field';
import {TrueFalseQuestion} from '../../Models/true-false-question';
import {QuestionType} from '../../Models/question-type.enum';
import {mode} from 'src/app/quiz/components/edit-create-quiz/edit-or-create-quiz.component';
import {QuestionTypeResolver} from '../../shared/question-type-resolver';
import {AC_ConcreteEditQuestion} from '../ConcreteQuestions/ac-concrete-question';
import {DynamicComponentHostDirective} from '../../../Shared/directives/dynamic-component-host.directive';
import {GeneralHelper} from 'src/app/Shared/Classes/helpers/GeneralHelper';

@Component({
  selector: 'app-create-question',
  templateUrl: './edit-or-create-questionHeader.component.html',
  styleUrls: ['./edit-or-create-questionHeader.component.css']
})
export class EditOrCreateQuestionHeaderComponent implements OnInit, OnDestroy {

  @ViewChild(DynamicComponentHostDirective, {static: true}) dynamicComponentHost: DynamicComponentHostDirective;
  componentRef: ComponentRef<AC_ConcreteEditQuestion>;

  constructor(private componentFactoryResolver: ComponentFactoryResolver) {

  }

  @ViewChild('RichTextEditorComponent')
  richTextComponent: RichTextEditorComponent;

  isLoaded: boolean = false;

  @Input('question') inputQuestion: Question;
  @Input('grade') inputGrade: number = 1;
  @Input() hideGrade: boolean = false;

  question: Question;

  questionTypes = QuestionTypeResolver.questionTypeNames;

  questionTypeFC: FormControl = new FormControl();
  questionContentFC: FormControl = new FormControl();
  grade: FormControl = new FormControl(null, [Validators.min(0)]);
  commentFC: FormControl = new FormControl(null);

  showCommentBox: boolean = false;

  ngOnInit(): void {
    this.question = GeneralHelper.deepCopy(this.inputQuestion);
    this.questionContentFC.setValue(this.question.Body);
    this.grade.setValue(this.inputGrade);
    this.commentFC.setValue(this.question.Comment);
    this.showCommentBox = this.question.Comment != null;
    this.createQuestionComponent();
  }

  createQuestionComponent() {
    const questionTypeString = QuestionType[this.question.QuestionType];
    const component: Type<AC_ConcreteEditQuestion> = QuestionTypeResolver.editQuestionComponentsMap[questionTypeString];
    if (!component)
      return;
    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(component);

    const viewContainerRef = this.dynamicComponentHost.viewContainerRef;
    viewContainerRef.clear();

    this.componentRef = viewContainerRef.createComponent(componentFactory);
    this.componentRef.instance.inputQuestion = this.question;
    this.questionTypeFC.setValue(this.question.QuestionType);
  }

  onQuestionTypeChange(event) {
    this.question = QuestionTypeResolver.getNewQuestionInstance(event.value);
    this.question.Id = this.inputQuestion?.Id ?? 0;
    this.createQuestionComponent();
  }

  public async saveQuestion() {
    await this.richTextComponent.removeUnusedImages();

    this.question = this.componentRef.instance.saveQuestion();
    this.question.Body = this.questionContentFC.value;
    this.question.Comment = this.showCommentBox ? this.commentFC.value.trim() : null;

    return this.question;
  }

  public getQuestion() {
    let question = this.componentRef.instance.getQuestion();
    question.Body = this.questionContentFC.value;
    return question;
  }

  public getGrade(): number {
    return this.grade.value ? Math.max(0, this.grade.value) : 1;
  }

  public validateQuestion() {
    if(!this.questionContentFC.value || this.questionContentFC.value.length == 0)
      throw 'Question body cannot be empty';
    this.componentRef.instance.checkQuestionValidation();
  }

  ngOnDestroy(): void {
    this.componentRef.destroy();
  }

}
