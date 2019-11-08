import { TestBed } from '@angular/core/testing';

import { WindowRefService } from './window-ref.service';

describe('WindowRefService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should create', () => {
    const service: WindowRefService = TestBed.get(WindowRefService);
    expect(service).toBeTruthy();
  });
});
