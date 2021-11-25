import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { HealthAuthSiteRegRoutingModule } from './health-auth-site-reg-routing.module';
import { HealthAuthSiteRegDashboardComponent } from './shared/components/health-auth-site-reg-dashboard/health-auth-site-reg-dashboard.component';
import { SiteProgressIndicatorComponent } from './shared/components/site-progress-indicator/site-progress-indicator.component';

import { CollectionNoticePageComponent } from './pages/collection-notice-page/collection-notice-page.component';
import { AuthorizedUserPageComponent } from './pages/authorized-user-page/authorized-user-page.component';
import { AuthorizedUserNextStepsPageComponent } from './pages/authorized-user-next-steps-page/authorized-user-next-steps-page.component';
import { AuthorizedUserApprovedPageComponent } from './pages/authorized-user-approved-page/authorized-user-approved-page.component';
import { AuthorizedUserDeclinedPageComponent } from './pages/authorized-user-declined-page/authorized-user-declined-page.component';
import { SiteManagementPageComponent } from './pages/site-management-page/site-management-page.component';
import { HealthAuthCareTypePageComponent } from './pages/health-auth-care-type-page/health-auth-care-type-page.component';
import { HealthAuthCareTypeOverviewComponent } from './pages/health-auth-care-type-page/health-auth-care-type-overview.component';
import { SiteInformationPageComponent } from './pages/site-information-page/site-information-page.component';
import { SiteInformationOverviewComponent } from './pages/site-information-page/site-information-overview.component';
import { VendorPageComponent } from './pages/vendor-page/vendor-page.component';
import { VendorOverviewComponent } from './pages/vendor-page/vendor-overview.component';
import { SiteAddressPageComponent } from './pages/site-address-page/site-address-page.component';
import { SiteAddressOverviewComponent } from './pages/site-address-page/site-address-overview.component';
import { HoursOperationPageComponent } from './pages/hours-operation-page/hours-operation-page.component';
import { HoursOperationOverviewComponent } from './pages/hours-operation-page/hours-operation-overview.component';
import { AdministratorPageComponent } from './pages/administrator-page/administrator-page.component';
import { AdministratorOverviewComponent } from './pages/administrator-page/administrator-overview.component';
import { TechnicalSupportPageComponent } from './pages/technical-support-page/technical-support-page.component';
import { TechnicalSupportOverviewComponent } from './pages/technical-support-page/technical-support-overview.component';
import { OverviewPageComponent } from './pages/overview-page/overview-page.component';

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
    VendorOverviewComponent,
    SiteInformationPageComponent,
    SiteInformationOverviewComponent,
    HealthAuthCareTypePageComponent,
    HealthAuthCareTypeOverviewComponent,
    SiteAddressPageComponent,
    SiteAddressOverviewComponent,
    HoursOperationPageComponent,
    HoursOperationOverviewComponent,
    AdministratorPageComponent,
    AdministratorOverviewComponent,
    TechnicalSupportPageComponent,
    TechnicalSupportOverviewComponent,
    OverviewPageComponent
  ],
  imports: [
    SharedModule,
    DashboardModule,
    HealthAuthSiteRegRoutingModule
  ],
})
export class HealthAuthSiteRegModule {}
