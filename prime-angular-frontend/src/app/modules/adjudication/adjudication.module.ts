import { NgModule } from '@angular/core';
import { EditorModule } from '@tinymce/tinymce-angular';

import { SharedModule } from '@shared/shared.module';
import { AdjudicationRoutingModule } from './adjudication-routing.module';

import { EnrolmentsComponent } from './pages/enrolments/enrolments.component';
import { EnrolmentComponent } from './pages/enrolment/enrolment.component';
import { AdjudicatorNotesComponent } from './pages/adjudicator-notes/adjudicator-notes.component';
import { LimitsConditionsClausesComponent } from './pages/limits-conditions-clauses/limits-conditions-clauses.component';
import { EnrolleeProfileVersionComponent } from './pages/enrollee-profile-version/enrollee-profile-version.component';
import { EnrolleeProfileVersionsComponent } from './pages/enrollee-profile-versions/enrollee-profile-versions.component';
import { EnrolleeAccessTermsComponent } from './pages/enrollee-access-terms/enrollee-access-terms.component';
import { EnrolleeAccessTermComponent } from './pages/enrollee-access-term/enrollee-access-term.component';
import { EnrolleeAccessTermEnrolmentComponent } from './pages/enrollee-access-term-enrolment/enrollee-access-term-enrolment.component';

@NgModule({
  declarations: [
    EnrolmentsComponent,
    EnrolmentComponent,
    AdjudicatorNotesComponent,
    LimitsConditionsClausesComponent,
    EnrolleeProfileVersionComponent,
    EnrolleeProfileVersionsComponent,
    EnrolleeAccessTermsComponent,
    EnrolleeAccessTermComponent,
    EnrolleeAccessTermEnrolmentComponent
  ],
  imports: [
    SharedModule,
    AdjudicationRoutingModule,
    EditorModule
  ]
})
export class AdjudicationModule { }
