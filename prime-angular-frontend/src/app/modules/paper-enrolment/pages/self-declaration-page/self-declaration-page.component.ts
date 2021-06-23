import { Component, OnInit } from '@angular/core';
import { Validators, FormControl, FormArray, FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { map } from 'rxjs/operators';

import { selfDeclarationQuestions } from '@lib/data/self-declaration-questions';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { NoContent } from '@core/resources/abstract-resource';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { SelfDeclarationTypeEnum } from '@shared/enums/self-declaration-type.enum';
import { SelfDeclarationDocument } from '@shared/models/self-declaration-document.model';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentService } from '@paper-enrolment/services/paper-enrolment.service';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { PaperEnrolmentFormStateService } from '@paper-enrolment/services/paper-enrolment-form-state.service';
import { SelfDeclarationFormState } from './self-declaration-form-state.class';

@Component({
  selector: 'app-self-declaration-page',
  templateUrl: './self-declaration-page.component.html',
  styleUrls: ['./self-declaration-page.component.scss']
})
export class SelfDeclarationPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: SelfDeclarationFormState;
  public allowUploads: boolean;
  public enrollee: HttpEnrollee;
  public decisions: { code: boolean, name: string }[];
  public hasAttemptedFormSubmission: boolean;
  public showUnansweredQuestionsError: boolean;
  public SelfDeclarationTypeEnum = SelfDeclarationTypeEnum;
  public selfDeclarationQuestions = selfDeclarationQuestions;
  public routeUtils: RouteUtils;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private paperEnrolmentService: PaperEnrolmentService,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private paperEnrolmentFormStateService: PaperEnrolmentFormStateService,
    private utilsService: UtilsService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.allowUploads = false;
    this.decisions = [
      { code: false, name: 'No' },
      { code: true, name: 'Yes' }
    ];
    this.hasAttemptedFormSubmission = false;
    this.showUnansweredQuestionsError = false;
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public onUpload(controlName: string, sdd: SelfDeclarationDocument) {
    this.formState.addSelfDeclarationDocumentGuid(controlName, sdd.documentGuid);
  }

  public onRemove(controlName: string, documentGuid: string) {
    this.formState.removeSelfDeclarationDocumentGuid(controlName, documentGuid);
  }

  public onBack() {
    const certifications = this.enrollee?.certifications;
    const backRoutePath = (certifications?.length)
      ? PaperEnrolmentRoutes.REGULATORY
      : PaperEnrolmentRoutes.OBO_SITES;

    this.routeUtils.routeRelativeTo(['./', backRoutePath]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance(): void {
    this.formState = new SelfDeclarationFormState(this.fb);
  }

  protected patchForm(): void {
    const enrolleeId = +this.route.snapshot.params.eid;
    if (!enrolleeId) {
      throw new Error('No enrollee ID was provided');
    }

    // Start listening for changes triggered by the HTTP response
    this.initForm();

    this.paperEnrolmentResource.getEnrolleeById(enrolleeId)
      .pipe(map((enrollee: HttpEnrollee) => this.enrollee = enrollee))
      .subscribe(({ selfDeclarations }: HttpEnrollee) => {
        this.formState.patchValue({ selfDeclarations });
      });
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

  protected performSubmission(): NoContent {
    this.formState.form.markAsPristine();

    const payload = this.formState.json.selfDeclarations;
    return this.paperEnrolmentResource.updateSelfDeclarations(+this.route.snapshot.params.eid, payload);
  }

  protected onSubmitFormIsInvalid() {
    this.hasAttemptedFormSubmission = true;
    this.showUnansweredQuestionsError = this.showUnansweredQuestions();
    this.utilsService.scrollTop();
  }

  protected afterSubmitIsSuccessful(): void {
    this.formState.clearSelfDeclarationDocumentGuids();
    this.routeUtils.routeRelativeTo(['./', PaperEnrolmentRoutes.OVERVIEW]);
  }

  private toggleSelfDeclarationValidators(value: boolean, control: FormControl) {
    (!value)
      ? this.formUtilsService.resetAndClearValidators(control)
      : this.formUtilsService.setValidators(control, [Validators.required]);
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
}
