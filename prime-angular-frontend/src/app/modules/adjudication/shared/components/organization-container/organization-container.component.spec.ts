import { ComponentFixture, TestBed } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { OrganizationContainerComponent } from './organization-container.component';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AuthService } from '@auth/shared/services/auth.service';
import { MockAuthService } from 'test/mocks/mock-auth.service';
import { KeycloakService } from 'keycloak-angular';
import { ConfigService } from '@config/config.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { PermissionService } from '@auth/shared/services/permission.service';
import { MockPermissionService } from 'test/mocks/mock-permission.service';
import { AccessTokenService } from '@auth/shared/services/access-token.service';
import { MockAccessTokenService } from 'test/mocks/mock-access-token.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('OrganizationContainerComponent', () => {
  let component: OrganizationContainerComponent;
  let fixture: ComponentFixture<OrganizationContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
    declarations: [OrganizationContainerComponent],
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [BrowserAnimationsModule,
        RouterTestingModule,
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
        {
            provide: ConfigService,
            useClass: MockConfigService
        },
        {
            provide: PermissionService,
            useClass: MockPermissionService
        },
        {
            provide: AccessTokenService,
            useClass: MockAccessTokenService
        },
        KeycloakService,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
})
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OrganizationContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
