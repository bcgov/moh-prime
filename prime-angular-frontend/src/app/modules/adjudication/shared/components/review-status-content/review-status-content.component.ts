import { Component, OnInit, Input, OnChanges, SimpleChanges } from '@angular/core';

import { selfDeclarationQuestions } from '@lib/data/self-declaration-questions';
import { UtilsService } from '@core/services/utils.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolmentStatusReason } from '@shared/models/enrolment-status-reason.model';
import { EnrolmentStatusReason as EnrolmentStatusReasonEnum } from '@shared/enums/enrolment-status-reason.enum';
import { EnrolmentStatus } from '@shared/models/enrolment-status.model';
import { EnrolmentStatusEnum as EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';

import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';
import { ConfigCodePipe } from '@config/config-code.pipe';

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
    public documents?: BaseDocument[],
    public isSelfDeclaration?: boolean,
    public question?: string,
  ) { }
}

@Component({
  selector: 'app-review-status-content',
  templateUrl: './review-status-content.component.html',
  styleUrls: ['./review-status-content.component.scss']
})
export class ReviewStatusContentComponent implements OnInit, OnChanges {
  @Input() public enrollee: HttpEnrollee;
  @Input() public hideStatusHistory: boolean;
  public previousStatuses: Status[];
  public reasons: Reason[];
  private questions: { [key: number]: string } = selfDeclarationQuestions;

  constructor(
    private utilsService: UtilsService,
    private enrolmentResource: EnrolmentResource,
    private configPipe: ConfigCodePipe
  ) {
    this.hideStatusHistory = false;
  }

  public downloadDocument(document: BaseDocument, isSelfDeclaration: boolean): void {
    if (isSelfDeclaration) {
      return this.downloadSelfDeclarationDocument(document.id);
    }
    this.downloadIdentificationDocument(document.id);
  }

  private downloadSelfDeclarationDocument(id: number): void {
    this.enrolmentResource.getDownloadTokenSelfDeclarationDocument(this.enrollee.id, id)
      .subscribe((token: string) => this.utilsService.downloadToken(token));
  }

  private downloadIdentificationDocument(id: number): void {
    this.enrolmentResource.getDownloadTokenIdentificationDocument(this.enrollee.id, id)
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
          this.configPipe.transform(enrolmentStatus.statusCode, 'statuses'),
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

        if (esr.statusReasonCode === EnrolmentStatusReasonEnum.IDENTITY_PROVIDER) {
          return reasons.concat(new Reason(this.configPipe.transform(esr.statusReasonCode, 'statusReasons'), esr.reasonNote, this.enrollee.identificationDocuments));
        }

        reasons.push(new Reason(this.configPipe.transform(esr.statusReasonCode, 'statusReasons'), esr.reasonNote));
        return reasons;
      }, []);
  }

  private parseSelfDeclarations(enrollee: HttpEnrollee): Reason[] {
    return enrollee.selfDeclarations
      .reduce((selfDeclarations, selfDeclaration: SelfDeclaration) => {
        selfDeclarations.push(new Reason(
          'User answered yes to a self-declaration question:',
          selfDeclaration.selfDeclarationDetails,
          this.getDocumentsForSelfDeclaration(enrollee, selfDeclaration.selfDeclarationTypeCode),
          true,
          this.questions[selfDeclaration.selfDeclarationTypeCode]
        ));
        return selfDeclarations;
      }, []);
  }

  private getDocumentsForSelfDeclaration(enrollee: HttpEnrollee, code: SelfDeclarationTypeEnum): SelfDeclarationDocument[] {
    return enrollee.selfDeclarationDocuments.filter(d => d.selfDeclarationTypeCode === code);
  }
}
