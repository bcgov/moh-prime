import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { HealthAuthSiteRegRoutingModule } from './health-auth-site-reg-routing.module';

import { CollectionNoticePageComponent } from './pages/collection-notice-page/collection-notice-page.component';
import { SiteManagementPageComponent } from './pages/site-management-page/site-management-page.component';

@NgModule({
  declarations: [
    CollectionNoticePageComponent,
    SiteManagementPageComponent
  ],
  imports: [
    SharedModule,
    DashboardModule,
    HealthAuthSiteRegRoutingModule
  ],
})
export class HealthAuthSiteRegModule { }
