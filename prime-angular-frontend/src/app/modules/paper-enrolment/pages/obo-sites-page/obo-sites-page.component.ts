import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, of } from 'rxjs';
import { exhaustMap, tap } from 'rxjs/operators';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { HttpEnrollee } from '@shared/models/enrolment.model';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { OboSiteFormState } from './obo-sites-form-state.class';

@Component({
  selector: 'app-obo-sites-page',
  templateUrl: './obo-sites-page.component.html',
  styleUrls: ['./obo-sites-page.component.scss']
})
export class OboSitesPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: OboSiteFormState;
  public enrollee: HttpEnrollee;
  public allowDefaultOption: boolean;
  public defaultOptionLabel: string;
  public jobNames: Config<number>[];
  public routeUtils: RouteUtils;
  public CareSettingEnum = CareSettingEnum;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private configService: ConfigService,
    private fb: FormBuilder,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private route: ActivatedRoute,
    router: Router,
  ) {
    super(dialog, formUtilsService);

    this.allowDefaultOption = false;
    this.defaultOptionLabel = 'None';
    this.jobNames = this.configService.jobNames;
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public onBack() {
    const backRoutePath = (this.enrollee.profileCompleted)
      ? PaperEnrolmentRoutes.OVERVIEW
      : PaperEnrolmentRoutes.CARE_SETTING;
    this.routeUtils.routeRelativeTo(backRoutePath);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  protected createFormInstance(): void {
    this.formState = new OboSiteFormState(this.fb, this.formUtilsService);
  }

  protected patchForm(): void {
    const enrolleeId = +this.route.snapshot.params.eid;
    if (!enrolleeId) {
      throw new Error('No enrollee ID was provided');
    }

    this.paperEnrolmentResource.getEnrolleeById(enrolleeId)
      .pipe(tap((enrollee: HttpEnrollee) => this.enrollee = enrollee))
      .subscribe((enrollee: HttpEnrollee) =>
        this.formState.patchValue({ oboSites: enrollee.oboSites }, enrollee)
      );
  }

  protected performSubmission(): Observable<number> {
    this.formState.form.markAsPristine();

    const payload = this.formState.json;
    return this.paperEnrolmentResource.updateOboSites(this.enrollee.id, payload.oboSites)
      .pipe(
        exhaustMap(() =>
          // Remove certifications if obo sites have been added
          (this.enrollee.certifications.length)
            ? this.paperEnrolmentResource.updateCertifications(this.enrollee.id, [])
            : of(null)
        )
      );
  }

  protected afterSubmitIsSuccessful(): void {
    const nextRoutePath = (this.enrollee.profileCompleted)
      ? PaperEnrolmentRoutes.OVERVIEW
      : PaperEnrolmentRoutes.SELF_DECLARATION;
    this.routeUtils.routeRelativeTo(nextRoutePath);
  }
}
