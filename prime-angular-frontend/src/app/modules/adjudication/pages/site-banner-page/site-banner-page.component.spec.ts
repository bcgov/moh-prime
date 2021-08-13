import { ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { NgxMaskModule } from 'ngx-mask';
import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockPermissionService } from 'test/mocks/mock-permission.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { SiteBannerPageComponent } from './site-banner-page.component';

describe('SiteBannerPageComponent', () => {
  let component: SiteBannerPageComponent;
  let fixture: ComponentFixture<SiteBannerPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        ReactiveFormsModule,
        RouterTestingModule,
        NgxMaskModule.forRoot(),
        NgxMaterialModule,
        BrowserAnimationsModule
      ],
      providers: [
        KeycloakService,
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
        }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteBannerPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
