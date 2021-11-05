import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, inject, TestBed, waitForAsync } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';

import { ConfigService } from '@config/config.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { AuthService } from '@auth/shared/services/auth.service';

import { AccessCodeFormComponent } from './access-code-form.component';

describe('AccessCodeFormComponent', () => {
  let component: AccessCodeFormComponent;
  let fixture: ComponentFixture<AccessCodeFormComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
        RouterTestingModule,
        HttpClientTestingModule,
        MatSnackBarModule
      ],
      declarations: [AccessCodeFormComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ConfigService,
          useClass: MockConfigService
        },
        {
          provide: AuthService,
          useClass: MockAuthService
        }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(inject([EnrolmentFormStateService], (enrolmentFormStateService: EnrolmentFormStateService) => {
    fixture = TestBed.createComponent(AccessCodeFormComponent);
    component = fixture.componentInstance;
    component.form = enrolmentFormStateService.accessForm;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
