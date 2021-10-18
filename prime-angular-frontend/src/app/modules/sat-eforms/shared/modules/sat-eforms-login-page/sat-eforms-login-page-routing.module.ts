import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { SatEformsLoginPageComponent } from './sat-eforms-login-page.component';

const routes: Routes = [
  {
    path: '',
    component: SatEformsLoginPageComponent,
    data: { title: 'Special Authority E-Forms' }
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SatEformsLoginPageRoutingModule {}
