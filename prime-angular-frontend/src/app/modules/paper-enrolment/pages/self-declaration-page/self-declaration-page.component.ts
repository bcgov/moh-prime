import { Component, OnInit } from '@angular/core';
import { Validators, FormControl, FormArray, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { selfDeclarationQuestions } from '@lib/data/self-declaration-questions';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';

import { AuthService } from '@auth/shared/services/auth.service';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { Enrolment } from '@shared/models/enrolment.model';
import { PaperEnrolmentFormStateService } from '@paper-enrolment/services/paper-enrolment-form-state.service';
import { PaperEnrolmentService } from '@paper-enrolment/services/paper-enrolment.service';
import { SelfDeclarationFormState } from './self-declaration-page-form-state.class';
import { NoContent } from '@core/resources/abstract-resource';
import { pipe } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';

@Component({
  selector: 'app-self-declaration-page',
  templateUrl: './self-declaration-page.component.html',
  styleUrls: ['./self-declaration-page.component.scss']
})
export class SelfDeclarationPageComponent extends AbstractEnrolmentPage implements OnInit {
  public form: FormGroup;
  public formState: SelfDeclarationFormState;
  public decisions: { code: boolean, name: string }[];
  public hasAttemptedFormSubmission: boolean;
  public showUnansweredQuestionsError: boolean;
  public SelfDeclarationTypeEnum = SelfDeclarationTypeEnum;
  public selfDeclarationQuestions = selfDeclarationQuestions;
  public routeUtils: RouteUtils;
  public enrolment: Enrolment;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected paperEnrolmentService: PaperEnrolmentService,
    protected paperEnrolmentFormStateService: PaperEnrolmentFormStateService,
    protected paperEnrolmentResource: PaperEnrolmentResource,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    protected authService: AuthService
  ) {
    super(dialog, formUtilsService);

    this.decisions = [
      { code: false, name: 'No' },
      { code: true, name: 'Yes' }
    ];
    this.hasAttemptedFormSubmission = false;
    this.showUnansweredQuestionsError = false;

    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public onSubmit(): void {
    this.nextRouteAfterSubmit();
    this.hasAttemptedFormSubmission = true;
    // if (this.formUtilsService.checkValidity(this.form)) {
    //   this.handleSubmission();
    // } else {
    //   this.utilService.scrollToErrorSection();
    // }
  }

  public onUpload(controlName: string, sdd: SelfDeclarationDocument) {
    this.addSelfDeclarationDocumentGuid(controlName, sdd.documentGuid);
  }

  public onRemove(constrolName: string, documentGuid: string) {
    this.removeSelfDeclarationDocumentGuid(constrolName, documentGuid);
  }

  public onBack() {
    const certifications = this.enrolment?.certifications;
    const backRoutePath = (certifications?.length)
      ? PaperEnrolmentRoutes.REGULATORY
      : PaperEnrolmentRoutes.OBO_SITES;

    // this.routeTo(['../', this.enrolment.id, backRoutePath]);
    this.routeUtils.routeRelativeTo(['../', '1', backRoutePath]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  protected createFormInstance(): void {
    this.formState = this.paperEnrolmentFormStateService.selfDeclarationFormState;
    this.form = this.formState.form;
  }

  protected initForm() {
    this.formState.hasConviction.valueChanges
      .subscribe((value: boolean) => {
        this.toggleSelfDeclarationValidators(value, this.formState.hasConvictionDetails);
        this.showUnansweredQuestionsError = this.showUnansweredQuestions();
      });

    this.formState.hasRegistrationSuspended.valueChanges
      .subscribe((value: boolean) => {
        this.toggleSelfDeclarationValidators(value, this.formState.hasRegistrationSuspendedDetails);
        this.showUnansweredQuestionsError = this.showUnansweredQuestions();
      });

    this.formState.hasDisciplinaryAction.valueChanges
      .subscribe((value: boolean) => {
        this.toggleSelfDeclarationValidators(value, this.formState.hasDisciplinaryActionDetails);
        this.showUnansweredQuestionsError = this.showUnansweredQuestions();
      });

    this.formState.hasPharmaNetSuspended.valueChanges
      .subscribe((value: boolean) => {
        this.toggleSelfDeclarationValidators(value, this.formState.hasPharmaNetSuspendedDetails);
        this.showUnansweredQuestionsError = this.showUnansweredQuestions();
      });
  }

  protected patchForm(): void {
    // Will be null if enrolment has not been created
    const enrollee = this.paperEnrolmentService.enrollee;
    this.formState.patchValue(enrollee);
  }

  protected performSubmission(): NoContent {
    // Update using the form which could contain changes, and ensure identity
    const enrolment = this.paperEnrolmentFormStateService.json;
    return this.paperEnrolmentResource.updateEnrollee(enrolment)
      .pipe(this.handleResponse());
  }

  protected onSubmitFormIsInvalid() {
    this.showUnansweredQuestionsError = this.showUnansweredQuestions();
  }

  protected afterSubmitIsSuccessful(): void {
    this.formState.clearSelfDeclarationDocumentGuids();
  }

  private toggleSelfDeclarationValidators(value: boolean, control: FormControl) {
    if (!value) {
      this.formUtilsService.resetAndClearValidators(control);
    } else {
      this.formUtilsService.setValidators(control, [Validators.required]);
    }
  }

  private showUnansweredQuestions(): boolean {
    let shouldShowUnansweredQuestions = false;

    if (this.hasAttemptedFormSubmission) {
      shouldShowUnansweredQuestions = this.formState.hasConviction.value === null
        || this.formState.hasRegistrationSuspended.value === null
        || this.formState.hasDisciplinaryAction.value === null
        || this.formState.hasPharmaNetSuspended.value === null;
    }

    return shouldShowUnansweredQuestions;
  }

  private addSelfDeclarationDocumentGuid(controlName: string, documentGuid: string) {
    this.formState.addSelfDeclarationDocumentGuid(this.form.get(controlName) as FormArray, documentGuid);
  }

  private removeSelfDeclarationDocumentGuid(controlName: string, documentGuid: string) {
    this.formState.removeSelfDeclarationDocumentGuid(this.form.get(controlName) as FormArray, documentGuid);
  }

  private handleResponse() {
    return pipe(
      map(() => {
        this.toastService.openSuccessToast('Enrolment information has been saved');
        this.form.markAsPristine();

        this.nextRouteAfterSubmit();
      }),
      catchError((error: any) => {
        this.toastService.openErrorToast('Enrolment information could not be saved');
        this.logger.error('[Enrolment] Submission error has occurred: ', error);

        throw error;
      })
    );
  }

  private nextRouteAfterSubmit(): void {
    // this.routeTo(['../', this.enrolment.id, PaperEnrolmentRoutes.OVERVIEW]);
    this.routeUtils.routeRelativeTo(['../', '1', PaperEnrolmentRoutes.OVERVIEW]);
  }
}
