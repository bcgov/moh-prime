import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, of, Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';
import { GisEnrolmentFormStateService } from '@gis/shared/services/gis-enrolment-form-state.service';
import { LdapUserPageFormState } from './ldap-user-page-form-state.class';

@Component({
  selector: 'app-ldap-user-page',
  templateUrl: './ldap-user-page.component.html',
  styleUrls: ['./ldap-user-page.component.scss']
})
export class LdapUserPageComponent extends AbstractEnrolmentPage implements OnInit {
  public busy: Subscription;
  public title: string;
  public formState: LdapUserPageFormState;
  public form: FormGroup;

  private routeUtils: RouteUtils;

  constructor(
    protected formUtilsService: FormUtilsService,
    private route: ActivatedRoute,
    private router: Router,
    private formStateService: GisEnrolmentFormStateService
  ) {
    super(
      formUtilsService
    );

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, GisEnrolmentRoutes.routePath(GisEnrolmentRoutes.MODULE_PATH));
  }

  public get ldapUser(): FormControl {
    return this.form.get('ldapUser') as FormControl;
  }

  public ngOnInit(): void {
    this.createFormInstance();
  }

  protected createFormInstance(): void {
    this.formState = this.formStateService.ldapUserPageFormState;
    this.form = this.formState.form;
  }

  protected patchForm(): void {
    throw new Error('Method not implemented.');
  }

  protected initForm(): void {
    throw new Error('Method not implemented.');
  }

  protected performSubmission(): Observable<null> {
    return of(null);
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo([`../${GisEnrolmentRoutes.LDAP_INFO_PAGE}`]);
  }
}
