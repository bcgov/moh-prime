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
    return (!enrolment.id && this.isInitialEnrolment)
      ? this.createEnrolment(enrolment)
        .pipe(
          exhaustMap((newEnrolment: Enrolment) =>
            this.enrolmentResource
              .createOrUpdateLinkedGpid(newEnrolment.id, this.formState.paperEnrolleeGpid.value)
          ),
          this.handleResponse()
        )
      : super.performHttpRequest(enrolment, beenThroughTheWizard);
  }

  private togglePaperEnrolleeReturneeValidator(isMatchingPaperEnrollee: boolean, paperEnrolmentGpid: FormControl): void {
    (isMatchingPaperEnrollee)
      ? this.formUtilsService.setValidators(paperEnrolmentGpid, [Validators.required])
      : this.formUtilsService.resetAndClearValidators(paperEnrolmentGpid);
  }
}
