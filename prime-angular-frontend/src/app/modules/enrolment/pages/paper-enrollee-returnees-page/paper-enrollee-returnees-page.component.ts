import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { FormControl, Validators } from '@angular/forms';

import { exhaustMap } from 'rxjs/operators';
import { noop, Observable, of } from 'rxjs';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { UtilsService } from '@core/services/utils.service';
import { NoContent } from '@core/resources/abstract-resource';
import { Enrolment } from '@shared/models/enrolment.model';
import { ToggleContentChange } from '@shared/components/toggle-content/toggle-content.component';

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
  public bcscUser: BcscUser;
  /**
   * @description
   * Paper enrollee GPID extracted from
   * API request (source of truth).
   */
  public paperEnrolleeGpid: string | null;

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
  }

  public onChangeRequestedOfflineAccess({ checked }: ToggleContentChange): void {
    this.togglePaperEnrolleeReturneeValidator(checked, this.formState.paperEnrolleeGpid);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm()
      .pipe(
        exhaustMap(([_, enrolment]: [BcscUser, Enrolment]) =>
          (enrolment)
            ? this.enrolmentResource.getLinkedGpid(enrolment.id)
            : of(null)
        )
      )
      .subscribe((paperEnrolleeGpid: string | null) => {
        this.paperEnrolleeGpid = paperEnrolleeGpid;
        this.formState.paperEnrolleeGpid.patchValue(paperEnrolleeGpid);
        this.initForm();
      });
  }

  protected createFormInstance(): void {
    this.formState = this.enrolmentFormStateService.paperEnrolleeReturneeFormState;
    this.form = this.formState.form;
  }

  protected initForm(): void {
    this.togglePaperEnrolleeReturneeValidator(!!this.paperEnrolleeGpid, this.formState.paperEnrolleeGpid);
  }

  protected performHttpRequest(enrolment: Enrolment, beenThroughTheWizard: boolean = false): Observable<void> {
    const paperEnrolleeGpid = this.formState.paperEnrolleeGpid.value;
    const request$ = (!enrolment.id && this.isInitialEnrolment)
      ? this.createEnrolment(enrolment)
        .pipe(
          exhaustMap((newEnrolment: Enrolment) =>
            this.createOrUpdateLinkedGpid(newEnrolment.id, paperEnrolleeGpid)
          )
        )
      : of(enrolment)
        .pipe(
          exhaustMap((enrolment: Enrolment) =>
            (paperEnrolleeGpid || paperEnrolleeGpid !== this.paperEnrolleeGpid)
              ? this.createOrUpdateLinkedGpid(enrolment.id, paperEnrolleeGpid)
              : of(noop())
          )
        );

    return request$.pipe(this.handleResponse());
  }

  private createOrUpdateLinkedGpid(enrolmentId: number, paperEnrolleeGpid: string): Observable<NoContent> {
    return this.enrolmentResource
      .createOrUpdateLinkedGpid(enrolmentId, paperEnrolleeGpid);
  }

  private togglePaperEnrolleeReturneeValidator(isMatchingPaperEnrollee: boolean, paperEnrolmentGpid: FormControl): void {
    (isMatchingPaperEnrollee)
      ? this.formUtilsService.setValidators(paperEnrolmentGpid, [
        Validators.required,
        FormControlValidators.startsWith('NOBCSC'),
        FormControlValidators.requiredLength(20)
      ])
      : this.formUtilsService.resetAndClearValidators(paperEnrolmentGpid);
  }
}
