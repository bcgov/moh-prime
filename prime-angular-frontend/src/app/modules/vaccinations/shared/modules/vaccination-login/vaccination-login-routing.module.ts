import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { VaccinationLoginComponent } from './vaccination-login.component';

const routes: Routes = [
  {
    path: '',
    component: VaccinationLoginComponent,
    data: { title: 'Vaccination Issuance' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class VaccinationLoginRoutingModule {}
