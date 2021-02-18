import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, Subscription } from 'rxjs';

import { ConfigService } from '@config/config.service';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { GisEnrolmentFormStateService } from '@gis/shared/services/gis-enrolment-form-state.service';
import { AbstractFormState } from '@lib/classes/abstract-form-state.class';

@Component({
  selector: 'app-ldap-user-page',
  templateUrl: './ldap-user-page.component.html',
  styleUrls: ['./ldap-user-page.component.scss']
})
export class LdapUserPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: AbstractFormState<unknown>;
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
    super(
      formUtilsService
    );

    this.title = route.snapshot.data.title;
  }

  public get ldapUser(): FormControl {
    return this.form.get('ldapUser') as FormControl;
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
    this.form.
  }

  protected initForm(): void {
    throw new Error('Method not implemented.');
  }

  protected performSubmission(): Observable<unknown> {
    throw new Error('Method not implemented.');
  }
}
