import { waitForAsync, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { KeycloakService } from 'keycloak-angular';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';

import { CollegeCertificationFormComponent } from './college-certification-form.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { CollegeConfig } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { FormIconGroupComponent } from '@shared/components/form-icon-group/form-icon-group.component';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { MatDatepickerModule } from '@angular/material/datepicker';

describe('CollegeCertificationFormComponent', () => {
  let component: CollegeCertificationFormComponent;
  let fixture: ComponentFixture<CollegeCertificationFormComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        BrowserAnimationsModule,
        NgxContextualHelpModule,
        HttpClientTestingModule,
        RouterTestingModule,
        NgxMaterialModule,
        ReactiveFormsModule,
        MatDatepickerModule,
        NgxMaskDirective,
        NgxMaskPipe
      ],
      declarations: [
        CollegeCertificationFormComponent,
        FormIconGroupComponent
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
        EnrolmentFormStateService,
        KeycloakService,
        provideNgxMask()
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(inject(
    [EnrolmentFormStateService, ConfigService],
    (enrolmentFormStateService: EnrolmentFormStateService, configService: ConfigService
    ) => {
      fixture = TestBed.createComponent(CollegeCertificationFormComponent);
      component = fixture.componentInstance;
      // Add the bound FormGroup to the component
      component.form = enrolmentFormStateService.regulatoryFormState.buildCollegeCertificationForm();
      component.selectedColleges = configService.colleges.map((college: CollegeConfig) => college.code);
      fixture.detectChanges();
    }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
