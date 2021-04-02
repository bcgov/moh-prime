import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatRadioChange } from '@angular/material/radio';
import { MatDialog } from '@angular/material/dialog';

import { of } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormControlValidators } from '@lib/validators/form-control.validators';
import { ConfigService } from '@config/config.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';

import { VaccinationsRoutes } from '@vaccinations/vaccinations.routes';

@Component({
  selector: 'app-credentials-page',
  templateUrl: './credentials-page.component.html',
  styleUrls: ['./credentials-page.component.scss']
})
export class CredentialsPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: null;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
  public credentialConfig: { code: number, name: string }[];
  public hasNoCredentialError: boolean;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private configService: ConfigService,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, VaccinationsRoutes.MODULE_PATH);

    this.credentialConfig = [
      { code: 1, name: 'User Personal Information' },
      { code: 2, name: 'COVID-19 Vaccination' },
      { code: 3, name: 'COVID-19 Vaccination Attributes' },
      { code: 4, name: 'COVID-19 Date of Vaccination' },
      { code: 5, name: 'COVID-19 2nd Vaccination Completed' },
      { code: 6, name: 'All' }
    ];
  }

  public credentialCode(): FormControl {
    return this.form.get('credentialCode') as FormControl;
  }

  public onSubmit(): void {
    this.hasAttemptedSubmission = true;

    if (this.checkValidity(this.form)) {
      this.onSubmitFormIsValid();
      // this.busy = this.performSubmission()
      //   .subscribe(() => this.afterSubmitIsSuccessful());
      this.afterSubmitIsSuccessful();
    } else {
      this.onSubmitFormIsInvalid();
    }
  }

  public onCredentialChange(change: MatRadioChange) {
    this.hasNoCredentialError = false;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  protected createFormInstance() {
    this.form = this.fb.group({
      credentialCode: [
        0,
        [FormControlValidators.requiredIndex]
      ]
    });
  }

  protected patchForm(): void {
  }

  protected initForm(): void {
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoCredentialError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    this.hasNoCredentialError = true;
  }

  protected performSubmission(): NoContent {
    return of().pipe(NoContentResponse);
  }

  protected afterSubmitIsSuccessful(): void {
    this.form.markAsPristine();
    this.routeUtils.routeRelativeTo(VaccinationsRoutes.ISSUANCE);
  }
}
