import { TestBed } from '@angular/core/testing';

import { RegistrantService } from './registrant.service';

describe('RegistrantServiceService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: RegistrantService = TestBed.inject(RegistrantService);
    expect(service).toBeTruthy();
  });
});
