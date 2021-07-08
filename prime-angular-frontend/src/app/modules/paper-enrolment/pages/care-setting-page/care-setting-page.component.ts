import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { of } from 'rxjs';
import { tap, exhaustMap } from 'rxjs/operators';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { OboSite } from '@enrolment/shared/models/obo-site.model';

import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { CareSettingFormState } from './care-setting-form-state.class';

@Component({
  selector: 'app-care-setting-page',
  templateUrl: './care-setting-page.component.html',
  styleUrls: ['./care-setting-page.component.scss']
})
export class CareSettingPageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: CareSettingFormState;
  public enrollee: HttpEnrollee;
  public careSettingTypes: Config<number>[];
  public filteredCareSettingTypes: Config<number>[];
  public healthAuthorities: Config<number>[];
  public routeUtils: RouteUtils;
  public hasNoHealthAuthoritiesError: boolean;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private configService: ConfigService,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.careSettingTypes = this.configService.careSettings;
    this.healthAuthorities = this.configService.healthAuthorities;
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
    this.hasNoHealthAuthoritiesError = false;
  }

  public onBack() {
    this.routeUtils.routeRelativeTo([PaperEnrolmentRoutes.DEMOGRAPHIC]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
    this.patchForm();
  }

  protected createFormInstance(): void {
    this.formState = new CareSettingFormState(this.fb, this.configService);
  }

  protected initForm(): void {
    // Always have at least one care setting ready for
    // the enrollee to fill out
    this.formState.addCareSetting();
  }

  protected patchForm(): void {
    const enrolleeId = +this.route.snapshot.params.eid;
    if (!enrolleeId) {
      return;
    }

    this.paperEnrolmentResource.getEnrolleeById(enrolleeId)
      .subscribe((enrollee: HttpEnrollee) => {
        this.enrollee = enrollee;
        const { enrolleeCareSettings, enrolleeHealthAuthorities } = enrollee;

        this.formState.patchValue({
          enrolleeCareSettings,
          enrolleeHealthAuthorities
        });
      });
  }

  protected onSubmitFormIsValid(): void {
    this.hasNoHealthAuthoritiesError = false;
  }

  protected onSubmitFormIsInvalid(): void {
    if (this.formState.hasSelectedHACareSetting() && this.formState.enrolleeHealthAuthorities.hasError) {
      this.hasNoHealthAuthoritiesError = true;
    }
  }

  protected performSubmission(): NoContent {
    this.formState.form.markAsPristine();
    let oboSites = this.enrollee.oboSites;
    const payload = this.formState.json;

    // Remove health authorities if health authority care setting not chosen
    if (!payload.careSettings.some(code => code === CareSettingEnum.HEALTH_AUTHORITY)) {
      payload.healthAuthorities = [];
    }

    // Remove any oboSites belonging to careSetting which is no longer selected
    this.careSettingTypes.forEach(type => {
      if (!payload.careSettings.some(code => code === type.code)) {
        oboSites = oboSites.filter((site: OboSite) => site.careSettingCode !== type.code);
      }
    });

    // When an individual health authority is deselected the OBO Sites should be removed
    oboSites = this.removeUnselectedHealthAuthOboSites(payload.healthAuthorities, oboSites);

    return this.paperEnrolmentResource.updateCareSettings(this.enrollee.id, payload)
      .pipe(
        exhaustMap(() =>
          (this.enrollee.oboSites.length !== oboSites.length)
            ? this.paperEnrolmentResource.updateOboSites(this.enrollee.id, oboSites)
              // Refresh obo sites for routing to the next view
              .pipe(tap(() => this.enrollee.oboSites = oboSites))
            : of(null)
        )
      );
  }

  protected afterSubmitIsSuccessful(): void {
    const oboSites = this.enrollee.oboSites;
    const certifications = this.enrollee.certifications;
    // Force regulatory/obo sites to always be visited regardless of the
    // profile completion so validations are applied prior to overview
    // pushing the responsibility of validation to obo sites
    const nextRoutePath = (!this.enrollee.profileCompleted || (!oboSites?.length && !certifications.length))
      ? PaperEnrolmentRoutes.REGULATORY
      : (oboSites?.length)
        ? PaperEnrolmentRoutes.OBO_SITES
        : PaperEnrolmentRoutes.OVERVIEW;
    this.routeUtils.routeRelativeTo(nextRoutePath);
  }

  private removeUnselectedHealthAuthOboSites(healthAuthorities: number[], oboSites: OboSite[]): OboSite[] {
    return oboSites.filter((oboSite: OboSite) =>
      healthAuthorities.some((healthAuthorityCode: number) => healthAuthorityCode === oboSite.healthAuthorityCode));
  }
}
