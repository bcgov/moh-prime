import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { BaseDocument } from '@shared/components/document-upload/document-upload/document-upload.component';
import { AuthService } from '@auth/shared/services/auth.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-id-submission',
  templateUrl: './id-submission.component.html',
  styleUrls: ['./id-submission.component.scss']
})
export class IdSubmissionComponent extends BaseEnrolmentProfilePage implements OnInit {
  public uploadedFile: boolean;
  public hasNoIdentificationDocumentError: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: ConsoleLoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    protected authService: AuthService
  ) {
    super(
      route,
      router,
      dialog,
      enrolmentService,
      enrolmentResource,
      enrolmentFormStateService,
      toastService,
      logger,
      utilService,
      formUtilsService,
      authService
    );

    this.uploadedFile = false;
  }

  public get identificationDocumentGuid(): FormControl {
    return this.form.get('identificationDocumentGuid') as FormControl;
  }

  public onUpload(document: BaseDocument) {
    this.identificationDocumentGuid.patchValue(document.documentGuid);
    this.uploadedFile = true;
    this.hasNoIdentificationDocumentError = false;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm().subscribe(() => this.initForm());
  }

  protected createFormInstance() {
    this.form = this.enrolmentFormStateService.identityDocumentForm;
  }

  protected initForm() { }

  protected handleSubmission(beenThroughTheWizard: boolean = false) {
    if (this.isInitialEnrolment) {
      // Allow routing to occur without invoking the deactivation,
      // modal to persist form state being dirty between views
      this.allowRoutingWhenDirty = true;
      this.nextRouteAfterSubmit();
    }
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoIdentificationDocumentError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    this.hasNoIdentificationDocumentError = true;
  }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = EnrolmentRoutes.BCEID_DEMOGRAPHIC;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }
}
