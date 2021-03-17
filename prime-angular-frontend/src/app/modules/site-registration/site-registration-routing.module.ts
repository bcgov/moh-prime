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

import { CollectionNoticePageComponent } from './pages/collection-notice-page/collection-notice-page.component';
import { SiteManagementPageComponent } from './pages/site-management-page/site-management-page.component';
import { OrganizationSigningAuthorityPageComponent } from './pages/organization-signing-authority-page/organization-signing-authority-page.component';
import { OrganizationNamePageComponent } from './pages/organization-name-page/organization-name-page.component';
import { OrganizationAgreementPageComponent } from './pages/organization-agreement-page/organization-agreement-page.component';
import { CareSettingPageComponent } from './pages/care-setting-page/care-setting-page.component';
import { BusinessLicencePageComponent } from './pages/business-licence-page/business-licence-page.component';
import { SiteAddressPageComponent } from './pages/site-address-page/site-address-page.component';
import { HoursOperationPageComponent } from './pages/hours-operation-page/hours-operation-page.component';
import { AdministratorPageComponent } from './pages/administrator-page/administrator-page.component';
import { PrivacyOfficerPageComponent } from './pages/privacy-officer-page/privacy-officer-page.component';
import { TechnicalSupportPageComponent } from './pages/technical-support-page/technical-support-page.component';
import { RemoteUsersPageComponent } from './pages/remote-users-page/remote-users-page.component';
import { RemoteUserPageComponent } from './pages/remote-user-page/remote-user-page.component';
import { OverviewPageComponent } from './pages/overview-page/overview-page.component';
import { NextStepsPageComponent } from './pages/next-steps-page/next-steps-page.component';

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
        component: CollectionNoticePageComponent,
        data: { title: 'Collection Notice' }
      },
      {
        path: SiteRoutes.SITE_MANAGEMENT,
        children: [
          {
            path: '',
            component: SiteManagementPageComponent,
            data: { title: 'Site Management' },
          },
          {
            path: ':oid',
            children: [
              {
                path: SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY,
                component: OrganizationSigningAuthorityPageComponent,
                canActivate: [OrganizationGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Signing Authority' }
              },
              {
                path: SiteRoutes.ORGANIZATION_NAME,
                component: OrganizationNamePageComponent,
                canActivate: [OrganizationGuard],
                canDeactivate: [CanDeactivateFormGuard],
                data: { title: 'Organization Information' }
              },
              {
                path: SiteRoutes.ORGANIZATION_REVIEW,
                component: OverviewPageComponent,
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
                    component: CareSettingPageComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Care Setting' }
                  },
                  {
                    path: SiteRoutes.BUSINESS_LICENCE,
                    component: BusinessLicencePageComponent,
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
                        component: RemoteUserPageComponent,
                        canActivate: [SiteGuard],
                        canDeactivate: [CanDeactivateFormGuard],
                        data: { title: 'Remote User' }
                      }
                    ]
                  },
                  {
                    path: SiteRoutes.ADMINISTRATOR,
                    component: AdministratorPageComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'PharmaNet Administrator' }
                  },
                  {
                    path: SiteRoutes.PRIVACY_OFFICER,
                    component: PrivacyOfficerPageComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Privacy Officer' }
                  },
                  {
                    path: SiteRoutes.TECHNICAL_SUPPORT,
                    component: TechnicalSupportPageComponent,
                    canActivate: [SiteGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Technical Support Contact' }
                  },
                  {
                    path: SiteRoutes.ORGANIZATION_AGREEMENT,
                    component: OrganizationAgreementPageComponent,
                    canActivate: [SiteGuard, OrganizationGuard],
                    canDeactivate: [CanDeactivateFormGuard],
                    data: { title: 'Organization Agreement' }
                  },
                  {
                    path: SiteRoutes.SITE_REVIEW,
                    canActivate: [SiteGuard, OrganizationGuard],
                    component: OverviewPageComponent,
                    data: { title: 'Site Registration Review' }
                  },
                  {
                    path: SiteRoutes.NEXT_STEPS,
                    canActivate: [SiteGuard, OrganizationGuard],
                    component: NextStepsPageComponent,
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
