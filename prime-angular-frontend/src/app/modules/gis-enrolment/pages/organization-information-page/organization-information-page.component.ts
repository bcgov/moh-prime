import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, Subscription } from 'rxjs';

import { ConfigService } from '@config/config.service';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { GisEnrolmentFormStateService } from '@gis/shared/services/gis-enrolment-form-state.service';

@Component({
  selector: 'app-organization-information-page',
  templateUrl: './organization-information-page.component.html',
  styleUrls: ['./organization-information-page.component.scss']
})
export class OrganizationInformationPageComponent extends AbstractEnrolmentPage implements OnInit {
  public busy: Subscription;
  public title: string;
  public form: FormGroup;

  constructor(
    protected formUtilsService: FormUtilsService,
    private route: ActivatedRoute,
    private router: Router,
    private formStateService: GisEnrolmentFormStateService,
    private configService: ConfigService
  ) {
    super(formUtilsService);

    this.title = route.snapshot.data.title;
  }

  public get organization(): FormControl {
    return this.form.get('organization') as FormControl;
  }

  public get role(): FormControl {
    return this.form.get('role') as FormControl;
  }

  public onSubmit() {

  }

  public ngOnInit(): void {
    this.createFormInstance();
  }

  protected createFormInstance(): void {
    this.formState = this.formStateService.ldapUserPageFormState;
    this.form = this.formState.form;
  }

  protected patchForm(): void {

  }

  protected initForm(): void {
    throw new Error('Method not implemented.');
  }

  protected performSubmission(): Observable<unknown> {
    throw new Error('Method not implemented.');
  }
}
