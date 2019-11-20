import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { CertificateComponent } from './pages/certificate/certificate.component';
import { DashboardComponent } from '@shared/components/dashboard/dashboard.component';


const routes: Routes = [
  {
    path: 'enrolment-certificates',
    component: DashboardComponent,
    children: [
      {
        path: '',
        component: CertificateComponent,
      }
    ]
  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EnrolmentCertificateRoutingModule { }
