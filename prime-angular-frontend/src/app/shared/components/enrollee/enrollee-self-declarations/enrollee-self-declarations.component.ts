import { EventEmitter, Component, Input, OnChanges, OnInit, Output, SimpleChanges } from '@angular/core';
import { UtilsService } from '@core/services/utils.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { Router } from '@angular/router';

import { EnumUtils } from '@lib/utils/enum-utils.class';

import moment from 'moment';

import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';
import { SelfDeclaration } from '@shared/models/self-declarations.model';
import { SelfDeclarationVersion } from '@shared/models/self-declaration-version.model';

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
export class EnrolleeSelfDeclarationsComponent implements OnChanges, OnInit {
  @Input() public enrolment: Enrolment;
  /**
   * @description
   * Show the redirect icon.
   */
  @Input() public showRedirect: boolean;
  /**
   * @description
   * Route path for redirection.
   */
  @Input() public redirectRoutePath: string | string[];
  /**
   * @description
   * Emit route event when no redirect route path is provided.
   */
  @Output() public route: EventEmitter<void>;

  public selfDeclarationComposites: SelfDeclarationComposite[];
  public selfDeclarationQuestions = new Map<number, string>();

  constructor(
    private enrolmentResource: EnrolmentResource,
    private utilsService: UtilsService,
    private router: Router
  ) {
    this.route = new EventEmitter<void>();
  }

  public isAdjudication(): boolean {
    return this.router.url.includes('adjudication');
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

  public ngOnInit(): void {
    if (this.selfDeclarationQuestions.size === 0) {
      let targetDate = this.enrolment.selfDeclarationCompleteDate ?
        this.enrolment.selfDeclarationCompleteDate : this.enrolment.currentStatus.statusDate;
      this.enrolmentResource.getSelfDeclarationVersion(moment(targetDate).format()).subscribe(
        (versions) => {
          versions.forEach(v => {
            this.selfDeclarationQuestions.set(v.selfDeclarationTypeCode, v.text);
          });
          // re-construct the composites
          this.selfDeclarationComposites = this.createSelfDeclarationComposites();
        });
    }
  }

  private createSelfDeclarationComposites() {
    return this.enrolment.selfDeclarations
      .map((selfDeclaration: SelfDeclaration) => {
        const selfDeclarationTypeCode = selfDeclaration.selfDeclarationTypeCode;
        const selfDeclarationDocuments = this.enrolment.selfDeclarationDocuments
          ?.filter(d => d.selfDeclarationTypeCode === selfDeclarationTypeCode) ?? [];

        return this.createSelfDeclarationComposite(selfDeclarationTypeCode, selfDeclaration, selfDeclarationDocuments);
      })
      .sort((a, b) => a.selfDeclarationTypeCode - b.selfDeclarationTypeCode);
  }

  private createSelfDeclarationComposite(
    selfDeclarationTypeCode: number,
    selfDeclaration: SelfDeclaration = null,
    selfDeclarationDocuments: SelfDeclarationDocument[] = []
  ) {
    return {
      selfDeclarationTypeCode,
      selfDeclarationQuestion: this.selfDeclarationQuestions.get(selfDeclarationTypeCode),
      selfDeclaration,
      selfDeclarationDocuments
    };
  }

  public onRoute(routePath: string | string[]) {
    (this.redirectRoutePath)
      ? this.router.navigate(this.getRoutePath(routePath))
      : this.route.emit();
  }

  private getRoutePath(routePath: string | string[]) {
    return (Array.isArray(routePath))
      ? routePath
      : [routePath];
  }
}
