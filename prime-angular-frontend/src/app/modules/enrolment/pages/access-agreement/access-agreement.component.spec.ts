import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { AccessAgreementComponent } from './access-agreement.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { AccessTokenService } from '@auth/shared/services/access-token.service';
import { MockAccessTokenService } from 'test/mocks/mock-access-token.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('AccessAgreementComponent', () => {
  let component: AccessAgreementComponent;
  let fixture: ComponentFixture<AccessAgreementComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          HttpClientTestingModule,
          RouterTestingModule,
          EnrolmentModule
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
            provide: EnrolmentService,
            useClass: MockEnrolmentService
          },
          {
            provide: AuthService,
            useClass: MockAuthService
          },
          {
            provde: AccessTokenService,
            useClass: MockAccessTokenService
          },
          KeycloakService
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AccessAgreementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
