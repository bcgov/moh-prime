import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, of } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { ConfigService } from '@config/config.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentResource } from '@paper-enrolment/shared/services/paper-enrolment-resource.service';
import { RegulatoryFormState } from './regulatory-form-state.class';
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
    private configService: ConfigService,
    private fb: FormBuilder,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public onBack(): void {
    const backRoutePath = (this.enrollee.profileCompleted)
      ? PaperEnrolmentRoutes.OVERVIEW
      : PaperEnrolmentRoutes.CARE_SETTING;
    this.routeUtils.routeRelativeTo(backRoutePath);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
    this.patchForm();
  }

  public ngOnDestroy(): void {
    this.formState.removeIncompleteCertifications(true);
  }

  protected createFormInstance(): void {
    this.formState = new RegulatoryFormState(this.fb, this.configService);
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
      throw new Error('No enrollee ID was provided');
    }

    this.paperEnrolmentResource.getEnrolleeById(enrolleeId)
      .subscribe((enrollee: HttpEnrollee) => {
        if (enrollee) {
          this.enrollee = enrollee;
          // Attempt to patch the form if not already patched
          this.formState.patchValue({ certifications: enrollee.certifications });
        }
      });
  }

  protected performSubmission(): Observable<number> {
    this.formState.removeIncompleteCertifications(true);
    this.formState.form.markAsPristine();

    const payload = this.formState.json.certifications;
    const oboSites = this.removeOboSites(this.enrollee.oboSites);

    return this.paperEnrolmentResource.updateCertifications(this.enrollee.id, payload)
      .pipe(
        exhaustMap(() =>
          (this.enrollee.oboSites.length !== oboSites.length)
            ? this.paperEnrolmentResource.updateOboSites(this.enrollee.id, oboSites)
            : of(null)
        )
      );
  }

  protected afterSubmitIsSuccessful(): void {
    // Force obo sites to always be checked regardless of the profile being
    // completed so validations are applied prior to overview pushing the
    // responsibility of validation to obo sites
    const nextRoutePath = (!this.formState.collegeCertifications.length)
      ? PaperEnrolmentRoutes.OBO_SITES
      : (this.enrollee.profileCompleted)
        ? PaperEnrolmentRoutes.OVERVIEW
        : PaperEnrolmentRoutes.SELF_DECLARATION;

    this.routeUtils.routeRelativeTo(nextRoutePath);
  }

  /**
   * @description
   * Remove obo sites from the enrolment as enrollees can not have
   * certificate(s), as well as, obo site(s).
   */
  private removeOboSites(oboSites: OboSite[]): OboSite[] {
    this.formState.removeIncompleteCertifications(true);

    if (this.formState.certifications.length) {
      oboSites = [];
    }

    return oboSites;
  }
}
