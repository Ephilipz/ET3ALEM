import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { AccessQuizComponent } from './access-quiz.component';

describe('AccessQuizComponent', () => {
  let component: AccessQuizComponent;
  let fixture: ComponentFixture<AccessQuizComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ AccessQuizComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessQuizComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
