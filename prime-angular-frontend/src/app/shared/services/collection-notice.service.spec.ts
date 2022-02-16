import { TestBed } from '@angular/core/testing';

import { CollectionNoticeService } from './collection-notice.service';

describe('CollectionNoticeService', () => {
  let service: CollectionNoticeService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(CollectionNoticeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
