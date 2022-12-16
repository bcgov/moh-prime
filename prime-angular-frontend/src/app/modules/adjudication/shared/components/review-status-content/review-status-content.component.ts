import { Component, OnInit, Input, OnChanges, SimpleChanges, Output, EventEmitter } from '@angular/core';

import { ConfigCodePipe } from '@config/config-code.pipe';
import { selfDeclarationQuestions } from '@lib/data/self-declaration-questions';
import { RoutePath } from '@lib/utils/route-utils.class';
import { DISPLAY_ID_OFFSET } from '@lib/constants';
import { UtilsService } from '@core/services/utils.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolmentStatusReason } from '@shared/models/enrolment-status-reason.model';
import { EnrolmentStatusReason as EnrolmentStatusReasonEnum } from '@shared/enums/enrolment-status-reason.enum';
import { EnrolmentStatus } from '@shared/models/enrolment-status.model';
import { EnrolmentStatusEnum as EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { EnrolleeReviewStatus } from '@shared/models/enrollee-review-status.model';

import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

export class Status {
  constructor(
    public date: string,
    public name: string,
    public code: number,
    public reasons: Reason[],
    public note?: string,
    public adjudicator?: string
  ) { }
}

export class Reason {
  constructor(
    public name: string,
    public note: string,
    public documents?: BaseDocument[],
    public isSelfDeclaration?: boolean,
    public question?: string,
    public potentialMatchIds?: number[],
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
  @Output() public route: EventEmitter<RoutePath>;
  public previousStatuses: Status[];
  public reasons: Reason[];
  private questions: { [key: number]: string } = selfDeclarationQuestions;
  private enrolleeReviewStatus: EnrolleeReviewStatus;

  public AdjudicationRoutes = AdjudicationRoutes;
  public DISPLAY_ID_OFFSET = DISPLAY_ID_OFFSET;

  constructor(
    private utilsService: UtilsService,
    private enrolmentResource: EnrolmentResource,
    private adjudicationResource: AdjudicationResource,
    private configPipe: ConfigCodePipe
  ) {
    this.hideStatusHistory = false;
    this.route = new EventEmitter<RoutePath>();
  }

  public downloadDocument(document: BaseDocument, isSelfDeclaration: boolean): void {
    if (isSelfDeclaration) {
      return this.downloadSelfDeclarationDocument(document.id);
    }
    this.downloadIdentificationDocument(document.id);
  }

  public ngOnChanges(changes: SimpleChanges): void {
    if (changes.enrollee) {
      this.enrollee = changes.enrollee.currentValue;
      this.getEnrolleeReviewStatus(this.enrollee);
    }
  }

  public onRoute(routePath: RoutePath, event: Event): void {
    event?.preventDefault();
    this.route.emit(routePath);
  }

  public ngOnInit(): void { }

  private getEnrolleeReviewStatus(enrollee: HttpEnrollee): void {
    if (!enrollee?.id) {
      return;
    }
    this.adjudicationResource.getEnrolleeReviewStatus(enrollee.id)
      .subscribe((reviewStatus: EnrolleeReviewStatus) => {
        this.enrolleeReviewStatus = reviewStatus;
        this.reasons = this.generateReasons(this.enrollee);
        this.previousStatuses = this.generatePreviousStatuses(this.enrolleeReviewStatus);
      });
  }

  private downloadSelfDeclarationDocument(id: number): void {
    this.enrolmentResource.getDownloadTokenSelfDeclarationDocument(this.enrollee.id, id)
      .subscribe((token: string) => this.utilsService.downloadToken(token));
  }

  private downloadIdentificationDocument(id: number): void {
    this.enrolmentResource.getDownloadTokenIdentificationDocument(this.enrollee.id, id)
      .subscribe((token: string) => this.utilsService.downloadToken(token));
  }

  private generatePreviousStatuses(reviewStatus: EnrolleeReviewStatus): Status[] {
    if (!reviewStatus) {
      return [];
    }
    return reviewStatus.enrolmentStatuses
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
          const selfDeclReasons = this.parseSelfDeclarations(this.enrollee, this.enrolleeReviewStatus);
          if (selfDeclReasons.length > 0) {
            return reasons.concat(selfDeclReasons);
          }
        }

        const statusReason = this.configPipe.transform(esr.statusReasonCode, 'statusReasons');

        if (esr.statusReasonCode === EnrolmentStatusReasonEnum.IDENTITY_PROVIDER) {
          return reasons.concat(new Reason(statusReason, esr.reasonNote, this.enrolleeReviewStatus.identificationDocuments));
        }

        const reason = new Reason(statusReason, esr.reasonNote);

        reasons.push(
          (esr.statusReasonCode === EnrolmentStatusReasonEnum.POSSIBLE_PAPER_ENROLMENT_MATCH)
            ? this.parsePotentialMatchIds(reason)
            : reason
        );

        return reasons;
      }, []);
  }

  private parseSelfDeclarations(enrollee: HttpEnrollee, reviewStatus: EnrolleeReviewStatus): Reason[] {
    return enrollee.selfDeclarations
      .reduce((selfDeclarations, selfDeclaration: SelfDeclaration) => {
        if (!selfDeclaration.answered) {
          return selfDeclarations;
        }
        selfDeclarations.push(new Reason(
          'User answered yes to a self-declaration question:',
          selfDeclaration.selfDeclarationDetails,
          this.getDocumentsForSelfDeclaration(reviewStatus, selfDeclaration.selfDeclarationTypeCode),
          true,
          this.questions[selfDeclaration.selfDeclarationTypeCode]
        ));
        return selfDeclarations;
      }, []);
  }

  private getDocumentsForSelfDeclaration(reviewStatus: EnrolleeReviewStatus, code: SelfDeclarationTypeEnum): SelfDeclarationDocument[] {
    return reviewStatus.selfDeclarationDocuments.filter(d => d.selfDeclarationTypeCode === code);
  }

  private parsePotentialMatchIds(reason: Reason): Reason {
    const lastColon = reason.note.lastIndexOf(':');

    // Get the ids and parse them to numbers since we want to show the display ID which is id + 1000
    reason.potentialMatchIds = reason.note.substring(lastColon).match(/\d+/g).map((id) => parseInt(id));

    // Remove the ids from the status reason so we can add them back as clickable DisplayIds hyper links
    reason.note = reason.note.substring(0, lastColon + 1);

    return reason;
  }
}
