import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { FormGroup, FormControl, Validators } from '@angular/forms';

import { exhaustMap, map, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';

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
  templateUrl: './paper-enrollee-returnees-page.component.html',
  styleUrls: ['./paper-enrollee-returnees-page.component.scss']
})
export class PaperEnrolleeReturneesPageComponent extends BaseEnrolmentProfilePage implements OnInit {
  public isOfflineFormAccessRequested: boolean;
  public formState: PaperEnrolleeReturneeFormState;
  public form: FormGroup;
  public bcscUser: BcscUser;
  public formControlConfig: { label: string, name: string }[];
  public userProvidedGpid: string;

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

  public get formUserProvidedGpid(): FormControl {
    return this.form.get('gpid') as FormControl;
  }

  public onChangeRequestedOfflineAccess({ checked }: MatSlideToggleChange): void {
    this.togglePaperEnrolleeReturneeValidator(checked, this.formUserProvidedGpid);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm()
      .pipe(
        exhaustMap(([_, enrolment]: [BcscUser, Enrolment]) =>
          (enrolment)
            ? this.enrolmentResource.getGpidFromLinkWithPotentialEnrollee(enrolment.id)
              .pipe(map((result: string) => result))
            : of(null)
        )
      )
      .subscribe((result: string) => {
        this.userProvidedGpid = result ? result : null;
        this.form.patchValue({ formUserProvidedGpid: this.userProvidedGpid })
        this.initForm()
      });
  }

  protected handleSubmission(): void {
    if (!this.enrolment.id && this.isInitialEnrolment) {
      // If yes and user provides a GPID, create enrollee here.
      this.getUser$()
        .pipe(
          map((enrollee: Enrollee) => {
            const { firstName, lastName, givenNames, verifiedAddress, ...remainder } = enrollee;
            const { userId, ...demographic } = this.enrolment.enrollee;
            return { ...remainder, ...demographic, firstName, lastName, givenNames, verifiedAddress };
          }),
          exhaustMap((enrollee: Enrollee) => this.enrolmentResource.createEnrollee({ enrollee })),
          // Populate the new enrolment within the form state by force patching
          tap((newEnrolment: Enrolment) => this.enrolmentFormStateService.setForm(newEnrolment, true)),
          exhaustMap((newEnrolment: Enrolment) => this.enrolmentResource.createOrUpdateInitialPaperEnrolleeLink(newEnrolment.id, this.formUserProvidedGpid.value)),
          this.handleResponse()
        )
        .subscribe();
    } else if ((!this.userProvidedGpid) || (this.userProvidedGpid !== this.formUserProvidedGpid.value)) {
      // For this page we always want to check for changes in the user provided GPID
      // before hitting the super onSubmit since this page's submission will be skipped
      // by the super.onSubmit after the profile is complete however users can still update / provide their gpids

      // Only update if the user had previously provided a paper enrolment gpid and
      // then updates the paper enrolment gpid through the form field
      this.enrolmentResource.createOrUpdateInitialPaperEnrolleeLink(this.enrolment.id, this.formUserProvidedGpid.value)
        .pipe(this.handleResponse())
        .subscribe();
    } else {
      this.nextRouteAfterSubmit();
    }
  }

  protected createFormInstance(): void {
    this.formState = this.enrolmentFormStateService.paperEnrolleeReturneeFormState;
    this.form = this.formState.form;
  }

  protected initForm(): void {
    this.isOfflineFormAccessRequested = !!(this.userProvidedGpid);

    this.togglePaperEnrolleeReturneeValidator(!!(this.userProvidedGpid), this.formUserProvidedGpid);
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
