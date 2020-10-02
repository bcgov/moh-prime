import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { KeycloakService } from 'keycloak-angular';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { DeviceProviderComponent } from './device-provider.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentModule } from '@enrolment/enrolment.module';

describe('DeviceProviderComponent', () => {
  let component: DeviceProviderComponent;
  let fixture: ComponentFixture<DeviceProviderComponent>;

  beforeEach(async(() => {
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
            provide: AuthService,
            useClass: MockAuthService
          },
          {
            provide: EnrolmentService,
            useClass: MockEnrolmentService
          },
          KeycloakService
        ]
      }
    ).compileComponents();
  }));

  beforeEach(inject([EnrolmentFormStateService], (enrolmentFormStateService: EnrolmentFormStateService) => {
    fixture = TestBed.createComponent(DeviceProviderComponent);
    component = fixture.componentInstance;
    // Add the bound FormGroup to the component
    component.form = enrolmentFormStateService.buildDeviceProviderForm();
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
