import { TestBed } from '@angular/core/testing';

import { CompletedQuizService } from './completed-quiz.service';

describe('CompletedQuizService', () => {
  let service: CompletedQuizService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CompletedQuizService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
