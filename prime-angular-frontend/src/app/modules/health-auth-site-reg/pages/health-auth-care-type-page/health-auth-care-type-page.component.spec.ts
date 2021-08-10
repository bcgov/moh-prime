import { HttpClientTestingModule } from '@angular/common/http/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockPermissionService } from 'test/mocks/mock-permission.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';

import { HealthAuthCareTypePageComponent } from './health-auth-care-type-page.component';

describe('HealthAuthCareTypePageComponent', () => {
  let component: HealthAuthCareTypePageComponent;
  let fixture: ComponentFixture<HealthAuthCareTypePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        // BrowserAnimationsModule,
        // HttpClientTestingModule,
        // RouterTestingModule,
        // ReactiveFormsModule,
        // NgxMaterialModule
      ],
      declarations: [
        HealthAuthCareTypePageComponent
      ],
      providers: [
        // KeycloakService,
        // {
        //   provide: APP_CONFIG,
        //   useValue: APP_DI_CONFIG
        // },
        // {
        //   provide: ConfigService,
        //   useClass: MockConfigService
        // },
        // {
        //   provide: AuthService,
        //   useClass: MockAuthService
        // },
        // {
        //   provide: PermissionService,
        //   useClass: MockPermissionService
        // }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthCareTypePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
