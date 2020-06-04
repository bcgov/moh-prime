import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigResolver } from '@config/config-resolver';
import { UnsupportedGuard } from '@core/guards/unsupported.guard';
import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { SiteRoutes } from './site-registration.routes';
import { RegistrantGuard } from './shared/guards/registrant.guard';
import { OrganizationGuard } from './shared/guards/organization.guard';
import { SiteGuard } from './shared/guards/site.guard';

import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';
import { OrganizationsComponent } from './pages/organizations/organizations.component';
import { OrganizationSigningAuthorityComponent } from './pages/organization-signing-authority/organization-signing-authority.component';
import { OrganizationInformationComponent } from './pages/organization-information/organization-information.component';
import { OrganizationTypeComponent } from './pages/organization-type/organization-type.component';
import { OrganizationAgreementComponent } from './pages/organization-agreement/organization-agreement.component';
import { OrganizationOverviewComponent } from './pages/organization-overview/organization-overview.component';

import { VendorComponent } from './pages/vendor/vendor.component';
import { SiteAddressComponent } from './pages/site-address/site-address.component';
import { BusinessLicenceComponent } from './pages/business-licence/business-licence.component';
import { HoursOperationComponent } from './pages/hours-operation/hours-operation.component';
import { RemoteUsersComponent } from './pages/remote-users/remote-users.component';
import { RemoteUserComponent } from './pages/remote-user/remote-user.component';
import { AdministratorComponent } from './pages/administrator/administrator.component';
import { PrivacyOfficerComponent } from './pages/privacy-officer/privacy-officer.component';
import { TechnicalSupportComponent } from './pages/technical-support/technical-support.component';
import { SiteOverviewComponent } from './pages/site-overview/site-overview.component';
import { ConfirmationComponent } from './pages/confirmation/confirmation.component';

const routes: Routes = [
  {
    path: SiteRoutes.MODULE_PATH,
    component: DashboardComponent,
    canActivate: [UnsupportedGuard],
    canActivateChild: [
      AuthenticationGuard,
      RegistrantGuard,
      // TODO leaving the updates to the RegistrationGuard to the end
      // RegistrationGuard
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
        path: SiteRoutes.ORGANIZATIONS,
        children: [
          {
            path: '',
            component: OrganizationsComponent,
            data: { title: 'Organizations' },
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
                path: SiteRoutes.ORGANIZATION_INFORMATION,
                component: OrganizationInformationComponent,
                canActivate: [OrganizationGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Organization Information' }
              },
              {
                path: SiteRoutes.ORGANIZATION_TYPE,
                component: OrganizationTypeComponent,
                canActivate: [OrganizationGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Organization Type' }
              },
              {
                path: SiteRoutes.ORGANIZATION_REVIEW,
                component: OrganizationOverviewComponent,
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
                // TODO need a guard/component redirect back to signing authority if not completed
                path: '', // Equivalent to `/` and alias for `organization-review`
                redirectTo: SiteRoutes.ORGANIZATION_REVIEW,
                pathMatch: 'full'
              },
              {
                path: `${SiteRoutes.SITES}/:sid`,
                children: [
                  {
                    path: SiteRoutes.SITE_ADDRESS,
                    component: SiteAddressComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Site Name' }
                  },
                  {
                    path: SiteRoutes.BUSINESS_LICENCE,
                    component: BusinessLicenceComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Submit Your Business Licence' }
                  },
                  {
                    path: SiteRoutes.HOURS_OPERATION,
                    component: HoursOperationComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Hours of Operation' }
                  },
                  {
                    path: SiteRoutes.VENDOR,
                    component: VendorComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'What PharmaNet software vendor does this site use?' }
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
                    path: SiteRoutes.SITE_REVIEW,
                    canActivate: [SiteGuard],
                    component: SiteOverviewComponent,
                    data: { title: 'Site Registration Review' }
                  },
                  {
                    path: SiteRoutes.CONFIRMATION,
                    canActivate: [SiteGuard],
                    component: ConfirmationComponent,
                    data: { title: 'Submission Confirmation' }
                  },
                  {
                    // TODO need a guard/component redirect back to signing authority if not completed
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
        redirectTo: SiteRoutes.ORGANIZATIONS,
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
