import { Component, OnInit, Input } from '@angular/core';

import { UtilsService } from '@core/services/utils.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { EnrolmentStatusReason } from '@shared/models/enrolment-status-reason.model';
import { EnrolmentStatus } from '@shared/models/enrolment-status.model';
import { EnrolmentStatus as EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';

import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';

class Status {
  public date: string;
  public name: string;
  public code: number;
  public note: string;
  public adjudicator: string;
  public reasons: Reason[];
}

class Reason {
  public name: string;
  public note: string;
  public isSelfDeclaration: boolean;
  public question: string;
  public documents: SelfDeclarationDocument[];
}

@Component({
  selector: 'app-review-status-content',
  templateUrl: './review-status-content.component.html',
  styleUrls: ['./review-status-content.component.scss']
})
export class ReviewStatusContentComponent implements OnInit {
  private _enrollee: Enrolment;
  public previousStatuses: Status[];
  public reasons: Reason[];

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

  constructor(
    private utilsService: UtilsService,
    private enrolmentResource: EnrolmentResource,
  ) { }

  @Input() public set enrollee(value: Enrolment) {
    this._enrollee = value;
    this.reasons = this.generateReasons();
    this.previousStatuses = this.generatePreviousStatuses();
  }

  public get enrollee(): Enrolment {
    return this._enrollee;
  }

  public downloadDocument(document: SelfDeclarationDocument) {
    this.enrolmentResource.getDownloadTokenSelfDeclarationDocument(this.enrollee.id, document.id)
      .subscribe((token: string) => {
        this.utilsService.downloadToken(token);
      });
  }

  public ngOnInit(): void { }

  private generatePreviousStatuses(): Status[] {
    if (!this.enrollee) {
      return [];
    }
    return this.enrollee.enrolmentStatuses.reduce((acc: Status[], es: EnrolmentStatus) => {
      const status = new Status();
      status.name = es.status.name;
      status.code = es.statusCode;
      status.date = es.statusDate;
      status.reasons = this.parseReasons(es);
      if (es.enrolmentStatusReference) {
        const reference = es.enrolmentStatusReference;
        status.adjudicator = (reference.adjudicator) ? reference.adjudicator.idir : '';
        status.note = (reference.adjudicatorNote) ? reference.adjudicatorNote.note : '';
      }
      acc.push(status);
      return acc;
    }, []).reverse();
  }

  private generateReasons(): Reason[] {
    if (!this.enrollee || this.enrollee.currentStatus.statusCode !== EnrolmentStatusEnum.UNDER_REVIEW) {
      return [];
    }

    return this.parseReasons(this.enrollee.currentStatus);
  }

  private parseReasons(enrolmentStatus: EnrolmentStatus): Reason[] {
    if (!enrolmentStatus || !enrolmentStatus.enrolmentStatusReasons) {
      return [];
    }

    return enrolmentStatus.enrolmentStatusReasons.reduce((acc: Reason[], esr: EnrolmentStatusReason) => {
      if (esr.statusReasonCode === 10) {
        return acc.concat(this.parseSelfDeclarations(this.enrollee));
      }
      const reason = new Reason();
      reason.name = esr.statusReason.name;
      reason.note = esr.reasonNote;
      acc.push(reason);

      return acc;
    }, []);
  }

  private getDocumentsForSelfDeclaration(enrollee: Enrolment, code: SelfDeclarationTypeEnum) {
    return enrollee.selfDeclarationDocuments.filter(d => d.selfDeclarationTypeCode === code);
  }

  private parseSelfDeclarations(enrollee: Enrolment): Reason[] {
    return enrollee.selfDeclarations.reduce((acc, decl: SelfDeclaration) => {
      if (decl.selfDeclarationTypeCode === SelfDeclarationTypeEnum.HAS_CONVICTION) {
        const conviction = new Reason();
        conviction.name = 'User Answered Yes to a Self Declaration Question:';
        conviction.isSelfDeclaration = true;
        conviction.note = decl.selfDeclarationDetails;
        conviction.question = this.convictionQ;
        conviction.documents = this.getDocumentsForSelfDeclaration(enrollee, SelfDeclarationTypeEnum.HAS_CONVICTION);
        acc.push(conviction);
      }

      if (decl.selfDeclarationTypeCode === SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED) {
        const registationSuspended = new Reason();
        registationSuspended.name = 'User Answered Yes to a Self Declaration Question:';
        registationSuspended.isSelfDeclaration = true;
        registationSuspended.note = decl.selfDeclarationDetails;
        registationSuspended.question = this.registrationQ;
        registationSuspended.documents = this.getDocumentsForSelfDeclaration(enrollee, SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED);
        acc.push(registationSuspended);
      }

      if (decl.selfDeclarationTypeCode === SelfDeclarationTypeEnum.HAS_DISCIPLINARY_ACTION) {
        const disciplinaryAction = new Reason();
        disciplinaryAction.name = 'User Answered Yes to a Self Declaration Question:';
        disciplinaryAction.isSelfDeclaration = true;
        disciplinaryAction.note = decl.selfDeclarationDetails;
        disciplinaryAction.question = this.disciplinaryQ;
        disciplinaryAction.documents = this.getDocumentsForSelfDeclaration(enrollee, SelfDeclarationTypeEnum.HAS_DISCIPLINARY_ACTION);
        acc.push(disciplinaryAction);
      }

      if (decl.selfDeclarationTypeCode === SelfDeclarationTypeEnum.HAS_PHARMANET_SUSPENDED) {
        const pharmaNetSuspended = new Reason();
        pharmaNetSuspended.name = 'User Answered Yes to a Self Declaration Question:';
        pharmaNetSuspended.isSelfDeclaration = true;
        pharmaNetSuspended.note = decl.selfDeclarationDetails;
        pharmaNetSuspended.question = this.pharmanetQ;
        pharmaNetSuspended.documents = this.getDocumentsForSelfDeclaration(enrollee, SelfDeclarationTypeEnum.HAS_PHARMANET_SUSPENDED);
        acc.push(pharmaNetSuspended);
      }

      return acc;
    }, []);
  }
}
