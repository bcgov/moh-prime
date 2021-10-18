import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { SatEformsRoutingModule } from './sat-eforms-routing.module';
import { SatEformsDashboardComponent } from './shared/components/sat-eforms-dashboard/sat-eforms-dashboard.component';
import { SatEformsProgressIndicatorComponent } from './shared/components/sat-eforms-progress-indicator/sat-eforms-progress-indicator.component';

import { CollectionNoticePageComponent } from './pages/collection-notice-page/collection-notice-page.component';
import { DemographicPageComponent } from './pages/demographic-page/demographic-page.component';
import { RegulatoryPageComponent } from './pages/regulatory-page/regulatory-page.component';
import { SubmissionConfirmationPageComponent } from './pages/submission-confirmation-page/submission-confirmation-page.component';

@NgModule({
  declarations: [
    SatEformsDashboardComponent,
    SatEformsProgressIndicatorComponent,
    CollectionNoticePageComponent,
    DemographicPageComponent,
    RegulatoryPageComponent,
    SubmissionConfirmationPageComponent
  ],
  imports: [
    SharedModule,
    DashboardModule,
    SatEformsRoutingModule
  ]
})
export class SatEformsModule {}
