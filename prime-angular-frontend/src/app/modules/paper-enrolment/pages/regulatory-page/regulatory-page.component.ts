import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, of } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';

import { FormUtilsService } from '@core/services/form-utils.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { RegulatoryFormState } from './regulatory-form-state.class';
import { ConfigService } from '@config/config.service';
import { OboSite } from '@enrolment/shared/models/obo-site.model';

@Component({
  selector: 'app-regulatory-page',
  templateUrl: './regulatory-page.component.html',
  styleUrls: ['./regulatory-page.component.scss']
})
export class RegulatoryPageComponent extends AbstractEnrolmentPage implements OnInit, OnDestroy {
  public formState: RegulatoryFormState;
  public routeUtils: RouteUtils;
  public enrollee: HttpEnrollee;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public routeBackTo() {
    this.routeUtils.routeRelativeTo(PaperEnrolmentRoutes.CARE_SETTING);
  }

  public onSubmit(): void {
    this.formState.removeIncompleteCertifications(true);
    super.onSubmit();
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
    this.patchForm();
  }

  public ngOnDestroy() {
    this.formState.removeIncompleteCertifications(true);
  }

  protected createFormInstance(): void {
    this.formState = new RegulatoryFormState(this.fb);
    this.form = this.formState.form;
  }

  protected initForm(): void {
    // Always have at least one certification ready for
    // the enrollee to fill out
    if (!this.formState.certifications.length) {
      this.formState.addEmptyCollegeCertification();
    }
  }

  protected patchForm(): void {
    const enrolleeId = +this.route.snapshot.params.eid;
    if (!enrolleeId) {
      return;
    }

    this.paperEnrolmentResource.getEnrolleeById(enrolleeId)
      .subscribe((enrollee: HttpEnrollee) => {
        if (enrollee) {
          this.enrollee = enrollee;
          const {
            certifications
          } = enrollee;

          // Attempt to patch the form if not already patched
          this.formState.patchValue(certifications);
        }
      });
  }

  protected performSubmission(): Observable<number> {
    this.formState.form.markAsPristine();

    const payload = this.formState.json;
    let oboSites = this.removeJobs(this.enrollee.oboSites);

    return this.paperEnrolmentResource.updateCertifications(this.enrollee.id, payload)
      .pipe(
        exhaustMap(() =>
          (this.enrollee.oboSites.length !== oboSites.length)
            ? this.paperEnrolmentResource.updateOboSites(this.enrollee.id, oboSites)
            : of(null)
        )
      );
  }

  protected afterSubmitIsSuccessful() {
    this.formState.removeIncompleteCertifications(true);
    const certifications = this.formState.collegeCertifications;

    const nextRoutePath = (!certifications.length)
      ? PaperEnrolmentRoutes.OBO_SITES
      : PaperEnrolmentRoutes.SELF_DECLARATION;

    this.routeUtils.routeRelativeTo([nextRoutePath]);
  }

  /**
   * @description
   * Remove obo sites/jobs from the enrolment as enrollees can not have
   * certificate(s), as well as, job(s).
   */
  private removeJobs(oboSites: OboSite[]): OboSite[] {
    this.formState.removeIncompleteCertifications(true);

    if (this.formState.certifications.length) {
      oboSites = [];
    }

    return oboSites;
  }

}
