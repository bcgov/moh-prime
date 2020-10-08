import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Observable } from 'rxjs';
import { exhaustMap, map, tap } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { Enrollee } from '@shared/models/enrollee.model';

import { BceidUser } from '@auth/shared/models/bceid-user.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

@Component({
  selector: 'app-bceid-demographic',
  templateUrl: './bceid-demographic.component.html',
  styleUrls: ['./bceid-demographic.component.scss']
})
export class BceidDemographicComponent extends BaseEnrolmentProfilePage implements OnInit {
  /**
   * @description
   * User information from the provider.
   */
  public user: BceidUser;
  public addressFormControlNames: string[];

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    private authService: AuthService
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
      formUtilsService
    );

    this.addressFormControlNames = [
      'street',
      'city',
      'provinceCode',
      'countryCode',
      'postal'
    ];
  }

  public get mailingAddress(): FormGroup {
    return this.form.get('mailingAddress') as FormGroup;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
    if (!this.enrolmentService.enrolment) {
      console.log('TEST');

      this.getUser$()
        .subscribe((enrollee: Enrollee) =>
          this.form.patchValue(enrollee)
        );
    }
  }

  protected createFormInstance() {
    this.form = this.enrolmentFormStateService.bceidDemographicForm;
  }

  protected initForm() { }

  protected performHttpRequest(enrolment: Enrolment, beenThroughTheWizard: boolean = false): Observable<void> {
    if (!enrolment.id && this.isInitialEnrolment) {
      const payload = {
        enrollee: this.form.getRawValue() as Enrollee,
        identificationDocumentGuid: this.enrolmentFormStateService.identityDocumentForm.get('identificationDocumentGuid').value as string
      };
      return this.enrolmentResource.createEnrollee(payload)
        .pipe(
          // Merge the enrolment with generated keys
          map((newEnrolment: Enrolment) => {
            newEnrolment.enrollee = { ...newEnrolment.enrollee, ...enrolment.enrollee };
            return newEnrolment;
          }),
          // Populate generated keys within the form state
          tap((newEnrolment: Enrolment) => this.enrolmentFormStateService.setForm(newEnrolment, true)),
          this.handleResponse()
        );
    } else {
      return super.performHttpRequest(enrolment, beenThroughTheWizard);
    }
  }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = EnrolmentRoutes.REGULATORY;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }

  private getUser$(): Observable<Enrollee> {
    return this.authService.getUser$()
      .pipe(
        map(({ firstName, lastName, email = null }: BceidUser) => {
          // Enforced the enrollee type instead of using Partial<Enrollee>
          // to avoid creating constructors and partials for every model
          return {
            // Providing only the minimum required fields for creating an enrollee
            preferredFirstName: firstName,
            preferredLastName: lastName,
            email
          } as Enrollee;
        })
      );
  }
}
