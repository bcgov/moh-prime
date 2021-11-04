import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { HealthAuthoritySiteFormStateService } from './health-authority-site-form-state.service';

describe('HealthAuthoritySiteFormStateService', () => {
  let service: HealthAuthoritySiteFormStateService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        RouterTestingModule
      ],
      providers: [

        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    });
    service = TestBed.inject(HealthAuthoritySiteFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
