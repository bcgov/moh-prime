import { TestBed } from '@angular/core/testing';

import { AppConfig, APP_CONFIG } from 'app/app-config.module';

import { CollectionNoticeService } from './collection-notice.service';

describe('CollectionNoticeService', () => {
  let service: CollectionNoticeService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        {
          provide: APP_CONFIG,
          useValue: AppConfig
        }
      ]
    });
    service = TestBed.inject(CollectionNoticeService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
