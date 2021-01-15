import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConcreteAnswerQuestionTFComponent } from './concrete-answer-question-tf.component';

describe('ConcreteAnswerQuestionTFComponent', () => {
  let component: ConcreteAnswerQuestionTFComponent;
  let fixture: ComponentFixture<ConcreteAnswerQuestionTFComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConcreteAnswerQuestionTFComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConcreteAnswerQuestionTFComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
