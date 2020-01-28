import { Pipe, PipeTransform } from '@angular/core';

import { EnrolmentCertificate } from 'app/modules/provisioner-access/shared/models/enrolment-certificate.model';

@Pipe({
  name: 'certificate'
})
export class CertificatePipe implements PipeTransform {
  transform(certificate: EnrolmentCertificate, display: string): string {
    if (!certificate) {
      return null;
    }

    switch (display) {
      case 'preferredName':
        return this.getPreferredName(certificate);
      case 'fullName':
        return this.getFullName(certificate);
    }
  }

  private getFullName(certificate: any) {
    const firstName = (certificate.firstName) ? certificate.firstName : '';
    const middleName = (certificate.middleName) ? certificate.middleName : '';
    const lastName = (certificate.lastName) ? certificate.lastName : '';
    return ` ${firstName} ${middleName} ${lastName} `;
  }

  private getPreferredName(certificate: any) {
    const preferredFirstName = (certificate.preferredFirstName) ? certificate.preferredFirstName : '';
    const preferredMiddleName = (certificate.preferredMiddleName) ? certificate.preferredMiddleName : '';
    const preferredLastName = (certificate.preferredLastName) ? certificate.preferredLastName : '';
    return ` ${preferredFirstName} ${preferredMiddleName} ${preferredLastName} `;
  }
}
