import { TestBed } from '@angular/core/testing';

import { QuestionCollectionService } from './question-collection.service';

describe('QuestionCollectionService', () => {
  let service: QuestionCollectionService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(QuestionCollectionService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
