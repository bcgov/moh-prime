import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { UnsupportedGuard } from '@core/guards/unsupported.guard';
import { ProvisionerAccessRoutes } from '@certificate/provisioner-access.routes';
import { CertificateComponent } from '@certificate/pages/certificate/certificate.component';
import { ProvisionerAccessComponent } from '@certificate/shared/components/provisioner-access/provisioner-access.component';

const routes: Routes = [
  {
    path: ProvisionerAccessRoutes.MODULE_PATH,
    component: ProvisionerAccessComponent,
    canActivate: [UnsupportedGuard],
    canActivateChild: [UnsupportedGuard],
    children: [
      {
        path: ':tokenId',
        component: CertificateComponent,
        data: { title: 'Enrolment Certificate' }
      }
    ]
  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ProvisionerAccessRoutingModule { }
