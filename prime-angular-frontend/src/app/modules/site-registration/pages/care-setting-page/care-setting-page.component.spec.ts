import { MockPermissionService } from '../../../../../test/mocks/mock-permission.service';
import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { MockConfigService } from 'test/mocks/mock-config.service';

import { CareSettingPageComponent } from './care-setting-page.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { KeycloakService } from 'keycloak-angular';
import { AuthService } from '@auth/shared/services/auth.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('CareSettingComponent', () => {
  let component: CareSettingPageComponent;
  let fixture: ComponentFixture<CareSettingPageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    declarations: [
        CareSettingPageComponent
    ],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [BrowserAnimationsModule,
        RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule],
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
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
}).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CareSettingPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
