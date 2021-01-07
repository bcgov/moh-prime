import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { AuthService } from '@auth/shared/services/auth.service';
import { UtilsService } from '@core/services/utils.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';

import { selfDeclarationQuestions } from '@lib/data/self-declaration-questions';
import { EnumUtils } from '@lib/utils/enum-utils.class';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';
import { SelfDeclaration } from '@shared/models/self-declarations.model';

interface SelfDeclarationComposite {
  selfDeclarationTypeCode: SelfDeclarationTypeEnum,
  selfDeclarationQuestion: string,
  selfDeclaration: SelfDeclaration,
  selfDeclarationDocuments: SelfDeclarationDocument[]
}

@Component({
  selector: 'app-enrollee-self-declarations',
  templateUrl: './enrollee-self-declarations.component.html',
  styleUrls: ['./enrollee-self-declarations.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EnrolleeSelfDeclarationsComponent implements OnInit {
  @Input() public enrolment: Enrolment;

  public isEnrollee: boolean;
  public selfDeclarationComposites: SelfDeclarationComposite[];

  constructor(
    private enrolmentResource: EnrolmentResource,
    private utilsService: UtilsService,
    private authService: AuthService
  ) {
    this.isEnrollee = this.authService.isEnrollee();
  }

  public downloadSelfDeclarationDocument(documentId: number): void {
    this.enrolmentResource.getDownloadTokenSelfDeclarationDocument(this.enrolment.id, documentId)
      .subscribe((token: string) => this.utilsService.downloadToken(token));
  }

  public ngOnInit(): void {
    this.createSelfDeclarationComposites();
  }

  private createSelfDeclarationComposites() {
    console.log('WHAT', this.enrolment);


    const answered = this.enrolment.selfDeclarations
      .map(s => s.selfDeclarationTypeCode);
    const unanswered = (this.isEnrollee)
      ? EnumUtils.values(SelfDeclarationTypeEnum)
        .filter(type => !answered.includes(type))
        .map(type => this.createSelfDeclarationComposite(type))
      : [];

    this.selfDeclarationComposites = this.enrolment.selfDeclarations
      .map((selfDeclaration: SelfDeclaration) => {
        const selfDeclarationTypeCode = selfDeclaration.selfDeclarationTypeCode;
        const selfDeclarationDocuments = this.enrolment.selfDeclarationDocuments
          .filter(d => d.selfDeclarationTypeCode === selfDeclarationTypeCode);

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
