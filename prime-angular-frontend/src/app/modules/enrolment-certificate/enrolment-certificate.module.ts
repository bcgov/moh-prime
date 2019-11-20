import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { SharedModule } from '@shared/shared.module';
import { EnrolmentCertificateRoutingModule } from './enrolment-certificate-routing.module';
import { CertificateComponent } from './pages/certificate/certificate.component';


@NgModule({
  declarations: [CertificateComponent],
  imports: [
    CommonModule,
    SharedModule,
    EnrolmentCertificateRoutingModule
  ]
})
export class EnrolmentCertificateModule { }
