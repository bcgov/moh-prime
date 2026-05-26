import { AdjudicationModule } from '@adjudication/adjudication.module';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthService } from '@auth/shared/services/auth.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { KeycloakService } from 'keycloak-angular';
import { MockAuthService } from 'test/mocks/mock-auth.service';

import { EnrolleeMaintenancePageComponent } from './enrollee-maintenance-page.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('EnrolleeMaintenancePageComponent', () => {
  let component: EnrolleeMaintenancePageComponent;
  let fixture: ComponentFixture<EnrolleeMaintenancePageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule,
        AdjudicationModule],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        {
            provide: AuthService,
            useClass: MockAuthService
        },
        KeycloakService,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
})
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeMaintenancePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('testing onBack()', () => {
    it('should call routeUtis with \'../\'', () => {
      const spyOnRouteRelativeTo = spyOn((component as any).routeUtils, 'routeRelativeTo');

      component.onBack();
      expect(spyOnRouteRelativeTo).toHaveBeenCalledOnceWith(['../']);
    });
  });
});
