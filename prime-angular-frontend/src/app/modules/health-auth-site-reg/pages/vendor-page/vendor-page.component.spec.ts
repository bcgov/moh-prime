import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { ActivatedRoute } from '@angular/router';

import { KeycloakService } from 'keycloak-angular';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { VendorPageComponent } from './vendor-page.component';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

describe('VendorPageComponent', () => {
  const mockActivatedRoute = {
    snapshot: {
      data: {
        title: 'Health Authority Care Type',
      },
      params: {
        haid: 1,
        sid: 7
      }
    }
  };

  let component: VendorPageComponent;
  let fixture: ComponentFixture<VendorPageComponent>;
  let spyOnRouteRelativeTo;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule
      ],
      declarations: [
        VendorPageComponent
      ],
      providers: [
        KeycloakService,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: ConfigService,
          useClass: MockConfigService
        },
        {
          provide: ActivatedRoute,
          useValue: mockActivatedRoute
        },
        CapitalizePipe
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorPageComponent);
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
    it('should call routeToRelativePath with HealthAuthSiteRegRoutes.HEALTH_AUTH_CARE_TYPE', () => {
      component.isCompleted = false;

      component.onBack();
      expect(spyOnRouteRelativeTo).toHaveBeenCalledWith(HealthAuthSiteRegRoutes.HEALTH_AUTH_CARE_TYPE);
    });
  });
});
