import { SiteInfoComponent } from './pages/site-info/site-info.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from '@lib/modules/dashboard/components/dashboard/dashboard.component';
import { HealthAuthSiteRegRoutes } from './health-auth-site-reg.routes';
import { HealthAuthSiteRegGuard } from './shared/guards/health-auth-site-reg.guard';
import { AuthorizedUserComponent } from './pages/authorized-user/authorized-user.component';
import { VendorComponent } from './pages/vendor/vendor.component';
import { CareSettingComponent } from './pages/care-setting/care-setting.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    canLoad: [
      HealthAuthSiteRegGuard
    ],
    canActivate: [],
    canActivateChild: [
      HealthAuthSiteRegGuard
    ],
    children: [
      {
        path: HealthAuthSiteRegRoutes.AUTHORIZED_USER,
        component: AuthorizedUserComponent,
        data: { title: 'Authorized User' }
      },
      {
        path: HealthAuthSiteRegRoutes.SITE_MANAGEMENT,
        component: AuthorizedUserComponent,
        data: { title: 'Site Management' }
      },
      {
        path: HealthAuthSiteRegRoutes.VENDOR,
        component: VendorComponent,
        data: { title: 'Vendor' }
      },
      {
        path: HealthAuthSiteRegRoutes.HEALTH_AUTH_CARE_SETTING,
        component: CareSettingComponent,
        data: { title: 'Health Authority Care Setting' }
      },
      {
        path: HealthAuthSiteRegRoutes.SITE_INFORMATION,
        component: SiteInfoComponent,
        data: { title: 'Site Information' }
      },
      //     {
      //       path: HealthAuthSiteRegRoutes.SITE_ADDRESS,
      //       component: AuthorizedUserComponent,
      //       data: { title: '' }
      //     },
      //     {
      //       path: HealthAuthSiteRegRoutes.HOURS_OPERATION,
      //       component: AuthorizedUserComponent,
      //       data: { title: '' }
      //     },
      //     {
      //       path: HealthAuthSiteRegRoutes.REMOTE_USERS,
      //       children: [
      //         // {
      //         //   path: '',
      //         //   component: RemoteUsersPageComponent,
      //         //   // canActivate: [SiteGuard],
      //         //   // canDeactivate: [CanDeactivateFormGuard],
      //         //   data: { title: 'Practitioners Requiring Remote PharmaNet Access' },
      //         // },
      //         // {
      //         //   path: ':index',
      //         //   component: RemoteUserPageComponent,
      //         //   // canActivate: [SiteGuard],
      //         //   // canDeactivate: [CanDeactivateFormGuard],
      //         //   data: { title: 'Remote User' }
      //         // }
      //       ]
      //     },
      //     {
      //       path: HealthAuthSiteRegRoutes.ADMINISTRATOR,
      //       component: AuthorizedUserComponent,
      //       data: { title: '' }
      //     },
      //     {
      //       path: HealthAuthSiteRegRoutes.PRIVACY_OFFICER,
      //       component: AuthorizedUserComponent,
      //       data: { title: '' }
      //     },
      //     {
      //       path: HealthAuthSiteRegRoutes.TECHNICAL_SUPPORT,
      //       component: AuthorizedUserComponent,
      //       data: { title: '' }
      //     },
      //     {
      //       path: HealthAuthSiteRegRoutes.SITE_REVIEW,
      //       component: AuthorizedUserComponent,
      //       data: { title: '' }
      //     },
      //     {
      //       path: HealthAuthSiteRegRoutes.NEXT_STEPS,
      //       component: AuthorizedUserComponent,
      //       data: { title: '' }
      //     }
      //   ]
      // },
      {
        path: '', // Equivalent to `/` and alias for default view
        // TODO change to site management and hook up guards, but for demo use AuthorizedUser
        // redirectTo: HealthAuthSiteRegRoutes.SITE_MANAGEMENT,
        redirectTo: HealthAuthSiteRegRoutes.AUTHORIZED_USER,
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
