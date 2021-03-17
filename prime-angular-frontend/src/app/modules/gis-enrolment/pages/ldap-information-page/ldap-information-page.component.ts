import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Observable, of, Subscription } from 'rxjs';

import { ConfigService } from '@config/config.service';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { NoContent } from '@core/resources/abstract-resource';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';
import { GisEnrolmentFormStateService } from '@gis/shared/services/gis-enrolment-form-state.service';
import { GisResource } from '@gis/shared/resources/gis-resource.service';
import { LdapInformationPageFormState } from './ldap-information-page-form-state.class';

@Component({
  selector: 'app-ldap-information-page',
  templateUrl: './ldap-information-page.component.html',
  styleUrls: ['./ldap-information-page.component.scss']
})
export class LdapInformationPageComponent extends AbstractEnrolmentPage implements OnInit {
  public title: string;
  public formState: LdapInformationPageFormState;

  private routeUtils: RouteUtils;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private formStateService: GisEnrolmentFormStateService,
    private gisResource: GisResource,
    private configService: ConfigService,
    private route: ActivatedRoute,
    private router: Router,
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, GisEnrolmentRoutes.routePath(GisEnrolmentRoutes.MODULE_PATH));
  }

  public get ldapUsername(): FormControl {
    return this.form.get('ldapUsername') as FormControl;
  }

  public get ldapPassword(): FormControl {
    return this.form.get('ldapPassword') as FormControl;
  }

  public onBack() {
    this.routeUtils.routeRelativeTo([`./${ GisEnrolmentRoutes.LDAP_USER_PAGE }`]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
  }

  protected createFormInstance(): void {
    this.formState = this.formStateService.ldapInformationPageFormState;
    this.form = this.formState.form;
  }

  protected patchForm(): void {
    throw new Error('Method not implemented.');
  }

  protected initForm(): void {
    throw new Error('Method not implemented.');
  }

  protected performSubmission(): Observable<NoContent> {
    return this.gisResource.ldapLogin(this.form.value);
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo([`./${ GisEnrolmentRoutes.ORG_INFO_PAGE }`]);
  }
}
