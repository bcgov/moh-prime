import { Pipe, PipeTransform } from '@angular/core';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentStatusReason } from '@shared/models/enrolment-status-reason.model';
import { EnrolmentStatusReason as Enum } from '@shared/enums/enrolment-status-reason.enum';

export class Reason {
  public name: string;
  public note: string;
  public isSelfDeclaration: boolean;
  public question: string;
}

@Pipe({
  name: 'statusReasons'
})
export class StatusReasonsPipe implements PipeTransform {
  // TODO: Currenty we just store this in this place and in the self declaration form
  // Later on this would be best to be brought out and stored better!
  private registrationQ = 'Are you, or have you ever been, the subject of an order or a conviction under'
    + ' legislation in any jurisdiction for a matter that involved improper access to, collection,'
    + ' use, or disclosure of personal information?';
  private convictionQ = 'Are you, or have you ever been, subject to any limits, conditions'
    + ' or prohibitions imposed as a result of disciplinary actions taken by a governing body'
    + ' of a health profession in any jurisdiction, that involved improper access to, collection,'
    + ' use, or disclosure of personal information?';
  private pharmanetQ = 'Have you ever had your access to an electronic health record system,'
    + ' electronic medical record system, pharmacy or laboratory record system,'
    + ' or any similar health information system, in any jurisdiction, suspended or cancelled?';
  private disciplinaryQ = 'Have you ever been disciplined or fired by an employer, or had a contract for your services terminated,'
    + ' for a matter that involved improper access to, collection, use, or disclosure of personal information?';

  public transform(enrollee: Enrolment): Reason[] {
    // If not under review return []
    if (!enrollee || enrollee.currentStatus.statusCode !== 2) {
      return [];
    }
    return enrollee.currentStatus.enrolmentStatusReasons.reduce((acc: Reason[], esr: EnrolmentStatusReason) => {
      // Self declaration
      if (esr.statusReasonCode === 10) {
        return acc.concat(this.parseDeclarations(enrollee));
      }
      const reason = new Reason();
      reason.name = esr.statusReason.name;
      reason.note = esr.reasonNote;
      acc.push(reason);
      return acc;
    }, []);
  }

  private parseDeclarations(enrollee: Enrolment): Reason[] {
    const results = [];
    if (enrollee.hasConviction) {
      const conviction = new Reason();
      conviction.name = 'User Answered Yes to a Self Declaration Question:';
      conviction.isSelfDeclaration = true;
      conviction.note = enrollee.hasConvictionDetails;
      conviction.question = this.convictionQ;
      results.push(conviction);
    }

    if (enrollee.hasRegistrationSuspended) {
      const registationSuspended = new Reason();
      registationSuspended.name = 'User Answered Yes to a Self Declaration Question:';
      registationSuspended.isSelfDeclaration = true;
      registationSuspended.note = enrollee.hasRegistrationSuspendedDetails;
      registationSuspended.question = this.registrationQ;
      results.push(registationSuspended);
    }

    if (enrollee.hasDisciplinaryAction) {
      const disciplinaryAction = new Reason();
      disciplinaryAction.name = 'User Answered Yes to a Self Declaration Question:';
      disciplinaryAction.isSelfDeclaration = true;
      disciplinaryAction.note = enrollee.hasDisciplinaryActionDetails;
      disciplinaryAction.question = this.disciplinaryQ;
      results.push(disciplinaryAction);
    }

    if (enrollee.hasPharmaNetSuspended) {
      const pharmaNetSuspended = new Reason();
      pharmaNetSuspended.name = 'User Answered Yes to a Self Declaration Question:';
      pharmaNetSuspended.isSelfDeclaration = true;
      pharmaNetSuspended.note = enrollee.hasPharmaNetSuspendedDetails;
      pharmaNetSuspended.question = this.pharmanetQ;
      results.push(pharmaNetSuspended);
    }

    return results;
  }

}
