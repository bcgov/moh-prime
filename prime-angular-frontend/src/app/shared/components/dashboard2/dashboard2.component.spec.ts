import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';

import { Dashboard2Component } from './dashboard2.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { AuthService } from '@auth/shared/services/auth.service';

describe('Dashboard2Component', () => {
  let component: Dashboard2Component;
  let fixture: ComponentFixture<Dashboard2Component>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [Dashboard2Component],
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
        KeycloakService
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(Dashboard2Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
