import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, of } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
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
    private fb: FormBuilder,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private route: ActivatedRoute,
    router: Router,
    private configService: ConfigService
  ) {
    super(dialog, formUtilsService);

    this.allowDefaultOption = false;
    this.defaultOptionLabel = 'None';
    this.jobNames = this.configService.jobNames;
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public get careSettings() {
    return (this.enrollee?.enrolleeCareSettings)
      ? this.enrollee.enrolleeCareSettings
      : null;
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(PaperEnrolmentRoutes.REGULATORY);
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
      .subscribe((enrollee: HttpEnrollee) => {
        this.enrollee = enrollee;

        this.formState.addOboSitesByCareSettingCode(
          this.careSettings.map(cs => cs.careSettingCode),
          enrollee.enrolleeHealthAuthorities
        );

        // Attempt to patch the form if not already patched
        this.formState.patchValue(enrollee);
      });
  }

  protected performSubmission(): Observable<number> {
    this.formState.form.markAsPristine();

    // TODO refactor this into something readable, and move into FormState
    this.formState.oboSites.clear();
    this.formState.communityHealthSites.controls.forEach((site) => this.formState.oboSites.push(site));
    this.formState.communityPharmacySites.controls.forEach((site) => this.formState.oboSites.push(site));
    Object.keys(this.formState.healthAuthoritySites.controls).forEach(healthAuthorityCode => {
      const sitesOfHealthAuthority = this.formState.healthAuthoritySites.get(healthAuthorityCode) as FormArray;
      sitesOfHealthAuthority.controls.forEach((site) => this.formState.oboSites.push(site));
    });
    // TODO care setting sites? rename to obo sites?
    this.formState.removeCareSettingSites();

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
