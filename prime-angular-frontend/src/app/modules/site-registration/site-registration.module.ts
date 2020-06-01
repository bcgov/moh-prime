import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { SiteRegistrationRoutingModule } from './site-registration-routing.module';

import { BusinessHoursModule } from '@lib/modules/business-hours/business-hours.module';
// TODO split out all related filepond files into a /lib module ie. config and components
import { FilePondModule, registerPlugin } from 'ngx-filepond';
import FilePondPluginFileValidateType from 'filepond-plugin-file-validate-type';
registerPlugin(FilePondPluginFileValidateType);

import { RegistrantProfileFormComponent } from './shared/components/registrant-profile-form/registrant-profile-form.component';
import { SiteProgressIndicatorComponent } from './shared/components/site-progress-indicator/site-progress-indicator.component';
import { SiteCollectionNoticeComponent } from './shared/components/site-collection-notice/site-collection-notice.component';
import { SameAsComponent } from './shared/components/same-as/same-as.component';

import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';
import { OrganizationsComponent } from './pages/organizations/organizations.component';
import { OrganizationSigningAuthorityComponent } from './pages/organization-signing-authority/organization-signing-authority.component';
import { OrganizationInformationComponent } from './pages/organization-information/organization-information.component';
import { OrganizationTypeComponent } from './pages/organization-type/organization-type.component';
import { OrganizationOverviewComponent } from './pages/organization-overview/organization-overview.component';
import { OrganizationReviewComponent } from './shared/components/organization-review/organization-review.component';
import { OrganizationAgreementComponent } from './pages/organization-agreement/organization-agreement.component';

import { VendorComponent } from './pages/vendor/vendor.component';
import { AdministratorComponent } from './pages/administrator/administrator.component';
import { TechnicalSupportComponent } from './pages/technical-support/technical-support.component';
import { SiteOverviewComponent } from './pages/site-overview/site-overview.component';
import { HoursOperationComponent } from './pages/hours-operation/hours-operation.component';
import { PrivacyOfficerComponent } from './pages/privacy-officer/privacy-officer.component';
import { ConfirmationComponent } from './pages/confirmation/confirmation.component';
import { SiteAddressComponent } from './pages/site-address/site-address.component';
import { BusinessLicenceComponent } from './pages/business-licence/business-licence.component';

@NgModule({
  declarations: [
    CollectionNoticeComponent,
    SiteCollectionNoticeComponent,

    OrganizationsComponent,
    OrganizationSigningAuthorityComponent,
    OrganizationInformationComponent,
    OrganizationTypeComponent,
    OrganizationAgreementComponent,
    OrganizationOverviewComponent,
    OrganizationReviewComponent,

    SiteAddressComponent,
    BusinessLicenceComponent,
    HoursOperationComponent,
    VendorComponent,
    AdministratorComponent,
    PrivacyOfficerComponent,
    TechnicalSupportComponent,
    SiteOverviewComponent,
    ConfirmationComponent,

    SiteProgressIndicatorComponent,
    RegistrantProfileFormComponent,
    SameAsComponent
  ],
  imports: [
    SharedModule,
    SiteRegistrationRoutingModule,
    FilePondModule,
    BusinessHoursModule,
    SiteRegistrationRoutingModule
  ]
})
export class SiteRegistrationModule { }
