import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';
import { EnrolmentRoutingModule } from './enrolment-routing.module';

import { ProfileComponent } from './pages/profile/profile.component';
import { RegulatoryComponent } from './pages/regulatory/regulatory.component';
import { DeviceProviderComponent } from './pages/device-provider/device-provider.component';
import { JobComponent } from './pages/job/job.component';
import { SelfDeclarationComponent } from './pages/self-declaration/self-declaration.component';
import { OrganizationComponent } from './pages/organization/organization.component';
import { ReviewComponent } from './pages/review/review.component';
import { ConfirmationComponent } from './pages/confirmation/confirmation.component';
import { AccessAgreementComponent } from './pages/access-agreement/access-agreement.component';
import { SummaryComponent } from './pages/summary/summary.component';

import { CollegeCertificationsComponent } from './shared/components/college-certifications/college-certifications.component';
import { MoaAccessAgreementComponent } from './pages/access-agreement/components/moa-access-agreement/moa-access-agreement.component';
import { RuAccessAgreementComponent } from './pages/access-agreement/components/ru-access-agreement/ru-access-agreement.component';
import { PageRefDirective } from './pages/access-agreement/page-ref.directive';

@NgModule({
  declarations: [
    ProfileComponent,
    RegulatoryComponent,
    AccessAgreementComponent,
    DeviceProviderComponent,
    JobComponent,
    SelfDeclarationComponent,
    OrganizationComponent,
    ReviewComponent,
    ConfirmationComponent,
    SummaryComponent,
    CollegeCertificationsComponent,
    MoaAccessAgreementComponent,
    RuAccessAgreementComponent,
    PageRefDirective
  ],
  imports: [
    SharedModule,
    EnrolmentRoutingModule
  ],
  exports: []
})
export class EnrolmentModule { }
