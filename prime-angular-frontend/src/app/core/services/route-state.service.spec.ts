import { TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { RouteStateService } from './route-state.service';

describe('RouteStateService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      RouterTestingModule
    ]
  }));

  it('should be created', () => {
    const service: RouteStateService = TestBed.get(RouteStateService);
    expect(service).toBeTruthy();
  });
});
