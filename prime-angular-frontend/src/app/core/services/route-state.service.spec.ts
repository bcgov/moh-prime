import { TestBed } from '@angular/core/testing';

import { RouteStateService } from './route-state.service';

describe('RouteStateService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RouteStateService = TestBed.get(RouteStateService);
    expect(service).toBeTruthy();
  });
});
