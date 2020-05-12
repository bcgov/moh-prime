import { Pipe, PipeTransform } from '@angular/core';
import { EnrolmentStatus } from '@shared/models/enrolment-status.model';
import { Enrolment } from '@shared/models/enrolment.model';

export class Status {
  public name: string;
  public code: number;
  public date: string;
}

@Pipe({
  name: 'statuses'
})
export class StatusesPipe implements PipeTransform {

  // Returns the list of statuses in decending order
  public transform(enrollee: Enrolment): Status[] {
    if (!enrollee || !enrollee.enrolmentStatuses) {
      return [];
    }
    return enrollee.enrolmentStatuses.reduce((acc: Status[], es: EnrolmentStatus) => {
      const status = new Status();
      status.name = es.status.name;
      status.code = es.statusCode;
      status.date = es.statusDate;
      acc.push(status);
      return acc;
    }, []).reverse();
  }

}
