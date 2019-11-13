import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';
import { EnrolmentRoutingModule } from './enrolment-routing.module';

import { ProfileComponent } from './pages/profile/profile.component';
import { SelfDeclarationComponent } from './pages/self-declaration/self-declaration.component';
import { PharmanetAccessComponent } from './pages/pharmanet-access/pharmanet-access.component';
import { ReviewComponent } from './pages/review/review.component';
import { ConfirmationComponent } from './pages/confirmation/confirmation.component';
import { AccessAgreementComponent } from './pages/access-agreement/access-agreement.component';

import { CollegeCertificationsComponent } from './shared/components/college-certifications/college-certifications.component';
import { PageTitleComponent } from './shared/components/page-title/page-title.component';
import { SummaryComponent } from './pages/summary/summary.component';
import { RegulatoryComponent } from './pages/regulatory/regulatory.component';
import { DeviceProviderComponent } from './pages/device-provider/device-provider.component';
import { JobComponent } from './pages/job/job.component';

@NgModule({
  declarations: [
    ProfileComponent,
    SelfDeclarationComponent,
    PharmanetAccessComponent,
    ReviewComponent,
    ConfirmationComponent,
    CollegeCertificationsComponent,
    PageTitleComponent,
    AccessAgreementComponent,
    SummaryComponent,
    RegulatoryComponent,
    DeviceProviderComponent,
    JobComponent
  ],
  imports: [
    SharedModule,
    EnrolmentRoutingModule
  ],
  exports: []
})
export class EnrolmentModule { }
