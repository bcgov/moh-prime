import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { EnrolmentCertificateRoutingModule } from './enrolment-certificate-routing.module';
import { CertificateComponent } from './pages/certificate/certificate.component';


@NgModule({
  declarations: [CertificateComponent],
  imports: [
    CommonModule,
    EnrolmentCertificateRoutingModule
  ]
})
export class EnrolmentCertificateModule { }
