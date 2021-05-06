import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { HealthAuthSiteRegFormStateService } from './health-auth-site-reg-form-state.service';

describe('HealthAuthSiteRegFormStateService', () => {
  let service: HealthAuthSiteRegFormStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        RouterTestingModule,
      ],
    });
    service = TestBed.inject(HealthAuthSiteRegFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
