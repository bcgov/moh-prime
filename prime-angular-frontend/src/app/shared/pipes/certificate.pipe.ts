import { Pipe, PipeTransform } from '@angular/core';
import { EnrolmentCertificate } from 'app/modules/enrolment-certificate/shared/models/enrolment-certificate.model';

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
    const firstName = certificate.firstName ? certificate.firstName : '';
    const middleName = certificate.middleName ? certificate.middleName : '';
    const lastName = certificate.lastName ? certificate.lastName : '';
    return ` ${firstName} ${middleName} ${lastName} `;
  }

  private getPreferredName(certificate: any) {
    return ` ${certificate.preferredFirstName ? certificate.preferredFirstName : ''}
        ${certificate.preferredMiddleName ? certificate.preferredMiddleName : ''}
        ${ certificate.preferredLastName ? certificate.preferredLastName : ''} `;
  }
}
