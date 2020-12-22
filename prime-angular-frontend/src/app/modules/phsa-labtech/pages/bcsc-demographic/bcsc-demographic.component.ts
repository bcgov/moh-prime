import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { RouteUtils } from '@lib/utils/route-utils.class';

import { FormUtilsService } from '@core/services/form-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';

import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { PhsaEnrollee } from '@phsa/shared/models/phsa-lab-tech.model';
import { PhsaLabtechResource } from '@phsa/shared/services/phsa-labtech-resource.service';
import { PhsaLabtechRoutes } from '@phsa/phsa-labtech.routes';
import { PhsaFormStateService } from '@phsa/shared/services/phsa-labtech-form-state.service';

@Component({
  selector: 'app-bcsc-demographic',
  templateUrl: './bcsc-demographic.component.html',
  styleUrls: ['./bcsc-demographic.component.scss']
})
export class BcscDemographicComponent implements OnInit {

  public enrollee: PhsaEnrollee;
  public form: FormGroup;
  public busy: Subscription;

  private routeUtils: RouteUtils;

  public constructor(
    protected fb: FormBuilder,
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected phsaLabtechResource: PhsaLabtechResource,
    protected enrolmentFormStateService: PhsaFormStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    private authService: AuthService,
  ) {
    this.routeUtils = new RouteUtils(route, router, PhsaLabtechRoutes.MODULE_PATH);
  }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      this.busy = this.phsaLabtechResource.createEnrollee(this.formAsJson).subscribe();
      this.routeUtils.routeRelativeTo(PhsaLabtechRoutes.AVAILABLE_ACCESS);
    } else {
      this.utilService.scrollToErrorSection();
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();

    this.getUser$()
      .subscribe((enrollee: PhsaEnrollee) => {
        this.enrollee = enrollee;
        this.enrolmentFormStateService.setForm(enrollee);
      });
  }

  protected createFormInstance(): void {
    this.form = this.enrolmentFormStateService.demographicsForm;

  }

  /**
   * Convert BcscUser (Observable) from AuthService to PhsaLabtech (Observable)
   */
  private getUser$(): Observable<PhsaEnrollee> {
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
            email: null,
            partyTypes: []
          } as PhsaEnrollee;
        })
      );
  }

  private get formAsJson(): PhsaEnrollee {
    return this.form.value;
  }
}
