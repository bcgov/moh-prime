import { Pipe, PipeTransform } from '@angular/core';
import { Enrolment } from '@shared/models/enrolment.model';

@Pipe({
  name: 'enrolment'
})
export class EnrolmentPipe implements PipeTransform {

  transform(enrolment: Enrolment, display: string): string {
    if (enrolment) {
      const enrollee = enrolment.enrollee;

      if (enrollee) {
        switch (display) {
          case 'preferredName':
            return this.getPreferredName(enrollee);
          case 'fullName':
            return this.getFullName(enrollee);
        }
      }
    }

    return null;
  }

  private getFullName(enrollee: any) {
    const firstName = enrollee.firstName ? enrollee.firstName : '';
    const middleName = enrollee.middleName ? enrollee.middleName : '';
    const lastName = enrollee.lastName ? enrollee.lastName : '';
    return ` ${firstName} ${middleName} ${lastName} `;
  }

  private getPreferredName(enrollee: any) {
    return ` ${enrollee.preferredFirstName ? enrollee.preferredFirstName : ''}
        ${enrollee.preferredMiddleName ? enrollee.preferredMiddleName : ''}
        ${ enrollee.preferredLastName ? enrollee.preferredLastName : ''} `;
  }
}
