import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';

import { SharedModule } from '@shared/shared.module';

import { PhsaLabtechRoutingModule } from './phsa-labtech-routing.module';
import { PhsaLabtechDashboardComponent } from './shared/components/phsa-labtech-dashboard/phsa-labtech-dashboard.component';

import { AccessCodeComponent } from './pages/access-code/access-code.component';
import { BcscDemographicComponent } from './pages/bcsc-demographic/bcsc-demographic.component';
import { SubmissionConfirmationComponent } from './pages/submission-confirmation/submission-confirmation.component';
import { AvailableAccessComponent } from './pages/available-access/available-access.component';
import { PhsaProgressIndicatorComponent } from './shared/components/phsa-progress-indicator/phsa-progress-indicator.component';
import { PartyTypePipe } from './shared/pipes/party-type.pipe';

@NgModule({
  declarations: [
    PhsaLabtechDashboardComponent,
    AccessCodeComponent,
    PhsaProgressIndicatorComponent,
    BcscDemographicComponent,
    SubmissionConfirmationComponent,
    AvailableAccessComponent,
    PartyTypePipe
  ],
  imports: [
    SharedModule,
    PhsaLabtechRoutingModule,
    DashboardModule
  ]
})
export class PhsaLabtechModule { }
