import { waitForAsync, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { KeycloakService } from 'keycloak-angular';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { OboSitesPageComponent } from './obo-sites-page.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxContextualHelpModule } from '@lib/modules/ngx-contextual-help/ngx-contextual-help.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentModule } from '@enrolment/enrolment.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { AccessTokenService } from '@auth/shared/services/access-token.service';
import { MockAccessTokenService } from 'test/mocks/mock-access-token.service';

describe('OboSitesPageComponent', () => {
  let component: OboSitesPageComponent;
  let fixture: ComponentFixture<OboSitesPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
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
        {
          provde: AccessTokenService,
          useClass: MockAccessTokenService
        },
        EnrolmentFormStateService,
        KeycloakService
      ]
    })
      .compileComponents();
  }));

  beforeEach(inject([EnrolmentFormStateService], (enrolmentFormStateService: EnrolmentFormStateService) => {
    fixture = TestBed.createComponent(OboSitesPageComponent);
    component = fixture.componentInstance;
    // Add the bound FormGroup to the component
    component.form = enrolmentFormStateService.buildJobsForm();
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
