import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockPermissionService } from 'test/mocks/mock-permission.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { ConfigService } from '@config/config.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { EmailNotificationViewPageComponent } from './email-notification-view-page.component';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

describe('EmailNotificationViewPageComponent', () => {
  let component: EmailNotificationViewPageComponent;
  let fixture: ComponentFixture<EmailNotificationViewPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule,
        BrowserAnimationsModule
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
          provide: PermissionService,
          useClass: MockPermissionService
        }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailNotificationViewPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('testing onBack()', () => {
    it('should navigate with \'../\' and AdjudicationRoutes.NOTIFICATION_EMAILS', () => {
      const spyOnRouteRelativeTo = spyOn((component as any).routeUtils, 'routeRelativeTo');

      component.onBack();
      expect(spyOnRouteRelativeTo).toHaveBeenCalledTimes(1);
      expect(spyOnRouteRelativeTo).toHaveBeenCalledOnceWith(['../', AdjudicationRoutes.NOTIFICATION_EMAILS]);
    });
  });
});
