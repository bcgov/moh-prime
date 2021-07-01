import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { HealthAuthSiteRegRoutingModule } from './health-auth-site-reg-routing.module';
import { HealthAuthSiteRegDashboardComponent } from './shared/components/health-auth-site-reg-dashboard/health-auth-site-reg-dashboard.component';
// TODO share this between site registration modules by moving out into @lib...
import { SiteProgressIndicatorComponent } from '@health-auth/shared/components/site-progress-indicator/site-progress-indicator.component';

import { CollectionNoticePageComponent } from '@health-auth/pages/collection-notice-page/collection-notice-page.component';
import { AuthorizedUserPageComponent } from '@health-auth/pages/authorized-user-page/authorized-user-page.component';
import { AuthorizedUserNextStepsPageComponent } from './pages/authorized-user-next-steps-page/authorized-user-next-steps-page.component';
import { AuthorizedUserApprovedPageComponent } from './pages/authorized-user-approved-page/authorized-user-approved-page.component';
import { AuthorizedUserDeclinedPageComponent } from './pages/authorized-user-declined-page/authorized-user-declined-page.component';
import { SiteManagementPageComponent } from '@health-auth/pages/site-management-page/site-management-page.component';
import { HealthAuthCareTypePageComponent } from '@health-auth/pages/health-auth-care-type-page/health-auth-care-type-page.component';
import { SiteInformationPageComponent } from '@health-auth/pages/site-information-page/site-information-page.component';
import { VendorPageComponent } from '@health-auth/pages/vendor-page/vendor-page.component';
import { SiteAddressPageComponent } from '@health-auth/pages/site-address-page/site-address-page.component';
import { HoursOperationPageComponent } from '@health-auth/pages/hours-operation-page/hours-operation-page.component';
import { RemoteUsersPageComponent } from '@health-auth/pages/remote-users-page/remote-users-page.component';
import { RemoteUserPageComponent } from '@health-auth/pages/remote-user-page/remote-user-page.component';
import { AdministratorPageComponent } from '@health-auth/pages/administrator-page/administrator-page.component';
import { OverviewPageComponent } from '@health-auth/pages/overview-page/overview-page.component';

@NgModule({
  declarations: [
    HealthAuthSiteRegDashboardComponent,
    SiteProgressIndicatorComponent,
    CollectionNoticePageComponent,
    AuthorizedUserPageComponent,
    AuthorizedUserNextStepsPageComponent,
    AuthorizedUserApprovedPageComponent,
    AuthorizedUserDeclinedPageComponent,
    SiteManagementPageComponent,
    VendorPageComponent,
    SiteInformationPageComponent,
    HealthAuthCareTypePageComponent,
    SiteAddressPageComponent,
    HoursOperationPageComponent,
    RemoteUsersPageComponent,
    RemoteUserPageComponent,
    AdministratorPageComponent,
    OverviewPageComponent
  ],
  imports: [
    SharedModule,
    DashboardModule,
    HealthAuthSiteRegRoutingModule
  ],
})
export class HealthAuthSiteRegModule {}
