import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { UtilsService } from '@core/services/utils.service';

import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { PhsaEnrollee } from '@phsa/shared/models/phsa-eforms.model';
import { PhsaEformsRoutes } from '@phsa/phsa-eforms.routes';
import { PhsaEformsFormStateService } from '@phsa/shared/services/phsa-eforms-form-state.service';

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
    private enrolmentFormStateService: PhsaEformsFormStateService,
    private utilService: UtilsService,
    private formUtilsService: FormUtilsService,
    private authService: AuthService
  ) {
    this.routeUtils = new RouteUtils(route, router, PhsaEformsRoutes.MODULE_PATH);
  }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      this.routeUtils.routeRelativeTo(PhsaEformsRoutes.AVAILABLE_ACCESS);
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
    this.form = this.enrolmentFormStateService.demographicFormState.form;

  }

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
}
