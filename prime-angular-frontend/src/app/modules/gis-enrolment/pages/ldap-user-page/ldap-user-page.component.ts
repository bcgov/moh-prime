import { Component, HostListener, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Observable, of } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';
import { GisEnrolmentService } from '@gis/shared/services/gis-enrolment.service';
import { GisEnrolmentFormStateService } from '@gis/shared/services/gis-enrolment-form-state.service';
import { LdapUserPageFormState } from './ldap-user-page-form-state.class';

@Component({
  selector: 'app-ldap-user-page',
  templateUrl: './ldap-user-page.component.html',
  styleUrls: ['./ldap-user-page.component.scss']
})
export class LdapUserPageComponent extends AbstractEnrolmentPage implements OnInit {
  public title: string;
  public formState: LdapUserPageFormState;

  private routeUtils: RouteUtils;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private formStateService: GisEnrolmentFormStateService,
    private gisEnrolmentService: GisEnrolmentService,
    route: ActivatedRoute,
    router: Router,
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, GisEnrolmentRoutes.routePath(GisEnrolmentRoutes.MODULE_PATH));
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }
  @HostListener('window:keyup', ['$event'])
  keyEvent(event: KeyboardEvent) {
    if (event.code === 'Enter') {
      this.onSubmit();
    }
  }

  protected createFormInstance(): void {
    this.formState = this.formStateService.ldapUserPageFormState;
  }

  protected patchForm(): void {
    this.formStateService.setForm(this.gisEnrolmentService.enrolment);
  }

  protected initForm(): void { } // NOOP

  protected performSubmission(): Observable<null> {
    return of(null);
  }

  protected afterSubmitIsSuccessful(): void {
    this.routeUtils.routeRelativeTo([`./${ GisEnrolmentRoutes.LDAP_INFO_PAGE }`]);
  }
}
