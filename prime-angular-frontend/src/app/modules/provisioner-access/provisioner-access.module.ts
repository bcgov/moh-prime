import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { ProvisionerAccessRoutingModule } from '@certificate/provisioner-access-routing.module';
import { CertificateComponent } from '@certificate/pages/certificate/certificate.component';
import { ProvisionerAccessComponent } from '@certificate/shared/components/provisioner-access/provisioner-access.component';

@NgModule({
  declarations: [
    CertificateComponent,
    ProvisionerAccessComponent
  ],
  imports: [
    SharedModule,
    ProvisionerAccessRoutingModule
  ]
})
export class ProvisionerAccessModule { }
