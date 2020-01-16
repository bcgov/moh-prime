import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';
import { AdjudicationRoutingModule } from './adjudication-routing.module';

import { EnrolmentsComponent } from './pages/enrolments/enrolments.component';
import { EnrolmentComponent } from './pages/enrolment/enrolment.component';
import { AdjudicatorNotesComponent } from './pages/adjudicator-notes/adjudicator-notes.component';
import { UserAgreementNotesComponent } from './pages/user-agreement-notes/user-agreement-notes.component';
import { EnrolmentCertificateNotesComponent } from './pages/enrolment-certificate-notes/enrolment-certificate-notes.component';
import { EnrolleeProfileHistoryComponent } from './pages/enrollee-profile-history/enrollee-profile-history.component';
import { EnrolleeProfileHistoriesComponent } from './pages/enrollee-profile-histories/enrollee-profile-histories.component';

@NgModule({
  declarations: [
    EnrolmentsComponent,
    EnrolmentComponent,
    AdjudicatorNotesComponent,
    UserAgreementNotesComponent,
    EnrolmentCertificateNotesComponent,
    EnrolleeProfileHistoryComponent,
    EnrolleeProfileHistoriesComponent
  ],
  imports: [
    SharedModule,
    AdjudicationRoutingModule
  ]
})
export class AdjudicationModule { }
