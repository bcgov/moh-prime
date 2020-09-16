import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { KeycloakService } from 'keycloak-angular';

import { MockOrganizationService } from 'test/mocks/mock-organization.service';

import { CollectionNoticeComponent } from './collection-notice.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { SiteRoutes } from '@registration/site-registration.routes';
import {
  OrganizationSigningAuthorityComponent
} from '@registration/pages/organization-signing-authority/organization-signing-authority.component';

describe('CollectionNoticeComponent', () => {
  let component: CollectionNoticeComponent;
  let fixture: ComponentFixture<CollectionNoticeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [
        SiteRegistrationModule,
        RouterTestingModule.withRoutes([
          {
            path: SiteRoutes.SITE_MANAGEMENT,
            children: [
              {
                path: ':oid',
                children: [
                  {
                    path: SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY,
                    component: OrganizationSigningAuthorityComponent
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
      ]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CollectionNoticeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
