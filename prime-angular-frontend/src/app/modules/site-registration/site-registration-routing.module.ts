import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UnderagedGuard } from '@core/guards/underaged.guard';
import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';
import { AuthenticationGuard } from '@auth/shared/guards/authentication.guard';

import { SiteRoutes } from './site-registration.routes';
import { RegistrantGuard } from './shared/guards/registrant.guard';
import { RegistrationGuard } from './shared/guards/registration.guard';
import { OrganizationGuard } from './shared/guards/organization.guard';
import { SiteGuard } from './shared/guards/site.guard';
import { SiteRegistrationDashboardComponent } from './shared/components/site-registration-dashboard/site-registration-dashboard.component';

import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';
import { SiteManagementComponent } from './pages/site-management/site-management.component';

import { OrganizationSigningAuthorityComponent } from './pages/organization-signing-authority/organization-signing-authority.component';
import { OrganizationNameComponent } from './pages/organization-name/organization-name.component';
import { OrganizationAgreementComponent } from './pages/organization-agreement/organization-agreement.component';

import { CareSettingComponent } from './pages/care-setting/care-setting.component';
import { BusinessLicenceComponent } from './pages/business-licence/business-licence.component';
import { SiteAddressPageComponent } from './pages/site-address-page/site-address-page.component';
import { HoursOperationPageComponent } from './pages/hours-operation-page/hours-operation-page.component';
import { AdministratorComponent } from './pages/administrator/administrator.component';
import { PrivacyOfficerComponent } from './pages/privacy-officer/privacy-officer.component';
import { TechnicalSupportComponent } from './pages/technical-support/technical-support.component';
import { RemoteUsersPageComponent } from './pages/remote-users-page/remote-users-page.component';
import { RemoteUserComponent } from './pages/remote-user/remote-user.component';
import { OverviewComponent } from './pages/overview/overview.component';
import { NextStepsComponent } from './pages/next-steps/next-steps.component';

const routes: Routes = [
  {
    path: SiteRoutes.MODULE_PATH,
    component: SiteRegistrationDashboardComponent,
    canActivate: [
      AuthenticationGuard,
      UnderagedGuard
    ],
    canActivateChild: [
      AuthenticationGuard,
      RegistrantGuard,
      RegistrationGuard
    ],
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
                path: '', // Equivalent to `/` and alias for `organization-review`
                redirectTo: SiteRoutes.ORGANIZATION_REVIEW,
                pathMatch: 'full'
              },
              {
                path: `${ SiteRoutes.SITES }/:sid`,
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
                    data: { title: 'Site Business Licence' }
                  },
                  {
                    path: SiteRoutes.SITE_ADDRESS,
                    component: SiteAddressPageComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Site Address' }
                  },
                  {
                    path: SiteRoutes.HOURS_OPERATION,
                    component: HoursOperationPageComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Hours of Operation' }
                  },
                  {
                    path: SiteRoutes.REMOTE_USERS,
                    children: [
                      {
                        path: '',
                        component: RemoteUsersPageComponent,
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
                    data: { title: 'PharmaNet Administrator' }
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
                    path: SiteRoutes.ORGANIZATION_AGREEMENT,
                    component: OrganizationAgreementComponent,
                    canActivate: [SiteGuard, OrganizationGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Organization Agreement' }
                  },
                  {
                    path: SiteRoutes.SITE_REVIEW,
                    canActivate: [SiteGuard, OrganizationGuard],
                    component: OverviewComponent,
                    data: { title: 'Site Registration Review' }
                  },
                  {
                    path: SiteRoutes.NEXT_STEPS,
                    canActivate: [SiteGuard, OrganizationGuard],
                    component: NextStepsComponent,
                    data: { title: 'Next Steps' }
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
