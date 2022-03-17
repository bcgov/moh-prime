import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { EnrolleeAccessTermEnrolmentComponent } from './enrollee-access-term-enrolment.component';
import { APP_DI_CONFIG, APP_CONFIG } from 'app/app-config.module';
import { NgxBusyModule } from '@lib/modules/ngx-busy/ngx-busy.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('EnrolleeAccessTermEnrolmentComponent', () => {
  let component: EnrolleeAccessTermEnrolmentComponent;
  let fixture: ComponentFixture<EnrolleeAccessTermEnrolmentComponent>;

  let spyOnRouteWithin;
  let mockRoute;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        NgxBusyModule,
        NgxMaterialModule,
        HttpClientTestingModule,
        RouterTestingModule,
        AdjudicationModule
      ],
      declarations: [],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: AuthService,
          useClass: MockAuthService
        },
        KeycloakService
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeAccessTermEnrolmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
    spyOnRouteWithin = spyOn((component as any).routeUtils, 'routeWithin');
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('test onRoute()', () => {
    it('with a string only', () => {
      mockRoute = 'mockRoute';
      component.onRoute(mockRoute);

      expect(spyOnRouteWithin).toHaveBeenCalledWith(mockRoute);
    });

    it('with a (string | number)[]', () => {
      mockRoute = ['mockRoute', 1];
      component.onRoute(mockRoute);

      expect(spyOnRouteWithin).toHaveBeenCalledWith(mockRoute);
    });
  });
});
