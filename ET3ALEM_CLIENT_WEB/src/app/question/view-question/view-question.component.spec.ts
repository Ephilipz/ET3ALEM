import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { ViewQuestionComponent } from './view-question.component';

describe('ViewQuestionComponent', () => {
  let component: ViewQuestionComponent;
  let fixture: ComponentFixture<ViewQuestionComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewQuestionComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewQuestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
