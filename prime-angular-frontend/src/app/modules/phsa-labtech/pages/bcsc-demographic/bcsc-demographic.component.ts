import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, Subscription } from 'rxjs';
import { map } from 'rxjs/operators';

import { FormControlValidators } from '@lib/validators/form-control.validators';

import { FormUtilsService } from '@core/services/form-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';

import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { AuthService } from '@auth/shared/services/auth.service';

import { PhsaLabtech } from '@phsa/shared/models/phsa-lab-tech.model';
import { PhsaLabtechResource } from '@phsa/shared/services/phsa-labtech-resource.service';

@Component({
  selector: 'app-bcsc-demographic',
  templateUrl: './bcsc-demographic.component.html',
  styleUrls: ['./bcsc-demographic.component.scss']
})
export class BcscDemographicComponent implements OnInit {

  public enrollee: PhsaLabtech;
  public form: FormGroup;
  // TODO: Set when API called
  public busy: Subscription;

  public constructor(
    protected fb: FormBuilder,
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected phasLabtechResource: PhsaLabtechResource,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    private authService: AuthService
  ) { }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      this.phasLabtechResource.createEnrollee(this.formAsJson);
    } else {
      this.utilService.scrollToErrorSection();
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();

    this.getUser$()
      .subscribe((enrollee: PhsaLabtech) =>
        this.enrollee = enrollee
      );
  }

  protected createFormInstance(): void {
    this.form = this.fb.group({
      phone: [null, [
        Validators.required,
        FormControlValidators.phone
      ]],
      phoneExtension: [null, [FormControlValidators.numeric]],
      email: [null, [
        Validators.required,
        FormControlValidators.email
      ]],
    });

  }

  /**
   * Convert BcscUser (Observable) from AuthService to PhsaLabtech (Observable)
   */
  private getUser$(): Observable<PhsaLabtech> {
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
          } as PhsaLabtech;
        })
      );
  }

  private get formAsJson(): PhsaLabtech {
    return this.form.value;
  }
}
