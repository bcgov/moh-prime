import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigResolver } from '@config/config-resolver';
import { UnsupportedGuard } from '@core/guards/unsupported.guard';
import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { SiteRoutes } from './site-registration.routes';
import { RegistrantGuard } from './shared/guards/registrant.guard';
import { OrganizationGuard } from './shared/guards/organization.guard';
import { SiteGuard } from './shared/guards/site.guard';

import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';

import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';
import { SiteManagementComponent } from './pages/site-management/site-management.component';

import { OrganizationSigningAuthorityComponent } from './pages/organization-signing-authority/organization-signing-authority.component';
import { OrganizationNameComponent } from './pages/organization-name/organization-name.component';
import { OrganizationAgreementComponent } from './pages/organization-agreement/organization-agreement.component';

import { CareSettingComponent } from './pages/care-setting/care-setting.component';
import { BusinessLicenceComponent } from './pages/business-licence/business-licence.component';
import { SiteAddressComponent } from './pages/site-address/site-address.component';
import { HoursOperationComponent } from './pages/hours-operation/hours-operation.component';
import { AdministratorComponent } from './pages/administrator/administrator.component';
import { PrivacyOfficerComponent } from './pages/privacy-officer/privacy-officer.component';
import { TechnicalSupportComponent } from './pages/technical-support/technical-support.component';
import { RemoteUsersComponent } from './pages/remote-users/remote-users.component';
import { RemoteUserComponent } from './pages/remote-user/remote-user.component';
import { OverviewComponent } from './pages/overview/overview.component';
import { RegistrationGuard } from './shared/guards/registration.guard';

const routes: Routes = [
  {
    path: SiteRoutes.MODULE_PATH,
    component: DashboardComponent,
    canActivate: [UnsupportedGuard],
    canActivateChild: [
      AuthenticationGuard,
      RegistrantGuard,
      RegistrationGuard
    ],
    // Ensure that the configuration is loaded, otherwise
    // if it already exists NOOP
    resolve: [ConfigResolver],
    children: [
      {
        path: SiteRoutes.COLLECTION_NOTICE,
        component: CollectionNoticeComponent,
        data: { title: 'Collection Notice' }
      },
      {
        path: SiteRoutes.SITE_MANAGEMENT,
        children: [
          {
            path: '',
            component: SiteManagementComponent,
            data: { title: 'Site Management' },
          },
          {
            path: ':oid',
            children: [
              {
                path: SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY,
                component: OrganizationSigningAuthorityComponent,
                canActivate: [OrganizationGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Signing Authority' }
              },
              {
                path: SiteRoutes.ORGANIZATION_NAME,
                component: OrganizationNameComponent,
                canActivate: [OrganizationGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Organization Information' }
              },
              {
                path: SiteRoutes.ORGANIZATION_REVIEW,
                component: OverviewComponent,
                canActivate: [OrganizationGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Organization Review' }
              },
              {
                path: SiteRoutes.ORGANIZATION_AGREEMENT,
                component: OrganizationAgreementComponent,
                canActivate: [OrganizationGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Organization Agreement' }
              },
              {
                path: '', // Equivalent to `/` and alias for `organization-review`
                redirectTo: SiteRoutes.ORGANIZATION_REVIEW,
                pathMatch: 'full'
              },
              {
                path: `${SiteRoutes.SITES}/:sid`,
                children: [
                  {
                    path: SiteRoutes.CARE_SETTING,
                    component: CareSettingComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Care Setting' }
                  },
                  {
                    path: SiteRoutes.BUSINESS_LICENCE,
                    component: BusinessLicenceComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Submit Your Business Licence' }
                  },
                  {
                    path: SiteRoutes.SITE_ADDRESS,
                    component: SiteAddressComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Site Name' }
                  },
                  {
                    path: SiteRoutes.HOURS_OPERATION,
                    component: HoursOperationComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Hours of Operation' }
                  },
                  {
                    path: SiteRoutes.ADMINISTRATOR,
                    component: AdministratorComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Administrator of PharmaNet' }
                  },
                  {
                    path: SiteRoutes.PRIVACY_OFFICER,
                    component: PrivacyOfficerComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Privacy Officer' }
                  },
                  {
                    path: SiteRoutes.TECHNICAL_SUPPORT,
                    component: TechnicalSupportComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Technical Support Contact' }
                  },
                  {
                    path: SiteRoutes.REMOTE_USERS,
                    children: [
                      {
                        path: '',
                        component: RemoteUsersComponent,
                        canActivate: [SiteGuard],
                        canDeactivate: [CanDeactivateFormGuard],
                        data: { title: 'Practitioners Requiring Remote PharmaNet Access' },
                      },
                      {
                        path: ':index',
                        component: RemoteUserComponent,
                        canActivate: [SiteGuard],
                        canDeactivate: [CanDeactivateFormGuard],
                        data: { title: 'Remote User' }
                      }
                    ]
                  },
                  {
                    path: SiteRoutes.SITE_REVIEW,
                    canActivate: [SiteGuard, OrganizationGuard],
                    component: OverviewComponent,
                    data: { title: 'Site Registration Review' }
                  },
                  {
                    path: '', // Equivalent to `/` and alias for `site-review`
                    redirectTo: SiteRoutes.SITE_REVIEW,
                    pathMatch: 'full'
                  }
                ]
              }
            ]
          }
        ]
      },
      {
        path: '', // Equivalent to `/` and alias for `organizations`
        redirectTo: SiteRoutes.SITE_MANAGEMENT,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SiteRegistrationRoutingModule { }
