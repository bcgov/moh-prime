import { waitForAsync, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';

import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { KeycloakService } from 'keycloak-angular';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { EnrolmentRegulatoryForm } from './enrolment-regulatory-form.model';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';

import { RegulatoryComponent } from './regulatory.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { EnrolleeDeviceProvider } from '@shared/models/enrollee-device-provider.model';
import { AccessTokenService } from '@auth/shared/services/access-token.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { MockAccessTokenService } from 'test/mocks/mock-access-token.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { RegulatoryFormState } from './regulatory-form-state';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { CareSettingForm } from '@paper-enrolment/pages/care-setting-page/care-setting-form.model';

describe('RegulatoryComponent', () => {
  let component: RegulatoryComponent;
  let fixture: ComponentFixture<RegulatoryComponent>;

  let spyOnRouteTo;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          BrowserAnimationsModule,
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
          KeycloakService,
          provideNgxMask()
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
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

  describe('testing addEmptyCollegeCertification()', () => {
    it('should add 1 empty college license', () => {
      expect(component.selectedCollegeCodes.length).toEqual(0);

      component.addEmptyCollegeCertification();

      expect(component.selectedCollegeCodes.length).toEqual(1);
    });
  });

  describe('testing removeCertification()', () => {
    it('should remove 1 college license', () => {
      component.addEmptyCollegeCertification();

      expect(component.selectedCollegeCodes.length).toEqual(1);

      component.removeCertification(0);

      expect(component.selectedCollegeCodes.length).toEqual(0);
    });
  });

  describe('testing nextRouteAfterSubmit()', () => {
    beforeEach(() => spyOnRouteTo = spyOn(component, 'routeTo'));

    describe('with profileComplete set to true', () => {
      it('routeTo should be called with EnrolmentRoutes.OVERVIEW', () => {
        (component as any).nextRouteAfterSubmit();
        expect(spyOnRouteTo).toHaveBeenCalledWith(EnrolmentRoutes.OVERVIEW);
      });
    });

    describe('with profileComplete set to false, and no certifications', () => {
      it('routeTo should be called with EnrolmentRoutes.OBO_SITES', () => {
        component.isProfileComplete = false;

        (component as any).nextRouteAfterSubmit();
        expect(spyOnRouteTo).toHaveBeenCalledWith(EnrolmentRoutes.OBO_SITES);
      });
    });

    describe('with profileComplete set to false, with certifications', () => {
      describe('with isDeviceProvider set to true but no deviceProviderIdentifier', () => {
        it('routeTo should be called with EnrolmentRoutes.OBO_SITES', () => {
          component.isProfileComplete = false;
          component.isDeviceProvider = true;
          component.addEmptyCollegeCertification();

          (component as any).nextRouteAfterSubmit();
          expect(spyOnRouteTo).toHaveBeenCalledWith(EnrolmentRoutes.OBO_SITES);
        });
      });

      describe('with isDeviceProvider set to false and with deviceProviderIdentifier, and canRequestRemoteAccess', () => {
        it('routeTo should be called with EnrolmentRoutes.REMOTE_ACCESS', () => {
          const mockRegulatoryForm = {
            certifications: [{
              collegeCode: 1,
              licenseCode: 1,
              licenseNumber: "12345"
            }],
            enrolleeDeviceProviders: [],
            unlistedCertifications: [],
          } as EnrolmentRegulatoryForm;

          component.formState.patchValue(mockRegulatoryForm);
          component.isProfileComplete = false;
          component.isDeviceProvider = false;
          component.hasMatchingRemoteUser = true;

          (component as any).nextRouteAfterSubmit();

          expect(spyOnRouteTo).toHaveBeenCalledWith(EnrolmentRoutes.REMOTE_ACCESS);
        });
      });

      describe('with isDeviceProvider set to false and canRequestRemoteAccess returning false', () => {
        it('routeTo should be called with EnrolmentRoutes.SELF_DECLARATION', () => {
          const mockRegulatoryForm = {
            certifications: [{
              collegeCode: 1,
              licenseCode: 1,
              licenseNumber: "12345"
            }],
            enrolleeDeviceProviders: [],
            unlistedCertifications: [],
          } as EnrolmentRegulatoryForm;

          component.formState.patchValue(mockRegulatoryForm);
          component.isProfileComplete = false;
          component.isDeviceProvider = false;
          component.hasMatchingRemoteUser = false;
          component.addEmptyCollegeCertification();

          (component as any).nextRouteAfterSubmit();

          expect(spyOnRouteTo).toHaveBeenCalledWith(EnrolmentRoutes.SELF_DECLARATION);
        });
      });
    });
  });

  describe('testing removeIncompleteCertifications()', () => {
    describe('with noEmptyCert set to default value (false)', () => {
      it('should have one certificate', () => {
        component.addEmptyCollegeCertification();

        expect(component.selectedCollegeCodes.length).toEqual(1);

        (component as any).removeIncompleteCertifications();

        expect(component.selectedCollegeCodes.length).toEqual(1);
      });

    });

    describe('with noEmptyCert set to true', () => {
      it('should have no certificates', () => {
        component.addEmptyCollegeCertification();

        expect(component.selectedCollegeCodes.length).toEqual(1);

        (component as any).removeIncompleteCertifications(true);

        expect(component.selectedCollegeCodes.length).toEqual(0);
      });
    });
  });
});
