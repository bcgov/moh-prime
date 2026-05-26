import { waitForAsync, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { UntypedFormBuilder, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { BceidDemographicComponent } from './bceid-demographic.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { BceidDemographicFormState } from './bceid-demographic-form-state.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('BceidDemographicComponent', () => {
  let component: BceidDemographicComponent;
  let fixture: ComponentFixture<BceidDemographicComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        BceidDemographicComponent
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [BrowserAnimationsModule,
        RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule,
        MatDatepickerModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        {
            provide: AuthService,
            useClass: MockAuthService
        },
        {
            provide: ConfigService,
            useClass: MockConfigService
        },
        KeycloakService,
        EnrolmentFormStateService,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}).compileComponents();
  }));

  beforeEach(inject(
    [UntypedFormBuilder, FormUtilsService],
    (fb: UntypedFormBuilder, formUtilsService: FormUtilsService) => {
      fixture = TestBed.createComponent(BceidDemographicComponent);
      component = fixture.componentInstance;
      const bceidDemographicFormState = new BceidDemographicFormState(fb, formUtilsService);
      component.form = bceidDemographicFormState.form;
      fixture.detectChanges();
    }
  ));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
