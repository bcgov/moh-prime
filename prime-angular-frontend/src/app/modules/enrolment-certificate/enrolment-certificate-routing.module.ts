import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CertificateComponent } from './pages/certificate/certificate.component';
import { EnrolmentCertificateComponent } from './shared/components/enrolment-certificate/enrolment-certificate.component';
import { PageNotFoundComponent } from '@core/components/page-not-found/page-not-found.component';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';
import { EnrolmentCertificateRoutes } from './enrolment-certificate.routes';


const routes: Routes = [
  {
    path: EnrolmentCertificateRoutes.MODULE_PATH,
    component: EnrolmentCertificateComponent,
    children: [
      {
        path: ':tokenId',
        component: CertificateComponent,
      },
      // {
      //   path: '',
      //   component: PageNotFoundComponent,
      //   pathMatch: 'full'
      // }
    ]
  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EnrolmentCertificateRoutingModule { }
