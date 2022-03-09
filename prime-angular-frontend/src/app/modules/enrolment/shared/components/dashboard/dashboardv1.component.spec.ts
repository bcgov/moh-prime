import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';

import { MockAuthService } from 'test/mocks/mock-auth.service';

import { DashboardV1Component } from './dashboardv1.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { NgxProgressModule } from '@lib/modules/ngx-progress/ngx-progress.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { HeaderComponent } from '../header/header.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { KeycloakService } from 'keycloak-angular';
import { AccessTokenService } from '@auth/shared/services/access-token.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { MockAccessTokenService } from 'test/mocks/mock-access-token.service';
import { MockPermissionService } from 'test/mocks/mock-permission.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('DashboardComponent', () => {
  let component: DashboardV1Component;
  let fixture: ComponentFixture<DashboardV1Component>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        imports: [
          BrowserAnimationsModule,
          HttpClientTestingModule,
          RouterTestingModule,
          NgxMaterialModule,
          NgxProgressModule
        ],
        declarations: [
          DashboardV1Component,
          HeaderComponent
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
            provide: PermissionService,
            useClass: MockPermissionService
          },
          {
            provide: AccessTokenService,
            useClass: MockAccessTokenService
          },
          KeycloakService
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardV1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
