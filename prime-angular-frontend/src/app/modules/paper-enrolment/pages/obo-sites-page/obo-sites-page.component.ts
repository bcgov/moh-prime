import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, of, pipe } from 'rxjs';
import { catchError, exhaustMap, map } from 'rxjs/operators';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';
import { OboSite } from '@enrolment/shared/models/obo-site.model';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { OboSiteFormState } from './obo-sites-form-state.class';

@Component({
  selector: 'app-obo-sites-page',
  templateUrl: './obo-sites-page.component.html',
  styleUrls: ['./obo-sites-page.component.scss']
})
export class OboSitesPageComponent extends AbstractEnrolmentPage implements OnInit, OnDestroy {
  public formState: OboSiteFormState;
  public routeUtils: RouteUtils;
  public enrollee: HttpEnrollee;
  public defaultOptionLabel: string;
  public jobNames: Config<number>[];
  public allowDefaultOption: boolean;

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

    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
    this.jobNames = this.configService.jobNames;
    this.allowDefaultOption = false;
    this.defaultOptionLabel = 'None';
  }

  public get careSettings() {
    let careSettings = (this.enrollee?.enrolleeCareSettings) ? this.enrollee.enrolleeCareSettings : null;
    return careSettings;
  }

  public routeBackTo() {
    this.routeUtils.routeRelativeTo(PaperEnrolmentRoutes.REGULATORY);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
  }

  public ngOnDestroy(): void {
    this.removeIncompleteOboSites(true);
    this.formState.removeCareSettingSites();
  }

  protected createFormInstance(): void {
    this.formState = new OboSiteFormState(this.fb, this.formUtilsService, this.configService);
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

          // Add at least one site for each careSetting selected by enrollee
          this.careSettings?.forEach((careSetting) => {
            switch (careSetting.careSettingCode) {
              case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
                this.formState.communityHealthSites.setValidators([FormArrayValidators.atLeast(1)]);
                if (!this.formState.communityHealthSites.length) {
                  this.formState.addOboSite(careSetting.careSettingCode);
                }
                break;
              }
              case CareSettingEnum.COMMUNITY_PHARMACIST: {
                this.formState.communityPharmacySites.setValidators([FormArrayValidators.atLeast(1)]);
                if (!this.formState.communityPharmacySites.length) {
                  this.formState.addOboSite(careSetting.careSettingCode);
                }
                break;
              }
              case CareSettingEnum.HEALTH_AUTHORITY: {
                this.enrollee.enrolleeHealthAuthorities.forEach(ha => {
                  const sitesOfHealthAuthority = this.formState.healthAuthoritySites.get(`${ha.healthAuthorityCode}`) as FormArray;
                  if (!sitesOfHealthAuthority) {
                    this.formState.addOboSite(careSetting.careSettingCode, ha.healthAuthorityCode);
                  }
                });
                break;
              }
            }
          });

          // Attempt to patch the form if not already patched
          this.formState.patchValue(enrollee);
        }
      });
  }

  protected performSubmission(): Observable<number> {
    this.formState.form.markAsPristine();

    this.formState.oboSites.clear();
    this.formState.communityHealthSites.controls.forEach((site) => this.formState.oboSites.push(site));
    this.formState.communityPharmacySites.controls.forEach((site) => this.formState.oboSites.push(site));
    Object.keys(this.formState.healthAuthoritySites.controls).forEach(healthAuthorityCode => {
      const sitesOfHealthAuthority = this.formState.healthAuthoritySites.get(healthAuthorityCode) as FormArray;
      sitesOfHealthAuthority.controls.forEach((site) =>
        this.formState.oboSites.push(site));
    });
    this.formState.removeCareSettingSites();

    const payload = this.formState.json;

    return this.paperEnrolmentResource.updateOboSites(this.enrollee.id, payload.oboSites)
      .pipe(
        exhaustMap(() =>
          (this.enrollee.certifications.length)
            ? this.paperEnrolmentResource.updateCertifications(this.enrollee.id, [])
            : of(null)
        )
      );
  }

  protected afterSubmitIsSuccessful(): void {
    this.removeIncompleteOboSites(true);

    this.formState.oboSites.clear();
    this.formState.communityHealthSites.controls.forEach((site) => this.formState.oboSites.push(site));
    this.formState.communityPharmacySites.controls.forEach((site) => this.formState.oboSites.push(site));
    Object.keys(this.formState.healthAuthoritySites.controls).forEach(healthAuthorityCode => {
      const sitesOfHealthAuthority = this.formState.healthAuthoritySites.get(healthAuthorityCode) as FormArray;
      sitesOfHealthAuthority.controls.forEach((site) =>
        this.formState.oboSites.push(site));
    });
    this.formState.removeCareSettingSites();

    this.routeUtils.routeRelativeTo(PaperEnrolmentRoutes.SELF_DECLARATION);
  }


  /**
   * @description
   * Removes incomplete oboSites from the list in preparation for submission, and
   * allows for an empty list of oboSites if no jobs are solected.
   */
  private removeIncompleteOboSites(noEmptyOboSites: boolean = false) {
    this.formState.oboSites.controls
      .forEach((control: FormGroup, index: number) => {
        const value = control.get('physicalAddress').value.city;
        const careSetting = control.get('careSettingCode').value;

        // Remove when empty, default option, or group is invalid
        if (!value || value === this.defaultOptionLabel || control.invalid) {
          this.formState.removeOboSite(index, careSetting);
        }
      });

    // Add at least one site for each careSetting selected by enrollee
    this.careSettings?.forEach((careSetting) => {
      if (!noEmptyOboSites && !this.formState.oboSitesByCareSetting(careSetting.careSettingCode)?.length) {
        this.formState.addOboSite(careSetting.careSettingCode);
      }
    });
  }

}
