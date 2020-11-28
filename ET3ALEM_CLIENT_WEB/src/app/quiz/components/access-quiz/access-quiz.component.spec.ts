import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessQuizComponent } from './access-quiz.component';

describe('AccessQuizComponent', () => {
  let component: AccessQuizComponent;
  let fixture: ComponentFixture<AccessQuizComponent>;

  beforeEach(async(() => {
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
