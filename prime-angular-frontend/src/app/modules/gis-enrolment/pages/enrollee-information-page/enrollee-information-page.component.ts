import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Observable, of, Subscription } from 'rxjs';

import { ConfigService } from '@config/config.service';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { GisEnrolmentRoutes } from '@gis/gis-enrolment.routes';
import { GisEnrolmentFormStateService } from '@gis/shared/services/gis-enrolment-form-state.service';
import { EnrolleeInformationPageFormState } from './enrollee-information-page-form-state.class';

@Component({
  selector: 'app-enrollee-information-page',
  templateUrl: './enrollee-information-page.component.html',
  styleUrls: ['./enrollee-information-page.component.scss']
})
export class EnrolleeInformationPageComponent extends AbstractEnrolmentPage implements OnInit {
  public busy: Subscription;
  public title: string;
  public formState: EnrolleeInformationPageFormState;
  public form: FormGroup;

  private routeUtils: RouteUtils;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private route: ActivatedRoute,
    private router: Router,
    private formStateService: GisEnrolmentFormStateService,
    private configService: ConfigService
  ) {
    super(dialog, formUtilsService);

    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, GisEnrolmentRoutes.routePath(GisEnrolmentRoutes.MODULE_PATH));
  }

  public get phone(): FormControl {
    return this.form.get('phone') as FormControl;
  }

  public get email(): FormControl {
    return this.form.get('email') as FormControl;
  }

  public onBack() {
    this.routeUtils.routeRelativeTo([`../${ GisEnrolmentRoutes.ORG_INFO_PAGE }`]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
  }

  protected createFormInstance(): void {
    this.formState = this.formStateService.enrolleeInformationPageFormState;
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
    this.routeUtils.routeRelativeTo([`../${ GisEnrolmentRoutes.SUBMISSION_CONFIRMATION }`]);
  }
}
