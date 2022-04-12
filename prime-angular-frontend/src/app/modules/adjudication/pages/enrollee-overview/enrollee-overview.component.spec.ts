import faker from 'faker';

import { AdjudicationContainerComponent } from '@adjudication/shared/components/adjudication-container/adjudication-container.component';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { SharedModule } from '@shared/shared.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { KeycloakService } from 'keycloak-angular';
import { RouteUtils } from '@lib/utils/route-utils.class';

import { EnrolleeOverviewComponent } from './enrollee-overview.component';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

describe('EnrolleeOverviewComponent', () => {
  let component: EnrolleeOverviewComponent;
  let fixture: ComponentFixture<EnrolleeOverviewComponent>;

  let mockEnrolleeId;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        RouterTestingModule,
        HttpClientTestingModule,
        NgxMaterialModule,
        SharedModule
      ],
      declarations: [],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        KeycloakService
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    mockEnrolleeId = faker.random.number();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('testing onNavigateEnrollee()', () => {
    it('should call onRoute with passed in param and current route', () => {
      const spyOnOnRoute = spyOn(AdjudicationContainerComponent.prototype, 'onRoute');

      component.onNavigateEnrollee(mockEnrolleeId);
      expect(spyOnOnRoute).toHaveBeenCalledOnceWith([
        mockEnrolleeId,
        RouteUtils.currentRoutePath((component as any).router.url)
      ]);
    });
  });

  describe('testing onRedirectCommunitySite()', () => {
    it('should call router.navigate() with the adjudication MODULE_PATH, SITE_REGISTRATIONS and the remainder of the path', () => {
      const spyOnRouterNavigate = spyOn((component as any).router, 'navigate');
      const mockRoutePath = faker.random.word();

      component.onRedirectCommunitySite(mockRoutePath);
      expect(spyOnRouterNavigate).toHaveBeenCalledOnceWith([
        AdjudicationRoutes.MODULE_PATH,
        AdjudicationRoutes.SITE_REGISTRATIONS,
        ...mockRoutePath
      ]);
    });
  });
});
