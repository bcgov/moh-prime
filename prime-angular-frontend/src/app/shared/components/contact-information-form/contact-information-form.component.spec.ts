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
        EnrolmentModule
      ],
      declarations: [
        ContactInformationFormComponent
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        EnrolmentFormStateService,
        KeycloakService
      ]
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

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
