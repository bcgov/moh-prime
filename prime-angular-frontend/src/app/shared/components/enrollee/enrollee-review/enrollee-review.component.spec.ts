import { waitForAsync, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { EnrolleeReviewComponent } from './enrollee-review.component';
import { SharedModule } from '@shared/shared.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';

describe('EnrolleeReviewComponent', () => {
  let component: EnrolleeReviewComponent;
  let fixture: ComponentFixture<EnrolleeReviewComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        SharedModule
      ],
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
        },
        {
          provide: EnrolmentService,
          useClass: MockEnrolmentService
        },
        KeycloakService
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(inject([EnrolmentService], (enrolmentService: EnrolmentService) => {
    fixture = TestBed.createComponent(EnrolleeReviewComponent);
    component = fixture.componentInstance;
    component.enrolment = enrolmentService.enrolment;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
