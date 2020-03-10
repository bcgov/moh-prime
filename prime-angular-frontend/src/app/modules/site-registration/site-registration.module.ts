import { NgModule } from '@angular/core';
import { SiteCollectionNoticeComponent } from './pages/site-collection-notice/site-collection-notice.component';
import { SharedModule } from '@shared/shared.module';
import { SiteRegistrationRoutingModule } from './site-registration-routing.module';
import { VendorComponent } from './pages/vendor/vendor.component';
import { SigningAuthorityComponent } from './pages/signing-authority/signing-authority.component';
import { AdministratorComponent } from './pages/administrator/administrator.component';
import { MultipleSitesComponent } from './pages/multiple-sites/multiple-sites.component';
import { TechnicalSupportContactComponent } from './pages/technical-support-contact/technical-support-contact.component';
import { SiteReviewComponent } from './pages/site-review/site-review.component';
import { SiteInformationComponent } from './pages/site-information/site-information.component';
import { HoursOperationComponent } from './pages/hours-operation/hours-operation.component';
import { PrivacyOfficerComponent } from './pages/privacy-officer/privacy-officer.component';



@NgModule({
  declarations: [
    SiteCollectionNoticeComponent,
    VendorComponent,
    SigningAuthorityComponent,
    AdministratorComponent,
    MultipleSitesComponent,
    TechnicalSupportContactComponent,
    SiteReviewComponent,
    SiteInformationComponent,
    HoursOperationComponent,
    PrivacyOfficerComponent
  ],
  imports: [
    SharedModule,
    SiteRegistrationRoutingModule
  ]
})
export class SiteRegistrationModule { }
