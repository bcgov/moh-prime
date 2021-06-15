import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FormControl, FormGroup } from '@angular/forms';

import { exhaustMap } from 'rxjs/operators';
import { EMPTY, Observable } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { ViewportService } from '@core/services/viewport.service';
import { EnrolleeAgreement } from '@shared/models/agreement.model';
import { EnrolleeStatusAction } from '@shared/enums/enrollee-status-action.enum';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { EnrolleeClassification } from '@shared/enums/enrollee-classification.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';

import { AuthService } from '@auth/shared/services/auth.service';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/enrolment-page.class';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';

@Component({
  selector: 'app-access-agreement',
  templateUrl: './access-agreement.component.html',
  styleUrls: ['./access-agreement.component.scss']
})
export class AccessAgreementComponent extends BaseEnrolmentPage implements OnInit {
  public enrolment: Enrolment;
  public form: FormGroup;
  public currentPage: number;
  public hasReadAgreement: boolean;
  public agreed: FormControl;

  public hasAcceptedAgreement: boolean;
  public hasDownloadedFile: boolean;
  public hasUploadedFile: boolean;
  public hasNoUploadError: boolean;

  // Allow the use of enum in the component template
  public EnrolmentStatus = EnrolmentStatus;
  public EnrolleeClassification = EnrolleeClassification;
  public IdentityProviderEnum = IdentityProviderEnum;

  public accessTerm: EnrolleeAgreement;
  public identityProvider: IdentityProviderEnum;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private dialog: MatDialog,
    private changeDetectorRef: ChangeDetectorRef,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private enrolmentFormStateService: EnrolmentFormStateService,
    private toastService: ToastService,
    private utilsService: UtilsService,
    private viewportService: ViewportService,
    private logger: LoggerService,
    private authService: AuthService
  ) {
    super(route, router);

    this.currentPage = 0;
    this.hasReadAgreement = false;
    this.agreed = new FormControl(false);
  }

  public get accessAgreementGuid(): FormControl {
    return this.form.get('accessAgreementGuid') as FormControl;
  }

  public get isMobile(): boolean {
    return this.viewportService.isMobile;
  }

  public get hasAgreed(): boolean {
    return this.agreed.value;
  }

  public onSubmit(isAcceptingToa: boolean = false) {
    if (this.hasReadAgreement) {
      const status = (isAcceptingToa)
        ? { verb: 'Accept', adjective: 'accepted' }
        : { verb: 'Decline', adjective: 'declined' };

      const data: DialogOptions = {
        title: 'Terms of Access',
        message: `Are you sure you want to ${status.verb.toLowerCase()} the Terms of Access?`,
        actionText: `${status.verb} Terms of Access`,
        actionType: (!isAcceptingToa) ? 'warn' : 'primary'
      };
      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? this.enrolmentResource.enrolleeStatusAction(
                this.enrolment.id,
                isAcceptingToa ? EnrolleeStatusAction.ACCEPT_TOA : EnrolleeStatusAction.DECLINE_TOA,
                this.accessAgreementGuid.value
              )
              : EMPTY
          )
        )
        .subscribe(() => {
          this.toastService.openSuccessToast(`Terms of Access has been ${status.adjective}`);
          this.routeTo(EnrolmentRoutes.PHARMANET_ENROLMENT_SUMMARY, {
            state: { showProgressBar: this.isInitialEnrolment }
          });
        });
    }
  }

  public onPrevPage() {
    if (this.currentPage > 0) {
      this.utilsService.scrollTop();
      this.currentPage--;
      this.hasReadAgreement = false;
      this.agreed.reset(false);
    }
  }

  public onNextPage() {
    if (!this.hasReadAgreement) {
      this.utilsService.scrollTop();
      this.currentPage++;

      this.onPageChange({ atEnd: true });
    }
  }

  public onPageChange(agreement: { atEnd: boolean }) {
    if (agreement.atEnd) {
      this.hasReadAgreement = agreement.atEnd;
      this.changeDetectorRef.detectChanges();
    }
  }

  public onDownload() {
    this.enrolmentResource
      .getAccessTermSignable(this.enrolment.id, this.accessTerm.id)
      .subscribe((base64: string) => {
        const blob = this.utilsService.base64ToBlob(base64);
        this.utilsService.downloadDocument(blob, 'Terms-Of-Access');
        this.hasDownloadedFile = true;
      });
  }

  public onUpload(document: BaseDocument) {
    this.accessAgreementGuid.patchValue(document.documentGuid);
    this.hasUploadedFile = true;
    this.hasNoUploadError = false;
  }

  public onRemoveDocument(documentGuid: string) {
    this.accessAgreementGuid.patchValue(null);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.enrolmentFormStateService.accessAgreementForm;
  }

  private initForm() {
    this.enrolment = this.enrolmentService.enrolment;
    this.isInitialEnrolment = this.enrolmentService.isInitialEnrolment;
    this.authService.identityProvider$()
      .subscribe((identityProvider: IdentityProviderEnum) => this.identityProvider = identityProvider);
    this.busy = this.enrolmentResource.getLatestAccessTerm(this.enrolment.id, false)
      .subscribe((accessTerm: EnrolleeAgreement) => this.accessTerm = accessTerm);
  }
}
