import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, Subscription } from 'rxjs';

import { ConfigService } from '@config/config.service';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { GisEnrolmentFormStateService } from '@gis/shared/services/gis-enrolment-form-state.service';

@Component({
  selector: 'app-ldap-information-page',
  templateUrl: './ldap-information-page.component.html',
  styleUrls: ['./ldap-information-page.component.scss']
})
export class LdapInformationPageComponent extends AbstractEnrolmentPage implements OnInit {
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

  public get ldapUsername(): FormControl {
    return this.form.get('ldapUsername') as FormControl;
  }

  public get ldapPassword(): FormControl {
    return this.form.get('ldapPassword') as FormControl;
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
