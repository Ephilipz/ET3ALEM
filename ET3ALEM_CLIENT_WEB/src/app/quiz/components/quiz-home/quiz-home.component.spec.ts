import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { QuizHomeComponent } from './quiz-home.component';

describe('QuizHomeComponent', () => {
  let component: QuizHomeComponent;
  let fixture: ComponentFixture<QuizHomeComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizHomeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizHomeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
