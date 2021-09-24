import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { exhaustMap, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';

import { FormUtilsService } from '@core/services/form-utils.service';
import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { UtilsService } from '@core/services/utils.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { Enrollee } from '@shared/models/enrollee.model';

import { AuthService } from '@auth/shared/services/auth.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';

import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';

import { PaperEnrolleeReturneeFormState } from './paper-enrollee-returnee-form-state.class';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';

@Component({
  selector: 'app-paper-enrollee-returnees',
  templateUrl: './paper-enrollee-returnees.component.html',
  styleUrls: ['./paper-enrollee-returnees.component.scss']
})
export class PaperEnrolleeReturneesComponent extends BaseEnrolmentProfilePage implements OnInit {
  public isOfflineFormAccessRequested: boolean;
  public formState: PaperEnrolleeReturneeFormState;
  public form: FormGroup;
  public bcscUser: BcscUser;
  public formControlConfig: { label: string, name: string }[];
  public userProvidedGpid: String;

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
    this.formControlConfig = [
      { label: 'GPID', name: 'formUserProvidedGpid' }
    ];
  }

  public get formUserProvidedGpid(): FormControl {
    return this.form.get('formUserProvidedGpid') as FormControl;
  }

  public onChangeRequestedOfflineAccess({ checked }: MatSlideToggleChange): void {
    this.togglePaperEnrolleeReturneeValidator(checked, this.formUserProvidedGpid);
  }

  public onSubmit(beenThroughTheWizard: boolean = false): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      this.onSubmitFormIsValid();
      this.handleSubmission();
    } else {
      this.onSubmitFormIsInvalid();
      // this.utilService.scrollToErrorSection();
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    if (this.enrolmentService.enrolment.id) {
      this.patchForm()
        .pipe(
          map(() => {
            // Patch form only if an enrolment is created on the
            // bcsc-demographics page
            this.enrolmentResource.getLinkedEnrolment(this.enrolmentService.enrolment.id)
              .pipe(
                map((result) => {
                  this.userProvidedGpid = result ? result : null;
                  this.form.patchValue({ formUserProvidedGpid: this.userProvidedGpid })
                })
              ).subscribe(() => this.initForm())
          })
        )
        .subscribe();
    } else {
      this.initForm()
    }
  }

  /**
   * @description
   * Handle a valid form submission.
   */
  protected handleSubmission() {
    if (!!this.userProvidedGpid) {
      // pass userProvidedGpid and go to overview
      if (this.userProvidedGpid !== null) {
        this.updateUserProvidedGpid();
      }
      this.nextRouteAfterSubmit();
    } else {
      // Allow routing to occur without invoking the deactivation,
      // modal to persist form state being dirty between views
      this.allowRoutingWhenDirty = true;
      this.routeTo(EnrolmentRoutes.BCSC_DEMOGRAPHIC, { state: { userProvidedGpid: this.formUserProvidedGpid.value } });
    }
  }

  protected updateUserProvidedGpid() {
    // Only update if the user updates the paper enrolment gpid through the form field
    if (this.userProvidedGpid !== this.formUserProvidedGpid.value) {
      this.busy = this.enrolmentResource.updateLinkedGpid(this.enrolment.enrollee, this.userProvidedGpid)
        .subscribe(() => this.nextRouteAfterSubmit());
    }
  }

  protected createFormInstance(): void {
    this.formState = this.enrolmentFormStateService.paperEnrolleeReturneeFormState;
    this.form = this.formState.form;
  }

  protected initForm(): void {
    this.isOfflineFormAccessRequested = !!(this.userProvidedGpid);

    this.togglePaperEnrolleeReturneeValidator(this.isOfflineFormAccessRequested, this.formUserProvidedGpid);
  }

  private togglePaperEnrolleeReturneeValidator(isPaperEnrolmentReturnee: boolean, paperEnrolmentGpid: FormControl): void {
    isPaperEnrolmentReturnee
      ? this.formUtilsService.setValidators(paperEnrolmentGpid, [Validators.required])
      : this.formUtilsService.resetAndClearValidators(paperEnrolmentGpid);
  }
}
