import { waitForAsync, ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';

import * as faker from 'faker';
import { KeycloakService } from 'keycloak-angular';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';

import { AccessAgreementHistoryEnrolmentComponent } from './access-agreement-history-enrolment.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { ConfigService } from '@config/config.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

describe('AccessAgreementHistoryEnrolmentComponent', () => {
  let component: AccessAgreementHistoryEnrolmentComponent;
  let fixture: ComponentFixture<AccessAgreementHistoryEnrolmentComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        NgxBusyModule,
        HttpClientTestingModule,
        RouterTestingModule,
        NgxMaterialModule,
        ReactiveFormsModule
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
        KeycloakService
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(inject([EnrolmentService], (enrolmentService: EnrolmentService) => {
    fixture = TestBed.createComponent(AccessAgreementHistoryEnrolmentComponent);
    component = fixture.componentInstance;
    component.enrolmentSubmission = {
      id: faker.random.number(),
      enrolleeId: faker.random.number(),
      profileSnapshot: enrolmentService.enrolment,
      agreementType: faker.random.number({ min: 1, max: 6 }),
      createdDate: faker.date.past(1).toDateString()
    };
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
