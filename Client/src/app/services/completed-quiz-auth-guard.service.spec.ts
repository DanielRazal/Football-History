import { TestBed } from '@angular/core/testing';

import { CompletedQuizAuthGuardService } from './completed-quiz-auth-guard.service';

describe('CompletedQuizAuthGuardService', () => {
  let service: CompletedQuizAuthGuardService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CompletedQuizAuthGuardService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
