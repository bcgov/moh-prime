import { TestBed } from '@angular/core/testing';

import { FeedbackResourceService } from './feedback-resource.service';

describe('FeedbackResourceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: FeedbackResourceService = TestBed.get(FeedbackResourceService);
    expect(service).toBeTruthy();
  });
});
