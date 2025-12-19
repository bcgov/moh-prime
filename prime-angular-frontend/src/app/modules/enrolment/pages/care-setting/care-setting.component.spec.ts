import { waitForAsync, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { UntypedFormGroup, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { CareSettingComponent } from './care-setting.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('CareSettingComponent', () => {
  let component: CareSettingComponent;
  let fixture: ComponentFixture<CareSettingComponent>;
  let spyOnRouteTo;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          BrowserAnimationsModule,
          HttpClientTestingModule,
          RouterTestingModule,
          NgxBusyModule,
          NgxContextualHelpModule,
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
          KeycloakService
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
      }
    ).compileComponents();
  }));


  beforeEach(() => {
    fixture = TestBed.createComponent(CareSettingComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('testing addCareSetting()', () => {
    it('should add one careSetting', () => {
      expect(component.careSettings.length).toEqual(0);
      component.addCareSetting();
      expect(component.careSettings.length).toEqual(1);
    });
  });

  describe('testing removeCareSetting()', () => {
    it('should remove one careSetting', () => {
      component.addCareSetting();
      expect(component.careSettings.length).toEqual(1);
      component.removeCareSetting(0);
      expect(component.careSettings.length).toEqual(0);
    });
  });

  describe('testing filterCareSettingTypes()', () => {
    describe('without adding a care setting', () => {
      it('should return a list of all care setting types', () => {
        const mockCareTypeFormGroup = (component as any).enrolmentFormStateService.buildCareSettingForm();

        expect(component.filterCareSettingTypes(mockCareTypeFormGroup).length).toEqual(component.careSettingTypes.length);
      });
    });

    describe('with adding a care setting but without making a selection', () => {
      it('should return a list shorter than the list of all care setting types', () => {
        const mockCareTypeFormGroup = (component as any).enrolmentFormStateService.buildCareSettingForm() as UntypedFormGroup;
        component.addCareSetting();
        component.careSettings.controls[0].setValue({ careSettingCode: CareSettingEnum.COMMUNITY_PHARMACY });

        expect(component.filterCareSettingTypes(mockCareTypeFormGroup).length).toBeLessThan(component.careSettingTypes.length);
      });
    });

    describe('with adding one care setting and making one selection', () => {
      it('should return a list shorter than the list of all care setting types', () => {
        const mockCareTypeFormGroup = (component as any).enrolmentFormStateService.buildCareSettingForm() as UntypedFormGroup;
        mockCareTypeFormGroup.setValue({ careSettingCode: CareSettingEnum.COMMUNITY_PHARMACY });
        component.addCareSetting();
        component.careSettings.controls[0].setValue({ careSettingCode: CareSettingEnum.COMMUNITY_PHARMACY });

        expect(component.filterCareSettingTypes(mockCareTypeFormGroup).length).toEqual(component.careSettingTypes.length);
      });
    });
  });

  describe('testing hasSelectedHACareSetting', () => {
    describe('with Health Authority selected', () => {
      it('hasSelectedHACareSetting should return true', () => {
        component.addCareSetting();
        component.careSettings.controls[0].setValue({ careSettingCode: CareSettingEnum.HEALTH_AUTHORITY });

        expect(component.hasSelectedHACareSetting()).toBeTrue();
      });
    });

    describe('with care setting other than Health Authority selected', () => {
      it('hasSelectedHACareSetting should return false', () => {
        component.addCareSetting();
        component.careSettings.controls[0].setValue({ careSettingCode: CareSettingEnum.COMMUNITY_PHARMACY });

        expect(component.hasSelectedHACareSetting()).toBeFalse();
      });
    });
  });

  describe('testing nextRouteAfterSubmit()', () => {
    beforeEach(() => {
      spyOnRouteTo = spyOn<any>(component, 'routeTo');
    });

    describe('with profile complete', () => {
      it('should call routeTo with the path EnrolmentRoutes.OVERVIEW', () => {
        (component as any).nextRouteAfterSubmit();

        expect(spyOnRouteTo).toHaveBeenCalledWith(EnrolmentRoutes.OVERVIEW);
      });
    });

    describe('with profile not complete', () => {
      it('should call routeTo with the path EnrolmentRoutes.REGULATORY', () => {
        component.isProfileComplete = false;
        (component as any).nextRouteAfterSubmit();

        expect(spyOnRouteTo).toHaveBeenCalledWith(EnrolmentRoutes.REGULATORY);
      });
    });
  });

  describe('testing removeIncompleteCareSettings()', () => { });

  describe('testing removeOboSites()', () => { });

});
