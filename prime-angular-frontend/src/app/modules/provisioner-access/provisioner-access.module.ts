import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { EnrolmentCertificateRoutingModule } from './provisioner-access-routing.module';
import { CertificateComponent } from './pages/certificate/certificate.component';
import { ProvisionerAccessComponent } from './shared/components/provisioner-access/provisioner-access.component';

@NgModule({
  declarations: [
    CertificateComponent,
    ProvisionerAccessComponent
  ],
  imports: [
    SharedModule,
    EnrolmentCertificateRoutingModule
  ]
})
export class EnrolmentCertificateModule { }
