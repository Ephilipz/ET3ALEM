import { Component, ComponentFactoryResolver, ComponentRef, Input, OnInit, Type, ViewChild } from '@angular/core';
import { QuizQuestion } from 'src/app/quiz/Model/quizQuestion';
import { DynamicComponentHostDirective } from 'src/app/Shared/directives/dynamic-component-host.directive';
import { QuestionType } from '../../Models/question-type.enum';
import { QuestionTypeResolver } from '../../shared/question-type-resolver';
import { AC_ConcreteAnswerQuestion } from '../ConcreteAnswerQuestions/ac-concrete-answer-question';

@Component({
  selector: 'answer-question-header',
  templateUrl: './answer-question-header.component.html',
  styleUrls: ['./answer-question-header.component.css']
})
export class AnswerQuestionHeaderComponent implements OnInit {

  @ViewChild(DynamicComponentHostDirective, { static: true })
  dynamicComponentHost: DynamicComponentHostDirective;
  componentRef: ComponentRef<AC_ConcreteAnswerQuestion>;

  @Input() quizQuestion : QuizQuestion;

  constructor(private componentFactoryResolver: ComponentFactoryResolver) { }

  ngOnInit(): void {
    this.createQuestionComponent();
  }
  createQuestionComponent() {
    const question = this.quizQuestion.Question;
    const questionTypeString = QuestionType[question.QuestionType];
    const component: Type<AC_ConcreteAnswerQuestion> = QuestionTypeResolver.answerQuestionComponentMap[questionTypeString];
    if (!component)
      return;
    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(component);

    const viewContainerRef = this.dynamicComponentHost.viewContainerRef;
    viewContainerRef.clear();

    this.componentRef = viewContainerRef.createComponent(componentFactory);
    this.componentRef.instance.question = question;
  }

}
