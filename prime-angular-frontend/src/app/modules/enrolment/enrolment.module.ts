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
import { PageRefDirective } from './pages/access-agreement/page-ref.directive';
import { MoaAccessAgreementComponent } from './pages/access-agreement/components/moa-access-agreement/moa-access-agreement.component';
import { RuAccessAgreementComponent } from './pages/access-agreement/components/ru-access-agreement/ru-access-agreement.component';
import { SummaryComponent } from './pages/summary/summary.component';

import { CollegeCertificationFormComponent } from './shared/components/college-certification-form/college-certification-form.component';
import { JobFormComponent } from './shared/components/job-form/job-form.component';

@NgModule({
  declarations: [
    ProfileComponent,
    RegulatoryComponent,
    DeviceProviderComponent,
    JobComponent,
    SelfDeclarationComponent,
    OrganizationComponent,
    ReviewComponent,
    ConfirmationComponent,
    AccessAgreementComponent,
    MoaAccessAgreementComponent,
    RuAccessAgreementComponent,
    PageRefDirective,
    SummaryComponent,
    CollegeCertificationFormComponent,
    JobFormComponent,
  ],
  imports: [
    SharedModule,
    EnrolmentRoutingModule
  ],
  exports: []
})
export class EnrolmentModule { }
