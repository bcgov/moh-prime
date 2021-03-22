import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { HealthAuthSiteRegRoutingModule } from './health-auth-site-reg-routing.module';

import { CollectionNoticePageComponent } from './pages/collection-notice-page/collection-notice-page.component';
import { SiteManagementPageComponent } from './pages/site-management-page/site-management-page.component';
import { AuthorizedUserPageComponent } from './pages/authorized-user-page/authorized-user-page.component';
import { VendorPageComponent } from './pages/vendor-page/vendor-page.component';
import { CareSettingPageComponent } from './pages/care-setting-page/care-setting-page.component';
import { SiteInformationPageComponent } from './pages/site-information-page/site-information-page.component';
import { SiteAddressPageComponent } from './pages/site-address-page/site-address-page.component';
import { HoursOperationPageComponent } from './pages/hours-operation-page/hours-operation-page.component';
import { RemoteUsersPageComponent } from './pages/remote-users-page/remote-users-page.component';
import { RemoteUserPageComponent } from './pages/remote-user-page/remote-user-page.component';
import { AdministratorPageComponent } from './pages/administrator-page/administrator-page.component';
import { PrivacyOfficerPageComponent } from './pages/privacy-officer-page/privacy-officer-page.component';
import { TechnicalSupportPageComponent } from './pages/technical-support-page/technical-support-page.component';
import { OverviewPageComponent } from './pages/overview-page/overview-page.component';

@NgModule({
  declarations: [
    CollectionNoticePageComponent,
    SiteManagementPageComponent,
    AuthorizedUserPageComponent,
    VendorPageComponent,
    CareSettingPageComponent,
    SiteInformationPageComponent,
    SiteAddressPageComponent,
    HoursOperationPageComponent,
    RemoteUsersPageComponent,
    RemoteUserPageComponent,
    AdministratorPageComponent,
    PrivacyOfficerPageComponent,
    TechnicalSupportPageComponent,
    OverviewPageComponent
  ],
  imports: [
    SharedModule,
    DashboardModule,
    HealthAuthSiteRegRoutingModule
  ],
})
export class HealthAuthSiteRegModule { }
