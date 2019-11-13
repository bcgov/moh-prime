import { Pipe, PipeTransform } from '@angular/core';
import { Enrolment } from '@shared/models/enrolment.model';

@Pipe({
  name: 'enrolment'
})
export class EnrolmentPipe implements PipeTransform {

  transform(enrolment: Enrolment, display: string): string {
    const enrollee = enrolment.enrollee;

    if (enrollee) {
      switch (display) {
        case 'preferredName':
          return this.getPreferredName(enrollee);
        case 'fullName':
          return this.getFullName(enrollee);
      }
    }
    return null;
  }

  private getFullName(enrollee: any) {
    return ` ${enrollee.firstName ? enrollee.firstName : ''} ${enrollee.middleName ? enrollee.middleName : ''} ${enrollee.lastName ? enrollee.lastName : ''} `;
  }

  private getPreferredName(enrollee: any) {
    return ` ${enrollee.preferredFirstName ? enrollee.preferredFirstName : ''}
        ${enrollee.preferredMiddleName ? enrollee.preferredMiddleName : ''}
        ${ enrollee.preferredLastName ? enrollee.preferredLastName : ''} `;
  }
}
