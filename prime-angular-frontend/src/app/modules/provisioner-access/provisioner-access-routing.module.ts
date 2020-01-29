import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { PageNotFoundComponent } from '@core/components/page-not-found/page-not-found.component';

import { ProvisionerAccessRoutes } from './provisioner-access.routes';
import { CertificateComponent } from './pages/certificate/certificate.component';
import { ProvisionerAccessComponent } from './shared/components/provisioner-access/provisioner-access.component';

const routes: Routes = [
  {
    path: ProvisionerAccessRoutes.MODULE_PATH,
    component: ProvisionerAccessComponent,
    children: [
      {
        path: ':tokenId',
        component: CertificateComponent,
        data: { title: 'Enrolment Certificate' }
      },
      {
        path: '',
        component: PageNotFoundComponent,
        pathMatch: 'full'
      }
    ]
  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EnrolmentCertificateRoutingModule { }
