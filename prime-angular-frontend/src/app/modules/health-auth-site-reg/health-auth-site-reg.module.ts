import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { HealthAuthSiteRegRoutingModule } from './health-auth-site-reg-routing.module';
import { AuthorizedUserComponent } from './pages/authorized-user/authorized-user.component';
import { VendorComponent } from './pages/vendor/vendor.component';
import { CareSettingComponent } from './pages/care-setting/care-setting.component';
import { SiteInformationComponent } from './pages/site-information/site-information.component';

@NgModule({
  declarations: [
    AuthorizedUserComponent,
    VendorComponent,
    CareSettingComponent,
    SiteInformationComponent,
  ],
  imports: [
    SharedModule,
    DashboardModule,
    HealthAuthSiteRegRoutingModule
  ],
})
export class HealthAuthSiteRegModule { }
