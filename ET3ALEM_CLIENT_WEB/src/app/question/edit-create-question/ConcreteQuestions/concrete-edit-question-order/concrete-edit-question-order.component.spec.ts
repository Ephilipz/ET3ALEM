import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ConcreteEditQuestionOrderComponent } from './concrete-edit-question-order.component';

describe('ConcreteEditQuestionOrderComponent', () => {
  let component: ConcreteEditQuestionOrderComponent;
  let fixture: ComponentFixture<ConcreteEditQuestionOrderComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ConcreteEditQuestionOrderComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ConcreteEditQuestionOrderComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
