import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterTestingModule } from '@angular/router/testing';
import { provideHttpClientTesting } from '@angular/common/http/testing';
import { ActivatedRoute } from '@angular/router';

import { KeycloakService } from 'keycloak-angular';

import { MockOrganizationService } from 'test/mocks/mock-organization.service';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SiteRegistrationModule } from '@registration/site-registration.module';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { SiteRoutes } from '@registration/site-registration.routes';
import { OrganizationSigningAuthorityPageComponent } from '@registration/pages/organization-signing-authority-page/organization-signing-authority-page.component';
import { CollectionNoticePageComponent } from './collection-notice-page.component';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

describe('CollectionNoticePageComponent', () => {
  let component: CollectionNoticePageComponent;
  let fixture: ComponentFixture<CollectionNoticePageComponent>;
  const mockActivatedRoute = {
    snapshot: {
      data: {
        title: 'Collection Notice',
        redirectRouteSegments: {
          nextRoute: SiteRoutes.ORGANIZATIONS
        }
      }
    }
  };

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
    schemas: [CUSTOM_ELEMENTS_SCHEMA],
    imports: [RouterTestingModule.withRoutes([
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
        ])],
    providers: [
        {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
        },
        {
            provide: OrganizationService,
            useClass: MockOrganizationService
        },
        {
            provide: ActivatedRoute,
            useValue: mockActivatedRoute
        },
        KeycloakService,
        provideHttpClient(withInterceptorsFromDi()),
        provideHttpClientTesting()
    ]
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
