import { waitForAsync, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';

import { NgxMaskModule } from 'ngx-mask';
import { KeycloakService } from 'keycloak-angular';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';

import { RegulatoryComponent } from './regulatory.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { AccessTokenService } from '@auth/shared/services/access-token.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { MockAccessTokenService } from 'test/mocks/mock-access-token.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';

describe('RegulatoryComponent', () => {
  let component: RegulatoryComponent;
  let fixture: ComponentFixture<RegulatoryComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          BrowserAnimationsModule,
          HttpClientTestingModule,
          RouterTestingModule,
          NgxBusyModule,
          NgxContextualHelpModule,
          NgxMaskModule.forRoot(),
          NgxMaterialModule,
          ReactiveFormsModule,
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
            provide: AccessTokenService,
            useClass: MockAccessTokenService
          },
          KeycloakService
        ]
      }
    ).compileComponents();
  }));

  beforeEach(inject([EnrolmentFormStateService], (enrolmentFormStateService: EnrolmentFormStateService) => {
    fixture = TestBed.createComponent(RegulatoryComponent);
    component = fixture.componentInstance;
    // Add the bound FormGroup to the component
    component.form = enrolmentFormStateService.regulatoryFormState.form;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('testing selectedCollegeCodes()', () => {
    describe('with empty certifications array', () => {
      it('should return an empty array', () => {
        expect(component.selectedCollegeCodes.length).toEqual(0);
      });
    });

    describe('with certifications added', () => {
      it('should return a non-empty array', () => {
        component.addEmptyCollegeCertification();

        expect(component.selectedCollegeCodes.length).toBeGreaterThan(0);
      });
    });
  });

  describe('testing addEmptyCollegeCertification()', () => { });
  describe('testing removeCertification()', () => { });
  describe('testing nextRouteAfterSubmit()', () => { });
  describe('testing removeIncompleteCertifications()', () => { });
  describe('testing removeOboSites()', () => { });
  describe('testing canRequestRemoteAccess()', () => { });
  describe('testing removeRemoteAccessData()', () => { });
  describe('testing toggleDeviceProviderValidator()', () => { });
});
