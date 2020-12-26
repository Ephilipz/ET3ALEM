import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { EditOrCreateQuizComponent } from './edit-or-create-quiz.component';

describe('CreateQuizComponent', () => {
  let component: EditOrCreateQuizComponent;
  let fixture: ComponentFixture<EditOrCreateQuizComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ EditOrCreateQuizComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditOrCreateQuizComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
