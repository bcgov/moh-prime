
import { ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { KeycloakService } from 'keycloak-angular';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';

import { UnlistedCollegeLicenceFormComponent } from './unlisted-college-licence-form.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { RegulatoryFormState } from '@paper-enrolment/pages/regulatory-page/regulatory-form-state.class';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { MatDatepickerModule } from '@angular/material/datepicker';

describe('UnlistedCollegeLicenceFormComponent', () => {
  let component: UnlistedCollegeLicenceFormComponent;
  let fixture: ComponentFixture<UnlistedCollegeLicenceFormComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
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
      declarations: [UnlistedCollegeLicenceFormComponent],
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
        RegulatoryFormState,
        KeycloakService,
        provideNgxMask()
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  });

  beforeEach(inject(
    [RegulatoryFormState],
    (
      regulatoryFormState: RegulatoryFormState
    ) => {
      fixture = TestBed.createComponent(UnlistedCollegeLicenceFormComponent);
      component = fixture.componentInstance;
      component.formState = regulatoryFormState;
      component.form = regulatoryFormState.buildUnlistedCollegeCertificationForm();
      fixture.detectChanges();
    }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
