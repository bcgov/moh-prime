import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { MatSlideToggleChange } from '@angular/material/slide-toggle';
import { FormUtilsService } from '@core/services/form-utils.service';
import { ToastService } from '@core/services/toast.service';
import { AuthService } from '@auth/shared/services/auth.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { UtilsService } from '@core/services/utils.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { exhaustMap, map, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';

import { Enrollee } from '@shared/models/enrollee.model';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { PaperEnrolleeReturneeFormState } from './paper-enrollee-returnee-form-state.class';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';

import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';



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
    this.isOfflineFormAccessRequested = false;
  }

  public get paperEnrolmentGpid(): FormControl {
    return this.form.get('gpid') as FormControl;
  }

  protected createFormInstance(): void {
    this.formState = this.enrolmentFormStateService.paperEnrolleeReturneeFormState;
    this.form = this.formState.form;
  }

  private togglePaperEnrolleeReturneeValidator(isPaperEnrolmentReturnee: boolean, paperEnrolmentGpid: FormControl): void {
    if (!isPaperEnrolmentReturnee) {
      this.formUtilsService.resetAndClearValidators(paperEnrolmentGpid);
    } else {
      this.formUtilsService.setValidators(paperEnrolmentGpid, [Validators.required]);
    }
  }

  protected initForm(): void {
    this.isOfflineFormAccessRequested = !!(this.enrolment?.enrollee.gpid);
    this.togglePaperEnrolleeReturneeValidator(this.isOfflineFormAccessRequested, this.paperEnrolmentGpid);
  }

  public onChangeRequestedOfflineAccess({ checked }: MatSlideToggleChange): void {
    this.togglePaperEnrolleeReturneeValidator(checked, this.paperEnrolmentGpid);
  }

  protected performHttpRequest(enrolment: Enrolment, beenThroughTheWizard: boolean = false): Observable<void> {
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
      return super.performHttpRequest(enrolment, beenThroughTheWizard);
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
