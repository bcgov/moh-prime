import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { VaccinationsRoutes } from './vaccinations.routes';
import { VaccinationsDashboardComponent } from '@vaccinations/shared/components/vaccinations-dashboard/vaccinations-dashboard.component';

import { CredentialsPageComponent } from './pages/credentials-page/credentials-page.component';
import { IssuancePageComponent } from './pages/issuance-page/issuance-page.component';

const routes: Routes = [
  {
    path: '',
    component: VaccinationsDashboardComponent,
    canLoad: [],
    canActivate: [],
    canActivateChild: [],
    children: [
      {
        path: VaccinationsRoutes.CREDENTIALS,
        component: CredentialsPageComponent,
        data: { title: 'Credentials' }
      },
      {
        path: VaccinationsRoutes.ISSUANCE,
        component: IssuancePageComponent,
        data: { title: 'Issuance' }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VaccinationsRoutingModule {}
