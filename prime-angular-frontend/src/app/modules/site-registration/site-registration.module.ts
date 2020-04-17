import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { SiteRegistrationRoutingModule } from './site-registration-routing.module';

import { RegistrantProfileFormComponent } from './shared/components/registrant-profile-form/registrant-profile-form.component';
import { RegistrantProfileReviewComponent } from './shared/components/registrant-profile-review/registrant-profile-review.component';
import { SiteProgressIndicatorComponent } from './shared/components/site-progress-indicator/site-progress-indicator.component';
import { SiteCollectionNoticeComponent } from './shared/components/site-collection-notice/site-collection-notice.component';

import { VendorComponent } from './pages/vendor/vendor.component';
import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';
import { SigningAuthorityComponent } from './pages/signing-authority/signing-authority.component';
import { AdministratorComponent } from './pages/administrator/administrator.component';
import { MultipleSitesComponent } from './pages/multiple-sites/multiple-sites.component';
import { TechnicalSupportContactComponent } from './pages/technical-support-contact/technical-support-contact.component';
import { SiteReviewComponent } from './pages/site-review/site-review.component';
import { OrganizationInformationComponent } from './pages/organization-information/organization-information.component';
import { HoursOperationComponent } from './pages/hours-operation/hours-operation.component';
import { PrivacyOfficerComponent } from './pages/privacy-officer/privacy-officer.component';
import { ConfirmationComponent } from './pages/confirmation/confirmation.component';
import { SiteAddressComponent } from './pages/site-address/site-address.component';
import { OrganizationAgreementComponent } from './pages/organization-agreement/organization-agreement.component';

@NgModule({
  declarations: [
    CollectionNoticeComponent,
    VendorComponent,
    SigningAuthorityComponent,
    AdministratorComponent,
    MultipleSitesComponent,
    TechnicalSupportContactComponent,
    SiteReviewComponent,
    OrganizationInformationComponent,
    HoursOperationComponent,
    PrivacyOfficerComponent,
    RegistrantProfileFormComponent,
    ConfirmationComponent,
    RegistrantProfileReviewComponent,
    SiteProgressIndicatorComponent,
    SiteCollectionNoticeComponent,
    SiteAddressComponent,
    OrganizationAgreementComponent
  ],
  imports: [
    SharedModule,
    SiteRegistrationRoutingModule
  ]
})
export class SiteRegistrationModule { }
