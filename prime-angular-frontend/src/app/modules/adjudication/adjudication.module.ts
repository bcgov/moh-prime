import { NgModule } from '@angular/core';
import { EditorModule } from '@tinymce/tinymce-angular';

import { SharedModule } from '@shared/shared.module';

import { AdjudicationRoutingModule } from './adjudication-routing.module';
import { AdjudicationContainerComponent } from './shared/components/adjudication-container/adjudication-container.component';
import { AdjudicatorActionsComponent } from './shared/components/adjudicator-actions/adjudicator-actions.component';
import { AdjudicatorNotesComponent } from './pages/adjudicator-notes/adjudicator-notes.component';
import { EnrolmentsComponent } from './pages/enrolments/enrolments.component';
import { EnrolmentComponent } from './pages/enrolment/enrolment.component';
import { LimitsConditionsClausesComponent } from './pages/limits-conditions-clauses/limits-conditions-clauses.component';
import { EnrolleeProfileVersionComponent } from './pages/enrollee-profile-version/enrollee-profile-version.component';
import { EnrolleeProfileVersionsComponent } from './pages/enrollee-profile-versions/enrollee-profile-versions.component';
import { EnrolleeAccessTermsComponent } from './pages/enrollee-access-terms/enrollee-access-terms.component';
import { EnrolleeAccessTermComponent } from './pages/enrollee-access-term/enrollee-access-term.component';
import { EnrolleeAccessTermEnrolmentComponent } from './pages/enrollee-access-term-enrolment/enrollee-access-term-enrolment.component';
import { EnrolleeEventsComponent } from './pages/enrollee-events/enrollee-events.component';
import { EnrolleeReviewStatusComponent } from './pages/enrollee-review-status/enrollee-review-status.component';
import { EnrolleeTableComponent } from './shared/components/enrollee-table/enrollee-table.component';
import { SearchFormComponent } from './shared/components/search-form/search-form.component';
import { DatedContentTableComponent } from './shared/components/dated-content-table/dated-content-table.component';
import { ReviewStatusContentComponent } from './pages/enrollee-review-status/components/review-status-content/review-status-content.component';
import { StatusReasonsPipe } from './pages/enrollee-review-status/pipes/status-reasons.pipe';

@NgModule({
  declarations: [
    AdjudicationContainerComponent,
    AdjudicatorActionsComponent,
    AdjudicatorNotesComponent,
    EnrolmentsComponent,
    EnrolmentComponent,
    LimitsConditionsClausesComponent,
    EnrolleeEventsComponent,
    EnrolleeReviewStatusComponent,
    EnrolleeProfileVersionComponent,
    EnrolleeProfileVersionsComponent,
    EnrolleeAccessTermsComponent,
    EnrolleeAccessTermComponent,
    EnrolleeAccessTermEnrolmentComponent,
    EnrolleeTableComponent,
    SearchFormComponent,
    DatedContentTableComponent,
    ReviewStatusContentComponent,
    StatusReasonsPipe
  ],
  imports: [
    SharedModule,
    AdjudicationRoutingModule,
    EditorModule
  ]
})
export class AdjudicationModule { }
