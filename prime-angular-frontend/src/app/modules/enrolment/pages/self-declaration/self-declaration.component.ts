import { Component, OnInit } from '@angular/core';
import { Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { FormUtilsService } from '@enrolment/shared/services/form-utils.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { ProgressStatus } from '@enrolment/shared/enums/progress-status.enum';

@Component({
  selector: 'app-self-declaration',
  templateUrl: './self-declaration.component.html',
  styleUrls: ['./self-declaration.component.scss']
})
export class SelfDeclarationComponent extends BaseEnrolmentProfilePage implements OnInit {
  public decisions: { code: boolean, name: string }[];
  public hasAttemptedFormSubmission: boolean;
  public showUnansweredQuestionsError: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private enrolmentStateService: EnrolmentStateService,
    private formUtilsService: FormUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) {
    super(route, router, dialog);

    this.decisions = [
      { code: false, name: 'No' },
      { code: true, name: 'Yes' }
    ];
    this.hasAttemptedFormSubmission = false;
    this.showUnansweredQuestionsError = false;
  }

  public get hasConviction(): FormControl {
    return this.form.get('hasConviction') as FormControl;
  }

  public get hasConvictionDetails(): FormControl {
    return this.form.get('hasConvictionDetails') as FormControl;
  }

  public get hasRegistrationSuspended(): FormControl {
    return this.form.get('hasRegistrationSuspended') as FormControl;
  }

  public get hasRegistrationSuspendedDetails(): FormControl {
    return this.form.get('hasRegistrationSuspendedDetails') as FormControl;
  }

  public get hasDisciplinaryAction(): FormControl {
    return this.form.get('hasDisciplinaryAction') as FormControl;
  }

  public get hasDisciplinaryActionDetails(): FormControl {
    return this.form.get('hasDisciplinaryActionDetails') as FormControl;
  }

  public get hasPharmaNetSuspended(): FormControl {
    return this.form.get('hasPharmaNetSuspended') as FormControl;
  }

  public get hasPharmaNetSuspendedDetails(): FormControl {
    return this.form.get('hasPharmaNetSuspendedDetails') as FormControl;
  }

  public onSubmit() {
    this.hasAttemptedFormSubmission = true;

    if (this.form.valid) {
      const payload = this.enrolmentStateService.enrolment;
      this.busy = this.enrolmentResource.updateEnrollee(payload)
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Self declaration has been saved');
            this.form.markAsPristine();
            this.routeTo(EnrolmentRoutes.OVERVIEW);
          },
          (error: any) => {
            this.toastService.openErrorToast('Self declaration could not be saved');
            this.logger.error('[Enrolment] SelfDeclaration::onSubmit error has occurred: ', error);
          });
    } else {
      this.form.markAllAsTouched();
      this.showUnansweredQuestionsError = this.showUnansweredQuestions();
    }
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  protected createFormInstance() {
    this.form = this.enrolmentStateService.selfDeclarationForm;
  }

  protected initForm() {
    // TODO make YES/NO into own component to encapsulate toggling and markup
    this.hasConviction.valueChanges
      .subscribe((value: boolean) => {
        this.toggleSelfDeclarationValidators(value, this.hasConvictionDetails);
        this.showUnansweredQuestionsError = this.showUnansweredQuestions();
      });

    this.hasRegistrationSuspended.valueChanges
      .subscribe((value: boolean) => {
        this.toggleSelfDeclarationValidators(value, this.hasRegistrationSuspendedDetails);
        this.showUnansweredQuestionsError = this.showUnansweredQuestions();
      });

    this.hasDisciplinaryAction.valueChanges
      .subscribe((value: boolean) => {
        this.toggleSelfDeclarationValidators(value, this.hasDisciplinaryActionDetails);
        this.showUnansweredQuestionsError = this.showUnansweredQuestions();
      });

    this.hasPharmaNetSuspended.valueChanges
      .subscribe((value: boolean) => {
        this.toggleSelfDeclarationValidators(value, this.hasPharmaNetSuspendedDetails);
        this.showUnansweredQuestionsError = this.showUnansweredQuestions();
      });
  }

  protected patchForm() {
    const enrolment = this.enrolmentService.enrolment;

    this.isProfileComplete = enrolment.profileCompleted;
    this.enrolmentStateService.enrolment = enrolment;
    this.isInitialEnrolment = enrolment.progressStatus !== ProgressStatus.FINISHED;
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
      shouldShowUnansweredQuestions = this.hasConviction.value === null
        || this.hasRegistrationSuspended.value === null
        || this.hasDisciplinaryAction.value === null
        || this.hasPharmaNetSuspended.value === null;
    }

    return shouldShowUnansweredQuestions;
  }
}
