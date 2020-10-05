import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';

import { UtilsService } from '@core/services/utils.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolmentStatusReason } from '@shared/models/enrolment-status-reason.model';
import { EnrolmentStatusReason as EnrolmentStatusReasonEnum } from '@shared/enums/enrolment-status-reason.enum';
import { EnrolmentStatus } from '@shared/models/enrolment-status.model';
import { EnrolmentStatus as EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';

import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';

class Status {
  constructor(
    public date: string,
    public name: string,
    public code: number,
    public reasons: Reason[],
    public note?: string,
    public adjudicator?: string
  ) { }
}

class Reason {
  constructor(
    public name: string,
    public note: string,
    public isSelfDeclaration?: boolean,
    public question?: string,
    public documents?: SelfDeclarationDocument[]
  ) { }
}

@Component({
  selector: 'app-review-status-content',
  templateUrl: './review-status-content.component.html',
  styleUrls: ['./review-status-content.component.scss']
})
export class ReviewStatusContentComponent implements OnInit, OnChanges {
  @Input() public enrollee: HttpEnrollee;
  public previousStatuses: Status[];
  public reasons: Reason[];

  // TODO: Currenty we just store this in this place and in the self declaration form
  // and should be centralize for reuse so it doesn't have to changed in multiple places
  private questions: { [key: number]: string } = {
    [SelfDeclarationTypeEnum.HAS_CONVICTION]: `Are you, or have you ever been, the subject of an order or a
    conviction under legislation in any jurisdiction for a matter that involved improper access to, collection,
    use, or disclosure of personal information?`,
    [SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED]: `Are you, or have you ever been, subject to any limits,
    conditions or prohibitions imposed as a result of disciplinary actions taken by a governing body of a health
    profession in any jurisdiction, that involved improper access to, collection, use, or disclosure of personal
    information?`,
    [SelfDeclarationTypeEnum.HAS_DISCIPLINARY_ACTION]: `Have you ever had your access to an electronic health record
    system, electronic medical record system, pharmacy or laboratory record system, or any similar health information
    system, in any jurisdiction, suspended or cancelled?`,
    [SelfDeclarationTypeEnum.HAS_PHARMANET_SUSPENDED]: `Have you ever been disciplined or fired by an employer, or had
    a contract for your services terminated, for a matter that involved improper access to, collection, use, or
    disclosure of personal information?`,
  };

  constructor(
    private utilsService: UtilsService,
    private enrolmentResource: EnrolmentResource,
  ) { }

  public downloadDocument(document: SelfDeclarationDocument): void {
    this.enrolmentResource.getDownloadTokenSelfDeclarationDocument(this.enrollee.id, document.id)
      .subscribe((token: string) => this.utilsService.downloadToken(token));
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if (changes.enrollee) {
      this.enrollee = changes.enrollee.currentValue;
      this.reasons = this.generateReasons(this.enrollee);
      this.previousStatuses = this.generatePreviousStatuses(this.enrollee);
    }
  }

  public ngOnInit(): void { }

  private generatePreviousStatuses(enrollee: HttpEnrollee): Status[] {
    if (!enrollee) {
      return [];
    }
    return enrollee.enrolmentStatuses
      .reduce((statuses: Status[], enrolmentStatus: EnrolmentStatus) => {
        const status = new Status(
          enrolmentStatus.statusDate,
          enrolmentStatus.status.name,
          enrolmentStatus.statusCode,
          this.parseReasons(enrolmentStatus)
        );
        const reference = enrolmentStatus?.enrolmentStatusReference;
        if (reference) {
          status.adjudicator = (reference.adjudicator) ? reference.adjudicator.idir : '';
          status.note = (reference.adjudicatorNote) ? reference.adjudicatorNote.note : '';
        }
        statuses.push(status);
        return statuses;
      }, []).reverse();
  }

  private generateReasons(enrollee: HttpEnrollee): Reason[] {
    return (enrollee?.currentStatus.statusCode === EnrolmentStatusEnum.UNDER_REVIEW)
      ? this.parseReasons(enrollee.currentStatus)
      : [];
  }

  private parseReasons(enrolmentStatus: EnrolmentStatus): Reason[] {
    if (!enrolmentStatus || !enrolmentStatus.enrolmentStatusReasons) {
      return [];
    }

    return enrolmentStatus.enrolmentStatusReasons
      .reduce((reasons: Reason[], esr: EnrolmentStatusReason) => {
        if (esr.statusReasonCode === EnrolmentStatusReasonEnum.SELF_DECLARATION) {
          return reasons.concat(this.parseSelfDeclarations(this.enrollee));
        }

        reasons.push(new Reason(esr.statusReason.name, esr.reasonNote));
        return reasons;
      }, []);
  }

  private parseSelfDeclarations(enrollee: HttpEnrollee): Reason[] {
    return enrollee.selfDeclarations
      .reduce((selfDeclarations, selfDeclaration: SelfDeclaration) => {
        selfDeclarations.push(new Reason(
          'User answered yes to a self-declaration question:',
          selfDeclaration.selfDeclarationDetails,
          true,
          this.questions[selfDeclaration.selfDeclarationTypeCode],
          this.getDocumentsForSelfDeclaration(enrollee, selfDeclaration.selfDeclarationTypeCode)
        ));
        return selfDeclarations;
      }, []);
  }

  private getDocumentsForSelfDeclaration(enrollee: HttpEnrollee, code: SelfDeclarationTypeEnum): SelfDeclarationDocument[] {
    return enrollee.selfDeclarationDocuments.filter(d => d.selfDeclarationTypeCode === code);
  }
}
