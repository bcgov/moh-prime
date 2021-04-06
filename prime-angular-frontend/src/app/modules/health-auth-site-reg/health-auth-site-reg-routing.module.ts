import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';

import { HealthAuthSiteRegRoutes } from './health-auth-site-reg.routes';
import { HealthAuthSiteRegGuard } from './shared/guards/health-auth-site-reg.guard';
import { HealthAuthSiteRegDashboardComponent } from './shared/components/health-auth-site-reg-dashboard/health-auth-site-reg-dashboard.component';

import { CollectionNoticePageComponent } from '@health-auth/pages/collection-notice-page/collection-notice-page.component';
import { AuthorizedUserPageComponent } from '@health-auth/pages/authorized-user-page/authorized-user-page.component';
import { SiteManagementPageComponent } from '@health-auth/pages/site-management-page/site-management-page.component';
import { HealthAuthCareSettingPageComponent } from '@health-auth/pages/health-auth-care-setting-page/health-auth-care-setting-page.component';
import { SiteInformationPageComponent } from '@health-auth/pages/site-information-page/site-information-page.component';
import { VendorPageComponent } from '@health-auth/pages/vendor-page/vendor-page.component';
import { SiteAddressPageComponent } from '@health-auth/pages/site-address-page/site-address-page.component';
import { HoursOperationPageComponent } from '@health-auth/pages/hours-operation-page/hours-operation-page.component';
import { RemoteUsersPageComponent } from '@health-auth/pages/remote-users-page/remote-users-page.component';
import { RemoteUserPageComponent } from '@health-auth/pages/remote-user-page/remote-user-page.component';
import { AdministratorPageComponent } from '@health-auth/pages/administrator-page/administrator-page.component';
import { PrivacyOfficerPageComponent } from '@health-auth/pages/privacy-officer-page/privacy-officer-page.component';
import { TechnicalSupportPageComponent } from '@health-auth/pages/technical-support-page/technical-support-page.component';
import { OverviewPageComponent } from '@health-auth/pages/overview-page/overview-page.component';

const routes: Routes = [
  {
    path: '',
    component: HealthAuthSiteRegDashboardComponent,
    canLoad: [
      HealthAuthSiteRegGuard
    ],
    canActivate: [],
    canActivateChild: [
      HealthAuthSiteRegGuard
    ],
    children: [
      {
        path: HealthAuthSiteRegRoutes.COLLECTION_NOTICE,
        component: CollectionNoticePageComponent,
        data: { title: 'Collection Notice' }
      },
      {
        path: HealthAuthSiteRegRoutes.AUTHORIZED_USER,
        component: AuthorizedUserPageComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Authorized User' }
      },
      {
        path: HealthAuthSiteRegRoutes.SITE_MANAGEMENT,
        component: SiteManagementPageComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Site Management' }
      },
      // TODO add in site ID url parameter... maybe an organization ID?
      {
        path: HealthAuthSiteRegRoutes.VENDOR,
        component: VendorPageComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Vendor' }
      },
      {
        path: HealthAuthSiteRegRoutes.HEALTH_AUTH_CARE_SETTING,
        component: HealthAuthCareSettingPageComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Health Authority Care Setting' }
      },
      {
        path: HealthAuthSiteRegRoutes.SITE_INFORMATION,
        component: SiteInformationPageComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Site Information' }
      },
      {
        path: HealthAuthSiteRegRoutes.SITE_ADDRESS,
        component: SiteAddressPageComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Site Address' }
      },
      {
        path: HealthAuthSiteRegRoutes.HOURS_OPERATION,
        component: HoursOperationPageComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Hours of Operation' }
      },
      {
        path: HealthAuthSiteRegRoutes.REMOTE_USERS,
        children: [
          {
            path: '',
            component: RemoteUsersPageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'Practitioners Requiring Remote Access' },
          },
          {
            path: ':index',
            component: RemoteUserPageComponent,
            canDeactivate: [CanDeactivateFormGuard],
            data: { title: 'Remote User' }
          }
        ]
      },
      {
        path: HealthAuthSiteRegRoutes.ADMINISTRATOR,
        component: AdministratorPageComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'PharmaNet Administrator' }
      },
      {
        path: HealthAuthSiteRegRoutes.PRIVACY_OFFICER,
        component: PrivacyOfficerPageComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Privacy Officer' }
      },
      {
        path: HealthAuthSiteRegRoutes.TECHNICAL_SUPPORT,
        component: TechnicalSupportPageComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Technical Support Contact' }
      },
      {
        path: HealthAuthSiteRegRoutes.SITE_OVERVIEW,
        component: OverviewPageComponent,
        data: { title: 'Information Review' }
      },
      {
        path: '', // Equivalent to `/` and alias for default view
        redirectTo: HealthAuthSiteRegRoutes.SITE_MANAGEMENT,
        pathMatch: 'full'
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class HealthAuthSiteRegRoutingModule { }
