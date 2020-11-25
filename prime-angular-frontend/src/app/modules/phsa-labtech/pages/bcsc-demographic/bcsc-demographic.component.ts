import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { AuthService } from '@auth/shared/services/auth.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { Enrollee } from '@shared/models/enrollee.model';

@Component({
  selector: 'app-bcsc-demographic',
  templateUrl: './bcsc-demographic.component.html',
  styleUrls: ['./bcsc-demographic.component.scss']
})
export class BcscDemographicComponent extends BaseEnrolmentProfilePage implements OnInit {


  /**
   * @description
   * Enrollee information from the provider not
   * contained within the form.
   */
  public enrollee: Enrollee;

  public hasMailingAddress: boolean;


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
  }

  public get mailingAddress(): FormGroup {
    return this.form.get('mailingAddress') as FormGroup;
  }

  ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
    this.initForm();

    this.getUser$()
      .subscribe((enrollee: Enrollee) =>
        this.enrollee = enrollee
      );    
  }

  protected createFormInstance(): void {
    this.form = this.enrolmentFormStateService.bcscDemographicForm;
  }

  protected initForm(): void {
    // TODO:
    // throw new Error('Method not implemented.');
  }


  // TODO:
  private getUser$(): Observable<Enrollee> {

    return this.authService.getUser$()
      .pipe(
        map(({ userId, hpdid, firstName, lastName, givenNames, dateOfBirth, physicalAddress }: BcscUser) => {
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
            physicalAddress,
            phone: null,
            email: null
          } as Enrollee;
        })
      );
  }


}
