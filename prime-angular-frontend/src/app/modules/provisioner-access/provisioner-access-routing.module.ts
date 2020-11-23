import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { DashboardComponent } from '@lib/modules/dashboard/components/dashboard/dashboard.component';

import { UnsupportedGuard } from '@core/guards/unsupported.guard';
import { CertificateComponent } from '@certificate/pages/certificate/certificate.component';

const routes: Routes = [
  {
    path: '',
    component: DashboardComponent,
    canActivateChild: [
      UnsupportedGuard
    ],
    children: [
      {
        path: ':tokenId',
        component: CertificateComponent,
        data: { title: 'Enrolment Certificate' }
      }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProvisionerAccessRoutingModule { }
