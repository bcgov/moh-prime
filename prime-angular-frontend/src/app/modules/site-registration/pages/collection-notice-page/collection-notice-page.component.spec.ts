import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockOrganizationService } from 'test/mocks/mock-organization.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { SiteRoutes } from '@registration/site-registration.routes';
import { OrganizationSigningAuthorityPageComponent } from '@registration/pages/organization-signing-authority-page/organization-signing-authority-page.component';
import { CollectionNoticePageComponent } from './collection-notice-page.component';

describe('CollectionNoticePageComponent', () => {
  let component: CollectionNoticePageComponent;
  let fixture: ComponentFixture<CollectionNoticePageComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        SiteRegistrationModule,
        RouterTestingModule.withRoutes([
          {
            path: SiteRoutes.ORGANIZATIONS,
            children: [
              {
                path: ':oid',
                children: [
                  {
                    path: SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY,
                    component: OrganizationSigningAuthorityPageComponent
                  }
                ]
              }
            ]
          }
        ])
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        {
          provide: OrganizationService,
          useClass: MockOrganizationService
        },
        KeycloakService
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollectionNoticePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
