import { ChangeDetectionStrategy, Component, Input, OnChanges, OnInit, SimpleChanges } from '@angular/core';
import { UtilsService } from '@core/services/utils.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';

import { selfDeclarationQuestions } from '@lib/data/self-declaration-questions';
import { EnumUtils } from '@lib/utils/enum-utils.class';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';
import { SelfDeclaration } from '@shared/models/self-declarations.model';

interface SelfDeclarationComposite {
  selfDeclarationTypeCode: SelfDeclarationTypeEnum;
  selfDeclarationQuestion: string;
  selfDeclaration: SelfDeclaration;
  selfDeclarationDocuments: SelfDeclarationDocument[];
}

@Component({
  selector: 'app-enrollee-self-declarations',
  templateUrl: './enrollee-self-declarations.component.html',
  styleUrls: ['./enrollee-self-declarations.component.scss']
})
export class EnrolleeSelfDeclarationsComponent implements OnChanges {
  @Input() public enrolment: Enrolment;
  /**
   * @description
   * Show all self declaration questions regardless of whether
   * the enrollee made a declaration or not.
   */
  @Input() public showAllSelfDeclarationsQuestions: boolean;

  public selfDeclarationComposites: SelfDeclarationComposite[];

  constructor(
    private enrolmentResource: EnrolmentResource,
    private utilsService: UtilsService
  ) {
    this.showAllSelfDeclarationsQuestions = true;
  }

  public downloadSelfDeclarationDocument(documentId: number): void {
    this.enrolmentResource.getDownloadTokenSelfDeclarationDocument(this.enrolment.id, documentId)
      .subscribe((token: string) => this.utilsService.downloadToken(token));
  }

  public ngOnChanges(changes: SimpleChanges): void {
    const selfDeclarations = changes.enrolment.currentValue.selfDeclarations;
    this.selfDeclarationComposites = (selfDeclarations)
      ? this.createSelfDeclarationComposites()
      : [];
  }

  private createSelfDeclarationComposites() {
    const answered = this.enrolment.selfDeclarations
      .map(s => s.selfDeclarationTypeCode);
    const unanswered = (this.showAllSelfDeclarationsQuestions)
      ? EnumUtils.values(SelfDeclarationTypeEnum)
        .filter(type => !answered.includes(type))
        .map(type => this.createSelfDeclarationComposite(type))
      : [];

    return this.enrolment.selfDeclarations
      .map((selfDeclaration: SelfDeclaration) => {
        const selfDeclarationTypeCode = selfDeclaration.selfDeclarationTypeCode;
        const selfDeclarationDocuments = this.enrolment.selfDeclarationDocuments
          ?.filter(d => d.selfDeclarationTypeCode === selfDeclarationTypeCode) ?? [];

        return this.createSelfDeclarationComposite(selfDeclarationTypeCode, selfDeclaration, selfDeclarationDocuments);
      })
      .concat(unanswered)
      .sort((a, b) => a.selfDeclarationTypeCode - b.selfDeclarationTypeCode);
  }

  private createSelfDeclarationComposite(
    selfDeclarationTypeCode: number,
    selfDeclaration: SelfDeclaration = null,
    selfDeclarationDocuments: SelfDeclarationDocument[] = []
  ) {
    return {
      selfDeclarationTypeCode,
      selfDeclarationQuestion: selfDeclarationQuestions[selfDeclarationTypeCode],
      selfDeclaration,
      selfDeclarationDocuments
    };
  }
}
