import { TestBed } from '@angular/core/testing';

import { SiteRegistrationStateService } from './site-registration-state.service';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

describe('SiteRegistrationStateService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [
      ReactiveFormsModule,
      RouterTestingModule
    ]
  }));

  it('should be created', () => {
    const service: SiteRegistrationStateService = TestBed.inject(SiteRegistrationStateService);
    expect(service).toBeTruthy();
  });
});
