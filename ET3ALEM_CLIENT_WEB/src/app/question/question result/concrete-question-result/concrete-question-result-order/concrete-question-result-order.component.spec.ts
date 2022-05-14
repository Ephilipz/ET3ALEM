import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConcreteQuestionResultOrderComponent } from './concrete-question-result-order.component';

describe('ConcreteQuestionResultOrderComponent', () => {
  let component: ConcreteQuestionResultOrderComponent;
  let fixture: ComponentFixture<ConcreteQuestionResultOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConcreteQuestionResultOrderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConcreteQuestionResultOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
