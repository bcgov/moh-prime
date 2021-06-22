import { NgModule } from '@angular/core';

import { EditorModule } from '@tinymce/tinymce-angular';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { PaperEnrolmentDashboardComponent } from './shared/components/paper-enrolment-dashboard/paper-enrolment-dashboard.component';
import { PaperEnrolmentProgressIndicatorComponent } from './shared/components/paper-enrolment-progress-indicator/paper-enrolment-progress-indicator.component';

import { PaperEnrolmentRoutingModule } from './paper-enrolment-routing.module';
import { DemographicPageComponent } from './pages/demographic-page/demographic-page.component';
import { CareSettingPageComponent } from './pages/care-setting-page/care-setting-page.component';
import { RegulatoryPageComponent } from './pages/regulatory-page/regulatory-page.component';
import { OboSitesPageComponent } from './pages/obo-sites-page/obo-sites-page.component';
import { SelfDeclarationPageComponent } from './pages/self-declaration-page/self-declaration-page.component';

@NgModule({
  declarations: [
    PaperEnrolmentDashboardComponent,
    PaperEnrolmentProgressIndicatorComponent,
    DemographicPageComponent,
    CareSettingPageComponent,
    RegulatoryPageComponent,
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
