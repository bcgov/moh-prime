import { NgModule } from '@angular/core';

import { EditorModule } from '@tinymce/tinymce-angular';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { PaperEnrolmentDashboardComponent } from './shared/components/paper-enrolment-dashboard/paper-enrolment-dashboard.component';
import { PaperEnrolmentProgressIndicatorComponent } from './shared/components/paper-enrolment-progress-indicator/paper-enrolment-progress-indicator.component';

import { PaperEnrolmentRoutingModule } from './paper-enrolment-routing.module';
import { DemographicComponent } from './pages/demographic/demographic.component';
import { CareSettingComponent } from './pages/care-setting/care-setting.component';
import { RegulatoryComponent } from './pages/regulatory/regulatory.component';
import { OboSitesPageComponent } from './pages/obo-sites-page/obo-sites-page.component';
import { SelfDeclarationPageComponent } from './pages/self-declaration-page/self-declaration-page.component';

@NgModule({
  declarations: [
    PaperEnrolmentDashboardComponent,
    PaperEnrolmentProgressIndicatorComponent,
    DemographicComponent,
    CareSettingComponent,
    RegulatoryComponent,
    OboSitesPageComponent,
    SelfDeclarationPageComponent
  ],
  imports: [
    PaperEnrolmentRoutingModule,
    SharedModule,
    DashboardModule,
    EditorModule
  ]
})
export class PaperEnrolmentModule {}
