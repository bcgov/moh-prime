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
      { label: 'GPID', name: 'gpid' }
    ];
  }

  public get paperEnrolmentGpid(): FormControl {
    return this.form.get('userProvidedGpid') as FormControl;
  }

  public onChangeRequestedOfflineAccess({ checked }: MatSlideToggleChange): void {
    this.togglePaperEnrolleeReturneeValidator(checked, this.paperEnrolmentGpid);
  }

  public onSubmit(beenThroughTheWizard: boolean = false): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      this.onSubmitFormIsValid();
      this.handleSubmission(beenThroughTheWizard);
    } else {
      this.onSubmitFormIsInvalid();
      this.utilService.scrollToErrorSection();
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm()
      .pipe(
        map((enrolment: Enrolment) => {
          if (!enrolment) {
            // Manage patching the form state for a new enrolment
            // that has not been created
            const paperEnrolmentGpid = enrolment.enrollee.gpid ?? '';
            this.form.patchValue({ paperEnrolmentGpid });
          }
        })
      )
      .subscribe(() => this.initForm());
  }

  /**
   * @description
   * Handle a valid form submission.
   */
  protected handleSubmission(beenThroughTheWizard: boolean = false) {
    if (this.isInitialEnrolment) {
      // Update using the form which could contain changes, and ensure identity
      // provider information was not altered by repopulating in the payload
      this.busy = this.authService.getUser$()
        .pipe(
          // TODO add idenity provider check to fork for BCeID
          map(({ firstName, lastName, givenNames, verifiedAddress }: BcscUser) => {
            const enrolment = this.enrolmentFormStateService.json;
            enrolment.enrollee = { ...enrolment.enrollee, firstName, lastName, givenNames, verifiedAddress };
            return enrolment;
          }),
          exhaustMap((enrolment: Enrolment) => this.performHttpRequest(enrolment, beenThroughTheWizard))
        )
        .subscribe();
    } else {
      // Allow routing to occur without invoking the deactivation,
      // modal to persist form state being dirty between views
      this.allowRoutingWhenDirty = true;
      this.nextRouteAfterSubmit();
    }
  }

  protected performHttpRequest(enrolment: Enrolment, beenThroughTheWizard: boolean = false): Observable<void> {
    // If this is the first time the enrollee logs in and is flagged for potential match for paper enrolment
    // then we create the enrollee here same as in the BCSC-Demographic page. That code remains the same to handle
    // enrollees who were not flagged by the paper enrolment match check.
    // We still need the enrollees information to create them in the database, but in this page we will also add GPID
    if (!enrolment.id && this.isInitialEnrolment) {
      return this.getUser$()
        .pipe(
          map((enrollee: Enrollee) => {
            const { firstName, lastName, givenNames, verifiedAddress, ...remainder } = enrollee;
            const { userId, ...demographic } = enrolment.enrollee;
            return { ...remainder, ...demographic, firstName, lastName, givenNames, verifiedAddress };
          }),
          exhaustMap((enrollee: Enrollee) => this.enrolmentResource.createEnrollee({ enrollee })),
          // Populate the new enrolment within the form state by force patching
          tap((newEnrolment: Enrolment) => this.enrolmentFormStateService.setForm(newEnrolment, true)),
          this.handleResponse()
        );
    } else {
      this.nextRouteAfterSubmit();
      // return super.performHttpRequest(enrolment, beenThroughTheWizard);
    }
  }

  protected createFormInstance(): void {
    this.formState = this.enrolmentFormStateService.paperEnrolleeReturneeFormState;
    this.form = this.formState.form;
  }

  protected initForm(): void {
    this.isOfflineFormAccessRequested = !!(this.enrolment?.enrollee.gpid);

    this.togglePaperEnrolleeReturneeValidator(this.isOfflineFormAccessRequested, this.paperEnrolmentGpid);
  }

  private togglePaperEnrolleeReturneeValidator(isPaperEnrolmentReturnee: boolean, paperEnrolmentGpid: FormControl): void {
    isPaperEnrolmentReturnee
      ? this.formUtilsService.setValidators(paperEnrolmentGpid, [Validators.required])
      : this.formUtilsService.resetAndClearValidators(paperEnrolmentGpid);
  }

  private getUser$(): Observable<Enrollee> {
    return this.authService.getUser$()
      .pipe(
        map(({ userId, hpdid, firstName, lastName, givenNames, dateOfBirth, verifiedAddress }: BcscUser) => {
          // Enforced the enrollee type instead of using Partial<Enrollee>
          // to avoid creating constructors and partials for every model
          return {
            // Providing only the minimum required fields for creating an enrollee
            userId,
            hpdid,
            firstName,
            lastName,
            givenNames,
            dateOfBirth,
            verifiedAddress,
            phone: null,
            email: null
          } as Enrollee;
        })
      );
  }
}
