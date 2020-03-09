import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { ConfigResolver } from '@config/config-resolver';
import { UnsupportedGuard } from '@core/guards/unsupported.guard';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';

import { SiteRoutes } from './site-registration.routes';
import { SiteCollectionNoticeComponent } from './pages/site-collection-notice/site-collection-notice.component';
import { VendorComponent } from './pages/vendor/vendor.component';



const routes: Routes = [
  {
    path: SiteRoutes.MODULE_PATH,
    component: DashboardComponent,
    canActivate: [UnsupportedGuard],
    canActivateChild: [
      // AuthenticationGuard,
      // SiteRegistrationGuard
    ],
    // Ensure that the configuration is loaded, otherwise
    // if it already exists NOOP
    resolve: [ConfigResolver],
    children: [
      {
        path: SiteRoutes.COLLECTION_NOTICE,
        component: SiteCollectionNoticeComponent,
        data: { title: 'Site Collection Notice' }
      },
      {
        path: SiteRoutes.VENDOR,
        component: VendorComponent,
        data: { title: 'Vendor' }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SiteRegistrationRoutingModule { }
