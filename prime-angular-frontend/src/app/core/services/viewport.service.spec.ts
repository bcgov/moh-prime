import { TestBed } from '@angular/core/testing';

import { ViewportService } from './viewport.service';

describe('ViewportService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should create', () => {
    const service: ViewportService = TestBed.get(ViewportService);
    expect(service).toBeTruthy();
  });
});
