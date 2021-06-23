import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, of } from 'rxjs';
import { tap, exhaustMap, map } from 'rxjs/operators';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
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
export class CareSettingPageComponent extends AbstractEnrolmentPage implements OnInit, OnDestroy {
  public formState: CareSettingFormState;
  public careSettingTypes: Config<number>[];
  public filteredCareSettingTypes: Config<number>[];
  public healthAuthorities: Config<number>[];
  public routeUtils: RouteUtils;
  public enrollee: HttpEnrollee;

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
  }

  public routeBackTo() {
    this.routeUtils.routeRelativeTo([PaperEnrolmentRoutes.DEMOGRAPHIC]);
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const canDeactivate = super.canDeactivate();

    return (canDeactivate instanceof Observable)
      ? canDeactivate.pipe(tap(() => this.formState.removeIncompleteCareSettings()))
      : canDeactivate;
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  public ngOnDestroy() {
    this.formState.removeIncompleteCareSettings();
  }

  protected createFormInstance(): void {
    this.formState = new CareSettingFormState(this.fb, this.configService);
  }

  protected initForm() {
    // Always have at least one care setting ready for
    // the enrollee to fill out
    if (!this.formState.careSettings.length) {
      this.formState.addCareSetting();
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
            enrolleeCareSettings,
            enrolleeHealthAuthorities
          } = enrollee;

          const careSettings = enrolleeCareSettings;

          // Attempt to patch the form if not already patched
          this.formState.patchValue({
            careSettings,
            enrolleeHealthAuthorities
          });
        }
      });
  }

  protected performSubmission(): Observable<number> {
    this.formState.form.markAsPristine();

    // Remove health authorities if health authority care setting not chosen
    if (!this.formState.careSettings.controls.some(c => c.value.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY)) {
      this.formState.removeHealthAuthorities();
    }

    const payload = this.formState.convertCareSettingFormToJson(this.enrollee.id);
    let oboSites = this.enrollee.oboSites;

    // Remove any oboSites belonging to careSetting which is no longer selected
    this.careSettingTypes.forEach(type => {
      if (!this.formState.careSettings.controls.some(c => c.value.careSettingCode === type.code)) {
        oboSites = this.removeOboSites(type.code, oboSites);
      }
    });

    // If an individual health authority was deselected, its Obo Sites should be removed as well
    oboSites = this.removeUnselectedHealthAuthOboSites(payload.healthAuthorities, oboSites);

    return this.paperEnrolmentResource.updateCareSettings(this.enrollee.id, payload)
      .pipe(
        exhaustMap(() =>
          (this.enrollee.oboSites.length !== oboSites.length)
            ? this.paperEnrolmentResource.updateOboSites(this.enrollee.id, oboSites)
            : of(null)
        ),
        map(() => this.enrollee.id)
      );
  }

  protected afterSubmitIsSuccessful() {
    const oboSites = this.enrollee.oboSites;

    let nextRoutePath = PaperEnrolmentRoutes.REGULATORY;
    if (oboSites?.length) {
      // Should edit existing Job/OboSites next
      nextRoutePath = PaperEnrolmentRoutes.OBO_SITES;
    }
    this.routeUtils.routeRelativeTo([nextRoutePath]);
  }

  private removeUnselectedHealthAuthOboSites(healthAuthorities: number[], oboSites: OboSite[]): OboSite[] {
    this.configService.healthAuthorities.forEach((healthAuthority, index) => {
      if (!healthAuthorities[index]) {
        for (let i = oboSites.length - 1; i >= 0; i--) {
          const oboSiteForm = oboSites[i];
          if (oboSiteForm.healthAuthorityCode === healthAuthority.code) {
            oboSites.splice(i, 1);
          }
        }
      }
    });

    return oboSites;
  }

  /**
   * @description
   * Remove obo sites by care setting if a care setting was removed
   * from the enrolment
   */
  private removeOboSites(careSettingCode: number, oboSites: OboSite[]): OboSite[] {
    oboSites.forEach((site: OboSite, index: number) => {
      if (site.careSettingCode === careSettingCode) {
        oboSites.splice(index, 1);
      }
    });

    return oboSites;
  }
}
