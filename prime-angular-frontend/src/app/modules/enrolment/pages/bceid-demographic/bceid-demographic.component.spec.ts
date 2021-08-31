import { waitForAsync, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { FormBuilder, ReactiveFormsModule } from '@angular/forms';
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

describe('BceidDemographicComponent', () => {
  let component: BceidDemographicComponent;
  let fixture: ComponentFixture<BceidDemographicComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        HttpClientTestingModule,
        RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule
      ],
      declarations: [
        BceidDemographicComponent
      ],
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
        EnrolmentFormStateService
      ]
    }).compileComponents();
  }));

  beforeEach(inject(
    [FormBuilder, FormUtilsService],
    (fb: FormBuilder, formUtilsService: FormUtilsService) => {
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
