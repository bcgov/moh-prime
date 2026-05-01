import { waitForAsync, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { ReactiveFormsModule, UntypedFormGroup } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';

import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';
import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';

import { AddressFormComponent } from './address-form.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('AddressFormComponent', () => {
  let component: AddressFormComponent;
  let fixture: ComponentFixture<AddressFormComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
    declarations: [
        AddressFormComponent
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [BrowserAnimationsModule,
        RouterTestingModule,
        NgxMaterialModule,
        ReactiveFormsModule,
        NgxMaskDirective,
        NgxMaskPipe],
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
            useValue: MockAuthService
        },
        EnrolmentFormStateService,
        KeycloakService,
        provideNgxMask(),
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}
    ).compileComponents();
  }));

  beforeEach(inject([EnrolmentFormStateService], (enrolmentFormStateService: EnrolmentFormStateService) => {
    fixture = TestBed.createComponent(AddressFormComponent);
    component = fixture.componentInstance;
    // Add the bound FormGroup to the component
    component.form = enrolmentFormStateService.bcscDemographicFormState.form.get('mailingAddress') as UntypedFormGroup;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
