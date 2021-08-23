import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { OrganizationFormStateService } from './organization-form-state.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('OrganizationFormStateService', () => {
  let service: OrganizationFormStateService;

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
    service = TestBed.inject(OrganizationFormStateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
