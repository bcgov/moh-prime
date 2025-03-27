import { waitForAsync, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { UntypedFormBuilder, ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { KeycloakService } from 'keycloak-angular';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { BcscDemographicComponent } from './bcsc-demographic.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { BcscDemographicFormState } from './bcsc-demographic-form-state.class';
import { AddressFormComponent } from '@shared/components/forms/address-form/address-form.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('BcscDemographicComponent', () => {
  let component: BcscDemographicComponent;
  let fixture: ComponentFixture<BcscDemographicComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          BrowserAnimationsModule,
          HttpClientTestingModule,
          NgxBusyModule,
          NgxContextualHelpModule,
          NgxMaterialModule,
          ReactiveFormsModule,
          RouterTestingModule,
          NgxMaskDirective,
          NgxMaskPipe
        ],
        declarations: [BcscDemographicComponent, AddressFormComponent],
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
          KeycloakService,
          provideNgxMask()
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
      }
    ).compileComponents();
  }));

  beforeEach(inject(
    [UntypedFormBuilder, FormUtilsService],
    (fb: UntypedFormBuilder, formUtilsService: FormUtilsService) => {
      fixture = TestBed.createComponent(BcscDemographicComponent);
      component = fixture.componentInstance;
      const bcscDemographicFormState = new BcscDemographicFormState(fb, formUtilsService);
      component.form = bcscDemographicFormState.form;
      fixture.detectChanges();
    }
  ));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
