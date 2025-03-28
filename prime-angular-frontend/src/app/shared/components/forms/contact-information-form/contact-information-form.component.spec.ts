import { waitForAsync, ComponentFixture, inject, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { FormGroup, ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { KeycloakService } from 'keycloak-angular';

import { ContactInformationFormComponent } from './contact-information-form.component';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';

describe('ContactInformationComponent', () => {
  let component: ContactInformationFormComponent;
  let fixture: ComponentFixture<ContactInformationFormComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        ReactiveFormsModule,
        BrowserAnimationsModule,
        EnrolmentModule,
        NgxMaskDirective,
        NgxMaskPipe
      ],
      declarations: [
        ContactInformationFormComponent
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
        EnrolmentFormStateService,
        KeycloakService,
        provideNgxMask()
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(inject(
    [EnrolmentFormStateService],
    (enrolmentFormStateService: EnrolmentFormStateService) => {
      fixture = TestBed.createComponent(ContactInformationFormComponent);
      component = fixture.componentInstance;
      // Add the bound FormGroup to the component
      component.form = enrolmentFormStateService.bceidDemographicFormState.form;
      fixture.detectChanges();
    }
  ));

  // TODO Fix null form for test
  // it('should create', () => {
  //   expect(component).toBeTruthy();
  // });
});
