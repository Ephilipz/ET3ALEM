import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConcreteAnswerQuestionOrderComponent } from './concrete-answer-question-order.component';

describe('ConcreteAnswerQuestionOrderComponent', () => {
  let component: ConcreteAnswerQuestionOrderComponent;
  let fixture: ComponentFixture<ConcreteAnswerQuestionOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConcreteAnswerQuestionOrderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConcreteAnswerQuestionOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
