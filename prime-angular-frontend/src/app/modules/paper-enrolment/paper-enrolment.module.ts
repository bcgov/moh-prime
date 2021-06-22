import { NgModule } from '@angular/core';

import { EditorModule } from '@tinymce/tinymce-angular';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';

import { SharedModule } from '@shared/shared.module';

import { DemographicComponent } from '@paper-enrolment/pages/demographic/demographic.component';
import { PaperEnrolmentProgressIndicatorComponent } from '@paper-enrolment/shared/components/paper-enrolment-progress-indicator/paper-enrolment-progress-indicator.component';
import { PaperEnrolmentRoutingModule } from './paper-enrolment-routing.module';
import { PaperEnrolmentDashboardComponent } from './shared/components/paper-enrolment-dashboard/paper-enrolment-dashboard.component';
import { CareSettingComponent } from './pages/care-setting/care-setting.component';
import { RegulatoryComponent } from './pages/regulatory/regulatory.component';
import { OboSitesComponent } from './pages/obo-sites/obo-sites.component';

@NgModule({
  declarations: [
    DemographicComponent,
    PaperEnrolmentDashboardComponent,
    PaperEnrolmentProgressIndicatorComponent,
    CareSettingComponent,
    RegulatoryComponent,
    OboSitesComponent
  ],
  imports: [
    PaperEnrolmentRoutingModule,
    SharedModule,
    DashboardModule,
    EditorModule
  ]
})
export class PaperEnrolmentModule { }
