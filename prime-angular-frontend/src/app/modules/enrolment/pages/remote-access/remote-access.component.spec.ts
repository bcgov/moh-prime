import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { KeycloakService } from 'keycloak-angular';

import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';

import { RemoteAccessComponent } from './remote-access.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('RemoteAccessComponent', () => {
  let component: RemoteAccessComponent;
  let fixture: ComponentFixture<RemoteAccessComponent>;

  let enrolleeRemoteUser;

  let spyOnRouteTo;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        NgxMaterialModule,
        HttpClientTestingModule,
        ReactiveFormsModule
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
          provide: EnrolmentService,
          useClass: MockEnrolmentService
        },
        {
          provide: ConfigService,
          useClass: MockConfigService
        },
        KeycloakService
      ],
      declarations: [RemoteAccessComponent],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoteAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('testing nextRouteAfterSubmit', () => {
    beforeEach(() => {
      spyOnRouteTo = spyOn(component, 'routeTo');
      enrolleeRemoteUser = (component as any).enrolmentFormStateService.enrolleeRemoteUserFormGroup();
    });

    describe('with isProfileComplete set to true, and enrolleeRemoteUsers has no items', () => {
      it('should routeTo with EnrolmentRoutes.OVERVIEW', () => {
        (component as any).nextRouteAfterSubmit();

        expect(spyOnRouteTo).toHaveBeenCalledWith(EnrolmentRoutes.OVERVIEW);
      });
    });

    describe('with isProfileComplete set to true, and enrolleeRemoteUsers has items', () => {
      it('should routeTo with EnrolmentRoutes.REMOTE_ACCESS_ADDRESSES', () => {
        enrolleeRemoteUser.patchValue({
          enrolleeId: 1,
          remoteUserId: 1
        });
        component.enrolleeRemoteUsers.push(enrolleeRemoteUser);
        (component as any).nextRouteAfterSubmit();

        expect(spyOnRouteTo).toHaveBeenCalledWith(EnrolmentRoutes.REMOTE_ACCESS_ADDRESSES);
      });
    });

    describe('with isProfileComplete set to false, and enrolleeRemoteUsers has no items', () => {
      it('should routeTo with EnrolmentRoutes.SELF_DECLARATION', () => {
        component.isProfileComplete = false;

        (component as any).nextRouteAfterSubmit();

        expect(spyOnRouteTo).toHaveBeenCalledWith(EnrolmentRoutes.SELF_DECLARATION);
      });
    });

    describe('with isProfileComplete set to false, and enrolleeRemoteUsers has items', () => {
      it('should routeTo with EnrolmentRoutes.REMOTE_ACCESS_ADDRESSES', () => {
        component.isProfileComplete = false;
        enrolleeRemoteUser.patchValue({
          enrolleeId: 1,
          remoteUserId: 1
        });
        component.enrolleeRemoteUsers.push(enrolleeRemoteUser);
        (component as any).nextRouteAfterSubmit();

        expect(spyOnRouteTo).toHaveBeenCalledWith(EnrolmentRoutes.REMOTE_ACCESS_ADDRESSES);
      });
    });
  });
});
