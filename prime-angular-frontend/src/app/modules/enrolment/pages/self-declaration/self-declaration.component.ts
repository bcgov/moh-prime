import { Component, OnInit } from '@angular/core';
import { Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';

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
    protected enrolmentService: EnrolmentService,
    protected enrolmentStateService: EnrolmentStateService,
    protected enrolmentResource: EnrolmentResource,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    private formUtilsService: FormUtilsService
  ) {
    super(route, router, dialog, enrolmentService, enrolmentResource, enrolmentStateService, toastService, logger, utilService);

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
    const hasBeenThroughTheWizard = true;
    this.hasAttemptedFormSubmission = true;
    super.onSubmit(hasBeenThroughTheWizard);
  }

  public onHasConvictionUpload(sdd: SelfDeclarationDocument) {
    this.createSelfDeclarationDocument(SelfDeclarationTypeEnum.HAS_CONVICTION, sdd);
  }

  public onHasRegistrationSuspendedUpload(sdd: SelfDeclarationDocument) {
    this.createSelfDeclarationDocument(SelfDeclarationTypeEnum.HAS_REGISTRATION_SUSPENDED, sdd);
  }

  public onHasDisciplinaryActionUpload(sdd: SelfDeclarationDocument) {
    this.createSelfDeclarationDocument(SelfDeclarationTypeEnum.HAS_DISCIPLINARY_ACTION, sdd);
  }

  public onHasPharmanetSuspendedUpload(sdd: SelfDeclarationDocument) {
    this.createSelfDeclarationDocument(SelfDeclarationTypeEnum.HAS_PHARMANET_SUSPENDED, sdd);
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

  protected onSubmitFormIsInvalid() {
    this.showUnansweredQuestionsError = this.showUnansweredQuestions();
  }

  private createSelfDeclarationDocument(code: SelfDeclarationTypeEnum, sdd: SelfDeclarationDocument) {
    const enrolleeId = this.enrolmentService.enrolment.id;
    this.enrolmentResource
      .createSelfDeclarationDocument(enrolleeId, code, sdd)
      .subscribe();
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
