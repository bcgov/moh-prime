import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';

import { SharedModule } from '@shared/shared.module';

import { PhsaEformsRoutingModule } from './phsa-eforms-routing.module';
import { PhsaEformsDashboardComponent } from './shared/components/phsa-eforms-dashboard/phsa-eforms-dashboard.component';
import { PhsaEformsProgressIndicatorComponent } from './shared/components/phsa-eforms-progress-indicator/phsa-eforms-progress-indicator.component';
import { PartyTypePipe } from './shared/pipes/party-type.pipe';

import { AccessCodeComponent } from './pages/access-code/access-code.component';
import { BcscDemographicComponent } from './pages/bcsc-demographic/bcsc-demographic.component';
import { SubmissionConfirmationComponent } from './pages/submission-confirmation/submission-confirmation.component';
import { AvailableAccessComponent } from './pages/available-access/available-access.component';
import { CollectionNoticeComponent } from './pages/collection-notice/collection-notice.component';

@NgModule({
  declarations: [
    PhsaEformsDashboardComponent,
    AccessCodeComponent,
    PhsaEformsProgressIndicatorComponent,
    BcscDemographicComponent,
    SubmissionConfirmationComponent,
    AvailableAccessComponent,
    CollectionNoticeComponent,
    PartyTypePipe,
  ],
  imports: [
    SharedModule,
    PhsaEformsRoutingModule,
    DashboardModule
  ]
})
export class PhsaEformsModule { }
