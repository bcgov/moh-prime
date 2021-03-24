import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { CanDeactivateFormGuard } from '@core/guards/can-deactivate-form.guard';

import { HealthAuthSiteRegRoutes } from './health-auth-site-reg.routes';
import { HealthAuthSiteRegGuard } from './shared/guards/health-auth-site-reg.guard';
import { HealthAuthSiteRegDashboardComponent } from './shared/components/health-auth-site-reg-dashboard/health-auth-site-reg-dashboard.component';

import { CollectionNoticePageComponent } from './pages/collection-notice-page/collection-notice-page.component';
import { SiteManagementPageComponent } from './pages/site-management-page/site-management-page.component';

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
        data: { title: 'Authorized User' }
      },
      // {
      //   path: HealthAuthSiteRegRoutes.AUTHORIZED_USER,
      //   component: AuthorizedUserPageComponent,
      //   canDeactivate: [CanDeactivateFormGuard],
      //   data: { title: 'Authorized User' }
      // },
      {
        path: HealthAuthSiteRegRoutes.SITE_MANAGEMENT,
        component: SiteManagementPageComponent,
        canDeactivate: [CanDeactivateFormGuard],
        data: { title: 'Site Management' }
      },
      // {
      //   path: HealthAuthSiteRegRoutes.VENDOR,
      //   component: VendorPageComponent,
      //   canDeactivate: [CanDeactivateFormGuard],
      //   data: { title: 'Vendor' }
      // },
      // {
      //   path: HealthAuthSiteRegRoutes.HEALTH_AUTH_CARE_SETTING,
      //   component: CareSettingPageComponent,
      //   canDeactivate: [CanDeactivateFormGuard],
      //   data: { title: 'Health Authority Care Setting' }
      // },
      // {
      //   path: HealthAuthSiteRegRoutes.SITE_INFORMATION,
      //   component: SiteInformationPageComponent,
      //   canDeactivate: [CanDeactivateFormGuard],
      //   data: { title: 'Site Information' }
      // },
      // {
      //   path: HealthAuthSiteRegRoutes.SITE_ADDRESS,
      //   component: SiteAddressPageComponent,
      //   canDeactivate: [CanDeactivateFormGuard],
      //   data: { title: '' }
      // },
      // {
      //   path: HealthAuthSiteRegRoutes.HOURS_OPERATION,
      //   component: HoursOperationPageComponent,
      //   canDeactivate: [CanDeactivateFormGuard],
      //   data: { title: '' }
      // },
      // {
      //   path: HealthAuthSiteRegRoutes.REMOTE_USERS,
      //   children: [
      //     {
      //       path: '',
      //       component: RemoteUsersPageComponent,
      //       // canActivate: [SiteGuard],
      //       canDeactivate: [CanDeactivateFormGuard],
      //       data: { title: 'Practitioners Requiring Remote PharmaNet Access' },
      //     },
      //     {
      //       path: ':index',
      //       component: RemoteUserPageComponent,
      //       // canActivate: [SiteGuard],
      //       canDeactivate: [CanDeactivateFormGuard],
      //       data: { title: 'Remote User' }
      //     }
      //   ]
      // },
      // {
      //   path: HealthAuthSiteRegRoutes.ADMINISTRATOR,
      //   component: AdministratorPageComponent,
      //   canDeactivate: [CanDeactivateFormGuard],
      //   data: { title: '' }
      // },
      // {
      //   path: HealthAuthSiteRegRoutes.PRIVACY_OFFICER,
      //   component: PrivacyOfficerPageComponent,
      //   canDeactivate: [CanDeactivateFormGuard],
      //   data: { title: '' }
      // },
      // {
      //   path: HealthAuthSiteRegRoutes.TECHNICAL_SUPPORT,
      //   component: TechnicalSupportPageComponent,
      //   canDeactivate: [CanDeactivateFormGuard],
      //   data: { title: '' }
      // },
      // {
      //   path: HealthAuthSiteRegRoutes.SITE_OVERVIEW,
      //   component: OverviewPageComponent,
      //   data: { title: '' }
      // },
      // {
      //   path: HealthAuthSiteRegRoutes.NEXT_STEPS,
      //   component: AuthorizedUserComponent,
      //   data: { title: '' }
      // },
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
