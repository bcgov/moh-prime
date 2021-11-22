import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ActivatedRoute } from '@angular/router';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { ConfigService } from '@config/config.service';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { HealthAuthCareTypePageComponent } from './health-auth-care-type-page.component';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

describe('HealthAuthCareTypePageComponent', () => {
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

  let component: HealthAuthCareTypePageComponent;
  let fixture: ComponentFixture<HealthAuthCareTypePageComponent>;
  let spyOnRouteRelativeTo;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule,
        BrowserAnimationsModule
      ],
      declarations: [
        HealthAuthCareTypePageComponent
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
          provide: ActivatedRoute,
          useValue: mockActivatedRoute
        },
        CapitalizePipe
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthCareTypePageComponent);
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
    it('should call routeToRelativePath with HealthAuthSiteRegRoutes.SITE_INFORMATION', () => {
      component.isCompleted = false;

      component.onBack();
      expect(spyOnRouteRelativeTo).toHaveBeenCalledWith(HealthAuthSiteRegRoutes.SITE_INFORMATION);
    });
  });
});
