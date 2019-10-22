import { TestBed, inject } from '@angular/core/testing';

import { AuthResource } from './auth-resource.service';

describe('AuthResource', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [AuthResource]
    });
  });

  it('should be created', inject([AuthResource], (service: AuthResource) => {
    expect(service).toBeTruthy();
  }));
});
