import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';

import { SharedModule } from '@shared/shared.module';

import { PhsaEformsRoutingModule } from './phsa-eforms-routing.module';
import { PhsaEformsDashboardComponent } from './shared/components/phsa-eforms-dashboard/phsa-eforms-dashboard.component';
import { PhsaEformsProgressIndicatorComponent } from './shared/components/phsa-eforms-progress-indicator/phsa-eforms-progress-indicator.component';

import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';
import { AccessCodeComponent } from './pages/access-code/access-code.component';
import { BcscDemographicComponent } from './pages/bcsc-demographic/bcsc-demographic.component';
import { AvailableAccessComponent } from './pages/available-access/available-access.component';
import { SubmissionConfirmationComponent } from './pages/submission-confirmation/submission-confirmation.component';

@NgModule({
  declarations: [
    PhsaEformsDashboardComponent,
    PhsaEformsProgressIndicatorComponent,
    CollectionNoticeComponent,
    AccessCodeComponent,
    BcscDemographicComponent,
    AvailableAccessComponent,
    SubmissionConfirmationComponent
  ],
  imports: [
    SharedModule,
    DashboardModule,
    PhsaEformsRoutingModule
  ]
})
export class PhsaEformsModule { }
