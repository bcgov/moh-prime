import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { SiteRegistrationRoutingModule } from './site-registration-routing.module';

import { BusinessHoursModule } from '@lib/modules/business-hours/business-hours.module';

import { SiteRegistrationDashboardComponent } from './shared/components/site-registration-dashboard/site-registration-dashboard.component';
import { PartyProfileFormComponent } from './shared/components/party-profile-form/party-profile-form.component';
import { SameAsComponent } from './shared/components/same-as/same-as.component';
import { SiteProgressIndicatorComponent } from './shared/components/site-progress-indicator/site-progress-indicator.component';
import { SiteCollectionNoticeComponent } from './shared/components/site-collection-notice/site-collection-notice.component';
import { SummaryCardComponent } from './shared/components/summary-card/summary-card.component';

import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';
import { SiteManagementComponent } from './pages/site-management/site-management.component';

import { OrganizationSigningAuthorityComponent } from './pages/organization-signing-authority/organization-signing-authority.component';
import { OrganizationNameComponent } from './pages/organization-name/organization-name.component';
import { OrganizationAgreementComponent } from './pages/organization-agreement/organization-agreement.component';

import { CareSettingComponent } from './pages/care-setting/care-setting.component';
import { BusinessLicenceComponent } from './pages/business-licence/business-licence.component';
import { SiteAddressComponent } from './pages/site-address/site-address.component';
import { HoursOperationComponent } from './pages/hours-operation/hours-operation.component';
import { AdministratorComponent } from './pages/administrator/administrator.component';
import { PrivacyOfficerComponent } from './pages/privacy-officer/privacy-officer.component';
import { TechnicalSupportComponent } from './pages/technical-support/technical-support.component';
import { RemoteUsersComponent } from './pages/remote-users/remote-users.component';
import { RemoteUserComponent } from './pages/remote-user/remote-user.component';
import { OverviewComponent } from './pages/overview/overview.component';

@NgModule({
  declarations: [
    CollectionNoticeComponent,
    SiteCollectionNoticeComponent,
    SiteManagementComponent,

    OrganizationSigningAuthorityComponent,
    OrganizationNameComponent,
    OrganizationAgreementComponent,

    CareSettingComponent,
    BusinessLicenceComponent,
    SiteAddressComponent,
    HoursOperationComponent,
    RemoteUsersComponent,
    RemoteUserComponent,
    AdministratorComponent,
    PrivacyOfficerComponent,
    TechnicalSupportComponent,

    SiteRegistrationDashboardComponent,
    SiteProgressIndicatorComponent,
    PartyProfileFormComponent,
    SameAsComponent,
    SummaryCardComponent,
    OverviewComponent
  ],
  imports: [
    SharedModule,
    SiteRegistrationRoutingModule,
    BusinessHoursModule
  ],
})
export class SiteRegistrationModule { }
