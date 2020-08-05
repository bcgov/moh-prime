import { NgModule } from '@angular/core';
import { EditorModule } from '@tinymce/tinymce-angular';

import { SharedModule } from '@shared/shared.module';

import { AdjudicationRoutingModule } from './adjudication-routing.module';

import { AdjudicationContainerComponent } from './shared/components/adjudication-container/adjudication-container.component';
import { AdjudicatorActionsComponent } from './shared/components/adjudicator-actions/adjudicator-actions.component';
import { AdjudicatorNotesComponent } from './pages/adjudicator-notes/adjudicator-notes.component';
import { EnrolleesComponent } from './pages/enrollees/enrollees.component';
import { EnrolmentComponent } from './pages/enrolment/enrolment.component';
import { LimitsConditionsClausesComponent } from './pages/limits-conditions-clauses/limits-conditions-clauses.component';
import { EnrolleeEnrolmentsComponent } from './pages/enrollee-enrolments/enrollee-enrolments.component';
import { EnrolleeAccessTermComponent } from './pages/enrollee-access-term/enrollee-access-term.component';
import { EnrolleeAccessTermEnrolmentComponent } from './pages/enrollee-access-term-enrolment/enrollee-access-term-enrolment.component';
import { EnrolleeEventsComponent } from './pages/enrollee-events/enrollee-events.component';
import { EnrolleeReviewStatusComponent } from './pages/enrollee-review-status/enrollee-review-status.component';
import { SiteRegistrationsComponent } from './pages/site-registrations/site-registrations.component';
import { SiteRegistrationComponent } from './pages/site-registration/site-registration.component';
import { SiteAdjudicationComponent } from './pages/site-adjudication/site-adjudication.component';
import { EnrolleeTableComponent } from './shared/components/enrollee-table/enrollee-table.component';
import { SearchFormComponent } from './shared/components/search-form/search-form.component';
import { DatedContentTableComponent } from './shared/components/dated-content-table/dated-content-table.component';
import {
  ReviewStatusContentComponent
} from './shared/components/review-status-content/review-status-content.component';
import { SiteRegistrationContainerComponent } from './shared/components/site-registration-container/site-registration-container.component';
import { SiteRegistrationTableComponent } from './shared/components/site-registration-table/site-registration-table.component';
import { SiteRegistrationActionsComponent } from './shared/components/site-registration-actions/site-registration-actions.component';
import { OrganizationInformationComponent } from './pages/organization-information/organization-information.component';

@NgModule({
  declarations: [
    AdjudicationContainerComponent,
    AdjudicatorActionsComponent,
    AdjudicatorNotesComponent,
    EnrolleesComponent,
    EnrolmentComponent,
    LimitsConditionsClausesComponent,
    EnrolleeEventsComponent,
    EnrolleeReviewStatusComponent,
    EnrolleeEnrolmentsComponent,
    EnrolleeAccessTermComponent,
    EnrolleeAccessTermEnrolmentComponent,
    EnrolleeTableComponent,
    SearchFormComponent,
    DatedContentTableComponent,
    ReviewStatusContentComponent,
    SiteRegistrationsComponent,
    SiteRegistrationComponent,
    SiteRegistrationContainerComponent,
    SiteRegistrationTableComponent,
    SiteRegistrationActionsComponent,
    SiteAdjudicationComponent,
    OrganizationInformationComponent
  ],
  imports: [
    SharedModule,
    EditorModule,
    AdjudicationRoutingModule
  ]
})
export class AdjudicationModule { }
