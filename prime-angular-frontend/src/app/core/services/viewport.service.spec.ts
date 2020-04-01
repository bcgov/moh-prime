import { TestBed } from '@angular/core/testing';

import { ViewportService } from './viewport.service';

describe('ViewportService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should create', () => {
    const service: ViewportService = TestBed.inject(ViewportService);
    expect(service).toBeTruthy();
  });
});
