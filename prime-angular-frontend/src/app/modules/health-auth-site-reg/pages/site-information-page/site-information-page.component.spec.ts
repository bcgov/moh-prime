import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockPermissionService } from 'test/mocks/mock-permission.service';

import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { SiteInformationPageComponent } from './site-information-page.component';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

describe('SiteInformationPageComponent', () => {
  let component: SiteInformationPageComponent;
  let fixture: ComponentFixture<SiteInformationPageComponent>;
  let spyOnRouteRelativeTo;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [SiteInformationPageComponent],
      imports: [
        BrowserAnimationsModule,
        HttpClientTestingModule,
        RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule
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
        },
        KeycloakService,
        CapitalizePipe
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteInformationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    spyOnRouteRelativeTo = spyOn(component.routeUtils, 'routeRelativeTo');
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('testing onBack() with a completed profile', () => {
    it('should call routeToRelativePath with HealthAuthSiteRegRoutes.SITE_OVERVIEW', () => {
      component.isCompleted = true;

      component.onBack();
      expect(spyOnRouteRelativeTo).toHaveBeenCalledWith(HealthAuthSiteRegRoutes.SITE_OVERVIEW);
    });
  });

  describe('testing onBack() with an incomplete profile', () => {
    it('should call routeToRelativePath with HealthAuthSiteRegRoutes.VENDOR', () => {
      component.isCompleted = false;

      component.onBack();
      expect(spyOnRouteRelativeTo).toHaveBeenCalledWith(HealthAuthSiteRegRoutes.VENDOR);
    });
  });
});
