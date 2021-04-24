import { Component, OnInit, ViewChild, ComponentRef, Input, ComponentFactoryResolver, Type } from '@angular/core';
import { DynamicComponentHostDirective } from 'src/app/Shared/directives/dynamic-component-host.directive';
import { QuestionAttempt } from '../../Models/question-attempt';
import { QuestionType } from '../../Models/question-type.enum';
import { QuestionTypeResolver } from '../../shared/question-type-resolver';
import { AC_ConcreteQuestionResult } from '../concrete-question-result/ac-concrete-question-result';

@Component({
  selector: 'question-result-header',
  templateUrl: './question-result-header.component.html',
  styleUrls: ['./question-result-header.component.css']
})
export class QuestionResultHeaderComponent implements OnInit {

  @ViewChild(DynamicComponentHostDirective, { static: true })
  dynamicComponentHost: DynamicComponentHostDirective;
  componentRef: ComponentRef<AC_ConcreteQuestionResult>;

  @Input() questionAttempt: QuestionAttempt;

  constructor(private componentFactoryResolver: ComponentFactoryResolver) { }

  ngOnInit(): void {
    this.createQuestionResultComponent();
  }

  private createQuestionResultComponent() {
    const question = this.questionAttempt.QuizQuestion.Question;
    const questionTypeString = QuestionType[question.QuestionType];
    const component: Type<AC_ConcreteQuestionResult> = QuestionTypeResolver.viewQuestionResultComponentMap[questionTypeString];
    if (!component)
      return;
    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(component);
    const viewContainerRef = this.dynamicComponentHost.viewContainerRef;
    this.componentRef = viewContainerRef.createComponent(componentFactory);
    this.componentRef.instance.questionAttempt = this.questionAttempt;
  }

}
