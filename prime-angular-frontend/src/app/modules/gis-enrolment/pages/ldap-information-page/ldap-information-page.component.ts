import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { EMPTY } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';
import { GisEnrolmentResource } from '@gis/shared/resources/gis-enrolment-resource.service';
import { GisEnrolmentService } from '@gis/shared/services/gis-enrolment.service';
import { GisEnrolmentFormStateService } from '@gis/shared/services/gis-enrolment-form-state.service';
import { LdapInformationPageFormState } from './ldap-information-page-form-state.class';
import { LdapErrorResponse } from '@gis/shared/models/ldap-error-response.model';

@Component({
  selector: 'app-ldap-information-page',
  templateUrl: './ldap-information-page.component.html',
  styleUrls: ['./ldap-information-page.component.scss']
})
export class LdapInformationPageComponent extends AbstractEnrolmentPage implements OnInit {
  public title: string;
  public formState: LdapInformationPageFormState;
  public locked: boolean;
  public remainingAttempts: number;

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
    this.remainingAttempts = 3;
  }

  public onBack() {
    this.routeUtils.routeRelativeTo([`./${GisEnrolmentRoutes.LDAP_USER_PAGE}`]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
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
        exhaustMap((response: NoContent | LdapErrorResponse) => {
          if (response instanceof LdapErrorResponse) {
            const { unlocked } = response;
            if(this.remainingAttempts) {
              this.remainingAttempts--;
            }
            this.handleLdapResponse(!unlocked);
            return EMPTY;
          }

          this.handleLdapResponse();
          return this.gisEnrolmentResource.updateEnrolment(this.formStateService.json);
        })
      );
  }

  protected afterSubmitIsSuccessful(): void {
    // Don't want the password around any longer than needed
    this.formState.clearPassword();
    this.routeUtils.routeRelativeTo([`./${GisEnrolmentRoutes.ENROLLEE_INFO_PAGE}`]);
  }

  private handleLdapResponse(locked?: boolean) {
    this.locked = !!locked;
  }
}
