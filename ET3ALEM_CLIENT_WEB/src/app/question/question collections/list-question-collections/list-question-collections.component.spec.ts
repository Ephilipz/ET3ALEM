import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ListQuestionCollectionsComponent } from './list-question-collections.component';

describe('ListQuestionCollectionsComponent', () => {
  let component: ListQuestionCollectionsComponent;
  let fixture: ComponentFixture<ListQuestionCollectionsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ListQuestionCollectionsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ListQuestionCollectionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
