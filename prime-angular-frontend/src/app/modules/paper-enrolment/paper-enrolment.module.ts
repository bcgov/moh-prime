import { NgModule } from '@angular/core';

import { EditorModule } from '@tinymce/tinymce-angular';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { PaperEnrolmentDashboardComponent } from './shared/components/paper-enrolment-dashboard/paper-enrolment-dashboard.component';
import { PaperEnrolmentProgressIndicatorComponent } from './shared/components/paper-enrolment-progress-indicator/paper-enrolment-progress-indicator.component';

import { PaperEnrolmentRoutingModule } from './paper-enrolment-routing.module';
import { DemographicPageComponent } from './pages/demographic-page/demographic-page.component';
import { DemographicOverviewComponent } from './pages/demographic-page/demographic-overview.component';
import { CareSettingPageComponent } from './pages/care-setting-page/care-setting-page.component';
import { CareSettingOverviewComponent } from './pages/care-setting-page/care-setting-overview.component';
import { RegulatoryPageComponent } from './pages/regulatory-page/regulatory-page.component';
import { RegulatoryOverviewComponent } from './pages/regulatory-page/regulatory-overview.component';
import { OboSitesPageComponent } from './pages/obo-sites-page/obo-sites-page.component';
import { OboSitesOverviewComponent } from './pages/obo-sites-page/obo-sites-overview.component';
import { SelfDeclarationPageComponent } from './pages/self-declaration-page/self-declaration-page.component';
import { SelfDeclarationOverviewComponent } from './pages/self-declaration-page/self-declaration-overview.component';
import { OverviewPageComponent } from './pages/overview-page/overview-page.component';
import { NextStepsPageComponent } from './pages/next-steps-page/next-steps-page.component';
import { UploadPageComponent } from './pages/upload-page/upload-page.component';

@NgModule({
  declarations: [
    PaperEnrolmentDashboardComponent,
    PaperEnrolmentProgressIndicatorComponent,
    DemographicPageComponent,
    DemographicOverviewComponent,
    CareSettingPageComponent,
    CareSettingOverviewComponent,
    RegulatoryPageComponent,
    RegulatoryOverviewComponent,
    OboSitesPageComponent,
    OboSitesOverviewComponent,
    SelfDeclarationPageComponent,
    SelfDeclarationOverviewComponent,
    OverviewPageComponent,
    UploadPageComponent,
    NextStepsPageComponent
  ],
  imports: [
    PaperEnrolmentRoutingModule,
    SharedModule,
    DashboardModule,
    EditorModule
  ]
})
export class PaperEnrolmentModule { }
