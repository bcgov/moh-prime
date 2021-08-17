import { AdjudicationModule } from '@adjudication/adjudication.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AuthService } from '@auth/shared/services/auth.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { KeycloakService } from 'keycloak-angular';
import { MockAuthService } from 'test/mocks/mock-auth.service';

import { EnrolleeMaintenancePageComponent } from './enrollee-maintenance-page.component';

describe('EnrolleeMaintenancePageComponent', () => {
  let component: EnrolleeMaintenancePageComponent;
  let fixture: ComponentFixture<EnrolleeMaintenancePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
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
      ]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeMaintenancePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
