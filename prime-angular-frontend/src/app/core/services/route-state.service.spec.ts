import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { RouteStateService } from './route-state.service';

describe('RouteStateService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      RouterTestingModule
    ]
  }));

  it('should create', () => {
    const service: RouteStateService = TestBed.inject(RouteStateService);
    expect(service).toBeTruthy();
  });
});
