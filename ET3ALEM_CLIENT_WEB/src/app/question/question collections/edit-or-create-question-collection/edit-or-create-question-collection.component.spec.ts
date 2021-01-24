import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EditOrCreateQuestionCollectionComponent } from './edit-or-create-question-collection.component';

describe('EditOrCreateQuestionCollectionComponent', () => {
  let component: EditOrCreateQuestionCollectionComponent;
  let fixture: ComponentFixture<EditOrCreateQuestionCollectionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EditOrCreateQuestionCollectionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EditOrCreateQuestionCollectionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
