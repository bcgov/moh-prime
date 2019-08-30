import { TestBed } from '@angular/core/testing';

import { AuthResourceService } from './auth-resource.service';

describe('AuthResourceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: AuthResourceService = TestBed.get(AuthResourceService);
    expect(service).toBeTruthy();
  });
});
