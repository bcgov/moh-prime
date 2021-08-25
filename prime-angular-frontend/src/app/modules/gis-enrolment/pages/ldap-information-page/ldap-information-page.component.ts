import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { exhaustMap, catchError, map } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { NoContent, NoContentResponse } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';
import { GisEnrolmentResource } from '@gis/shared/resources/gis-enrolment-resource.service';
import { GisEnrolmentService } from '@gis/shared/services/gis-enrolment.service';
import { GisEnrolmentFormStateService } from '@gis/shared/services/gis-enrolment-form-state.service';
import { LdapInformationPageFormState } from './ldap-information-page-form-state.class';
import { LdapThrottlingParameters } from '@gis/shared/models/ldap-error.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-ldap-information-page',
  templateUrl: './ldap-information-page.component.html',
  styleUrls: ['./ldap-information-page.component.scss']
})
export class LdapInformationPageComponent extends AbstractEnrolmentPage implements OnInit {
  public title: string;
  public formState: LdapInformationPageFormState;
  public remainingAttempts: number;
  public lockoutTimeInHours: number;
  public apiErrorCode: number;

  private routeUtils: RouteUtils;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private formStateService: GisEnrolmentFormStateService,
    private gisEnrolmentService: GisEnrolmentService,
    private gisEnrolmentResource: GisEnrolmentResource,
    route: ActivatedRoute,
    router: Router,
  ) {
    super(dialog, formUtilsService);
    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, GisEnrolmentRoutes.routePath(GisEnrolmentRoutes.MODULE_PATH));
  }

  public onBack() {
    this.routeUtils.routeRelativeTo([`./${GisEnrolmentRoutes.LDAP_USER_PAGE}`]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
    // this.setThrottling();
  }

  protected createFormInstance(): void {
    this.formState = this.formStateService.ldapInformationPageFormState;
  }

  protected patchForm(): void {
    this.formStateService.setForm(this.gisEnrolmentService.enrolment);
  }

  protected initForm(): void { } // NOOP

  protected performSubmission(): NoContent {
    return this.gisEnrolmentResource.ldapLogin(this.gisEnrolmentService.enrolment.id, this.formState.credentials)
      .pipe(
        exhaustMap(() => this.gisEnrolmentResource.updateEnrolment(this.formStateService.json)),
        map(({ remainingAttempts, lockoutTimeInHours }: any) => {
          this.remainingAttempts = remainingAttempts;
          this.lockoutTimeInHours = lockoutTimeInHours;
        })
        // map((result: any) => {
        //   this.remainingAttempts = +result.headers.get('remainingAttempts');
        //   this.lockoutTimeInHours = +result.headers.get('lockoutTimeInHours');
        //   console.log('Remaining attempts: ', this.remainingAttempts);
        //   console.log('Lockout Time in Hours: ', this.lockoutTimeInHours);
        // })
        // catchError((error: any) => {
        //   // this.setThrottling();
        //   if (error.status === 401 && !this.remainingAttempts) {
        //     // this.apiErrorCode = error.status;
        //     // // console.log('ENJOY YOUR ERROR', error);
        //     // this.remainingAttempts -= 1;

        //     return NoContentResponse;
        //   }

        //   throw error;
        // }),
        // catchError((error: any) => {
        //   if (error) {
        //     this.remainingAttempts = +error.headers.get('remainingAttempts');
        //     this.lockoutTimeInHours = +error.headers.get('lockoutTimeInHours');
        //   }
        //   return NoContentResponse;
        // })
      )
    // .subscribe(val => console.log(val))
  }

  // protected setThrottling(): void {
  //   this.gisEnrolmentResource.ldapLogin(this.gisEnrolmentService.enrolment.id, this.formState.credentials)
  //     .subscribe((ldapThrottlingParameters: LdapThrottlingParameters) => {
  //       this.remainingAttempts = ldapThrottlingParameters.remainingAttempts;
  //       this.lockoutTimeInHours = ldapThrottlingParameters.lockoutTimeInHours;
  //     })
  // }

  protected afterSubmitIsSuccessful(): void {
    // Don't want the password around any longer than needed
    this.formState.clearPassword();
    this.routeUtils.routeRelativeTo([`./${GisEnrolmentRoutes.ORG_INFO_PAGE}`]);
  }
}
