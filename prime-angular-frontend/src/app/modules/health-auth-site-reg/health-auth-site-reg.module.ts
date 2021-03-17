import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { HealthAuthSiteRegRoutingModule } from './health-auth-site-reg-routing.module';
import { AuthorizedUserComponent } from './pages/authorized-user/authorized-user.component';

@NgModule({
  imports: [
    SharedModule,
    DashboardModule,
    HealthAuthSiteRegRoutingModule
  ],
  declarations: [AuthorizedUserComponent]
})
export class HealthAuthSiteRegModule { }
