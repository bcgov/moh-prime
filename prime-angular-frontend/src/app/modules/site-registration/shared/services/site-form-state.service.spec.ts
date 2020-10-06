import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { SiteFormStateService } from './site-form-state.service';

describe('SiteFormStateService', () => {
  let service: SiteFormStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        RouterTestingModule
      ]
    });
    service = TestBed.inject(SiteFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
