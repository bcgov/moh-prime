import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { SiteRegistrationRoutingModule } from './site-registration-routing.module';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';

import { SiteRegistrationDashboardComponent } from './shared/components/site-registration-dashboard/site-registration-dashboard.component';
import { ContactProfileFormComponent } from './shared/components/contact-profile-form/contact-profile-form.component';
import { SameAsComponent } from './shared/components/same-as/same-as.component';
import { SiteProgressIndicatorComponent } from './shared/components/site-progress-indicator/site-progress-indicator.component';
import { SummaryCardComponent } from './shared/components/summary-card/summary-card.component';

import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';
import { SiteManagementPageComponent } from './pages/site-management-page/site-management-page.component';

import { OrganizationSigningAuthorityComponent } from './pages/organization-signing-authority/organization-signing-authority.component';
import { OrganizationNameComponent } from './pages/organization-name/organization-name.component';
import { OrganizationAgreementComponent } from './pages/organization-agreement/organization-agreement.component';

import { CareSettingPageComponent } from './pages/care-setting-page/care-setting-page.component';
import { BusinessLicenceComponent } from './pages/business-licence/business-licence.component';
import { SiteAddressPageComponent } from './pages/site-address-page/site-address-page.component';
import { HoursOperationPageComponent } from './pages/hours-operation-page/hours-operation-page.component';
import { AdministratorPageComponent } from './pages/administrator-page/administrator-page.component';
import { PrivacyOfficerPageComponent } from './pages/privacy-officer-page/privacy-officer-page.component';
import { TechnicalSupportPageComponent } from './pages/technical-support-page/technical-support-page.component';
import { RemoteUsersPageComponent } from './pages/remote-users-page/remote-users-page.component';
import { RemoteUserPageComponent } from './pages/remote-user-page/remote-user-page.component';
import { OverviewComponent } from './pages/overview/overview.component';
import { NextStepsComponent } from './pages/next-steps/next-steps.component';

@NgModule({
  declarations: [
    CollectionNoticeComponent,
    SiteManagementPageComponent,

    OrganizationSigningAuthorityComponent,
    OrganizationNameComponent,
    OrganizationAgreementComponent,

    CareSettingPageComponent,
    BusinessLicenceComponent,
    SiteAddressPageComponent,
    HoursOperationPageComponent,
    RemoteUsersPageComponent,
    RemoteUserPageComponent,
    AdministratorPageComponent,
    PrivacyOfficerPageComponent,
    TechnicalSupportPageComponent,

    SiteRegistrationDashboardComponent,
    SiteProgressIndicatorComponent,
    ContactProfileFormComponent,
    SameAsComponent,
    SummaryCardComponent,
    OverviewComponent,
    NextStepsComponent
  ],
  imports: [
    SiteRegistrationRoutingModule,
    SharedModule,
    DashboardModule
  ]
})
export class SiteRegistrationModule { }
