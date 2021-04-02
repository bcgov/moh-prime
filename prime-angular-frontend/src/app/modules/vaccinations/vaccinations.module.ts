import { NgModule } from '@angular/core';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { VaccinationsRoutingModule } from './vaccinations-routing.module';
import { VaccinationsDashboardComponent } from './shared/components/vaccinations-dashboard/vaccinations-dashboard.component';

import { IssuancePageComponent } from './pages/issuance-page/issuance-page.component';
import { CredentialsPageComponent } from './pages/credentials-page/credentials-page.component';

@NgModule({
  declarations: [
    VaccinationsDashboardComponent,
    IssuancePageComponent,
    CredentialsPageComponent,
  ],
  imports: [
    SharedModule,
    DashboardModule,
    VaccinationsRoutingModule
  ]
})
export class VaccinationsModule {}
