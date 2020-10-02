import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Observable, of } from 'rxjs';
import { map } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { User } from '@auth/shared/models/user.model';

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
  // TODO use an actual model... maybe BceidUser
  public user: { firstName: string, lastName: string, email: string };
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
      'postal'
    ];
  }

  public get physicalAddress(): FormGroup {
    return this.form.get('physicalAddress') as FormGroup;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
    this.getUser$()
      .subscribe((user: { firstName: string, lastName: string, email: string }) =>
        this.form.patchValue(user)
      );
  }

  protected createFormInstance() {
    this.form = this.enrolmentFormStateService.bceidDemographicForm;
  }

  protected initForm() { }

  protected performHttpRequest(enrolment: Enrolment, beenThroughTheWizard: boolean = false): Observable<void> {
    // TODO handle create or update similar to demographic and make method generic
    return of(void 0);
  }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = EnrolmentRoutes.REGULATORY;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }

  private getUser$(): Observable<{ firstName: string, lastName: string, email: string }> {
    return this.authService.getUser$()
      .pipe(
        map(({ firstName, lastName }: User) => {
          // Enforced the enrollee type instead of using Partial<Enrollee>
          // to avoid creating constructors and partials for every model
          return {
            // Providing only the minimum required fields for creating an enrollee
            firstName,
            lastName,
            email: null
          };
        })
      );
  }
}
