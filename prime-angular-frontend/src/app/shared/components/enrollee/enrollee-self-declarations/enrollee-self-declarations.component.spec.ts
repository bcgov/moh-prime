import { provideHttpClientTesting } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, inject, TestBed, waitForAsync } from '@angular/core/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { RouterTestingModule } from '@angular/router/testing';

import { EnrolleeSelfDeclarationsComponent } from './enrollee-self-declarations.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('EnrolleeSelfDeclarationsComponent', () => {
  let component: EnrolleeSelfDeclarationsComponent;
  let fixture: ComponentFixture<EnrolleeSelfDeclarationsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule,
        NgxMaterialModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        {
            provide: AuthService,
            useValue: MockAuthService
        },
        {
            provide: EnrolmentService,
            useClass: MockEnrolmentService
        },
        KeycloakService,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}).compileComponents();
  }));

  beforeEach(inject([EnrolmentService], (enrolmentService: EnrolmentService) => {
    fixture = TestBed.createComponent(EnrolleeSelfDeclarationsComponent);
    component = fixture.componentInstance;
    component.enrolment = enrolmentService.enrolment;
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
