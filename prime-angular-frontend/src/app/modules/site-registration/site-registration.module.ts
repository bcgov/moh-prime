import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { SiteRegistrationRoutingModule } from './site-registration-routing.module';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';

import { SiteRegistrationDashboardComponent } from './shared/components/site-registration-dashboard/site-registration-dashboard.component';
import { SiteProgressIndicatorComponent } from './shared/components/site-progress-indicator/site-progress-indicator.component';

import { CollectionNoticePageComponent } from './pages/collection-notice-page/collection-notice-page.component';
import { SiteManagementPageComponent } from './pages/site-management-page/site-management-page.component';
import { OrganizationSigningAuthorityPageComponent } from './pages/organization-signing-authority-page/organization-signing-authority-page.component';
import { OrganizationNamePageComponent } from './pages/organization-name-page/organization-name-page.component';
import { OrganizationClaimPageComponent } from './pages/organization-claim-page/organization-claim-page.component';
import { OrganizationClaimedPageComponent } from './pages/organization-claimed-page/organization-claimed-page.component';
import { OrganizationAgreementPageComponent } from './pages/organization-agreement-page/organization-agreement-page.component';
import { CareSettingPageComponent } from './pages/care-setting-page/care-setting-page.component';
import { BusinessLicencePageComponent } from './pages/business-licence-page/business-licence-page.component';
import { HoursOperationPageComponent } from './pages/hours-operation-page/hours-operation-page.component';
import { AdministratorPageComponent } from './pages/administrator-page/administrator-page.component';
import { PrivacyOfficerPageComponent } from './pages/privacy-officer-page/privacy-officer-page.component';
import { TechnicalSupportPageComponent } from './pages/technical-support-page/technical-support-page.component';
import { RemoteUsersPageComponent } from './pages/remote-users-page/remote-users-page.component';
import { RemoteUserPageComponent } from './pages/remote-user-page/remote-user-page.component';
import { OverviewPageComponent } from './pages/overview-page/overview-page.component';
import { NextStepsPageComponent } from './pages/next-steps-page/next-steps-page.component';
import { OrganizationClaimConfirmationPageComponent } from './pages/organization-claim-confirmation-page/organization-claim-confirmation-page.component';
import { BusinessLicenceRenewalPageComponent } from './pages/business-licence-renewal-page/business-licence-renewal-page.component';
import { BusinessLicenceExpiryComponent } from './shared/components/business-licence-expiry/business-licence-expiry.component';
import { ElectronicOrganizationAgreementPageComponent } from './pages/electronic-organization-agreement-page/electronic-organization-agreement-page.component';
import { DeviceProviderPageComponent } from './pages/device-provider-page/device-provider-page.component';
import { NgxMaskDirective, NgxMaskPipe, provideNgxMask } from 'ngx-mask';

@NgModule({
  declarations: [
    CollectionNoticePageComponent,
    SiteManagementPageComponent,
    OrganizationSigningAuthorityPageComponent,
    OrganizationClaimPageComponent,
    OrganizationNamePageComponent,
    OrganizationAgreementPageComponent,
    CareSettingPageComponent,
    BusinessLicencePageComponent,
    HoursOperationPageComponent,
    RemoteUsersPageComponent,
    RemoteUserPageComponent,
    AdministratorPageComponent,
    PrivacyOfficerPageComponent,
    TechnicalSupportPageComponent,
    SiteRegistrationDashboardComponent,
    SiteProgressIndicatorComponent,
    OverviewPageComponent,
    NextStepsPageComponent,
    OrganizationClaimConfirmationPageComponent,
    BusinessLicenceRenewalPageComponent,
    BusinessLicenceExpiryComponent,
    ElectronicOrganizationAgreementPageComponent,
    DeviceProviderPageComponent,
    OrganizationClaimedPageComponent
  ],
  imports: [
    SiteRegistrationRoutingModule,
    SharedModule,
    DashboardModule,
    NgxMaskDirective,
    NgxMaskPipe,
  ],
  providers: [
    provideNgxMask(),
  ]
})
export class SiteRegistrationModule { }
