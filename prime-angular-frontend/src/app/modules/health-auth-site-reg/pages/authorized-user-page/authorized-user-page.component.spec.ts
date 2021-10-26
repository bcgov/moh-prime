import { ComponentFixture, TestBed, inject } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { KeycloakService } from 'keycloak-angular';

import { MockAuthService } from 'test/mocks/mock-auth.service';
import { MockConfigService } from 'test/mocks/mock-config.service';
import { MockOrganizationService } from 'test/mocks/mock-organization.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ConfigService } from '@config/config.service';
import { NgxMaterialModule } from '@lib/modules/ngx-material/ngx-material.module';
import { CapitalizePipe } from '@shared/pipes/capitalize.pipe';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { AuthorizedUserPageComponent } from './authorized-user-page.component';

fdescribe('AuthorizedUserPageComponent', () => {
  let component: AuthorizedUserPageComponent;
  let fixture: ComponentFixture<AuthorizedUserPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        ReactiveFormsModule,
        NgxMaterialModule,
        BrowserAnimationsModule
      ],
      declarations: [
        AuthorizedUserPageComponent
      ],
      providers: [
        KeycloakService,
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
          provide: OrganizationService,
          useClass: MockOrganizationService
        },
        OrganizationFormStateService,
        CapitalizePipe
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  });

  beforeEach(inject(
    [OrganizationService, OrganizationFormStateService],
    (organizationService: OrganizationService, organizationFormStateService: OrganizationFormStateService) => {
      fixture = TestBed.createComponent(AuthorizedUserPageComponent);
      component = fixture.componentInstance;
      organizationFormStateService.setForm(organizationService.organization);
      fixture.detectChanges();
    })
  );

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
