import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl, FormArray, FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, of } from 'rxjs';
import { tap, exhaustMap, map } from 'rxjs/operators';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { OboSite } from '@enrolment/shared/models/obo-site.model';

import { PaperEnrolmentFormStateService } from '@paper-enrolment/services/paper-enrolment-form-state.service';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentService } from '@paper-enrolment/services/paper-enrolment.service';
import { EnrolleeHealthAuthority } from '@shared/models/enrollee-health-authority.model';
import { CareSettingFormState } from './care-setting-form-state.class';
import { HttpEnrollee } from '@shared/models/enrolment.model';

@Component({
  selector: 'app-care-setting-page',
  templateUrl: './care-setting-page.component.html',
  styleUrls: ['./care-setting-page.component.scss']
})
export class CareSettingPageComponent extends AbstractEnrolmentPage implements OnInit, OnDestroy {
  public formState: CareSettingFormState;
  public careSettingCtrl: FormControl;
  public careSettingTypes: Config<number>[];
  public filteredCareSettingTypes: Config<number>[];
  public healthAuthorities: Config<number>[];
  public routeUtils: RouteUtils;
  public enrollee: HttpEnrollee;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected paperEnrolmentService: PaperEnrolmentService,
    protected paperEnrolmentResource: PaperEnrolmentResource,
    protected paperEnrolmentFormStateService: PaperEnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    private configService: ConfigService,
    private fb: FormBuilder
  ) {
    super(dialog, formUtilsService);

    this.careSettingTypes = this.configService.careSettings;
    this.healthAuthorities = this.configService.healthAuthorities;

    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);

    this.allowRoutingWhenDirty = false;
  }

  public onSubmit(): void {
    const controls = this.formState.careSettings.controls;

    // Remove any oboSites belonging to careSetting which is no longer selected
    this.careSettingTypes.forEach(type => {
      if (!controls.some(c => c.value.careSettingCode === type.code)) {
        this.removeOboSites(type.code);
      }
    });

    // Remove health authorities if health authority care setting not chosen
    if (!controls.some(c => c.value.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY)) {
      this.formState.removeHealthAuthorities();
    }

    if (this.formUtilsService.checkValidity(this.formState.form)) {
      super.onSubmit();
    } else {
      this.utilService.scrollToErrorSection();
    }
  }

  public addCareSetting() {
    const careSetting = this.formState.buildCareSettingForm();
    this.formState.careSettings.push(careSetting);
  }

  public disableCareSetting(careSettingCode: number): boolean {
    return ![
      CareSettingEnum.COMMUNITY_PHARMACIST,
      CareSettingEnum.HEALTH_AUTHORITY,
      CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE
    ].includes(careSettingCode);
  }

  public removeCareSetting(index: number) {
    this.formState.careSettings.removeAt(index);
  }

  public filterCareSettingTypes(careSetting: FormGroup) {
    // Create a list of filtered care settings
    if (this.formState.careSettings.length) {
      // All the currently chosen care settings
      const selectedCareSettingCodes = this.formState.careSettings.value
        .map((cs: CareSetting) => cs.careSettingCode);
      // Current care setting selected
      const currentCareSetting = this.careSettingTypes
        .find(cs => cs.code === careSetting.get('careSettingCode').value);
      // Filter the list of possible care settings using the selected care setting
      const filteredCareSettingTypes = this.careSettingTypes
        .filter((c: Config<number>) => !selectedCareSettingCodes.includes(c.code));

      if (currentCareSetting) {
        // Add the current careSetting to the list of filtered
        // careSettings so it remains visible
        filteredCareSettingTypes.unshift(currentCareSetting);
      }

      return filteredCareSettingTypes;
    }

    // Otherwise, provide the entire list of care setting types
    return this.careSettingTypes;
  }

  public hasSelectedHACareSetting(): boolean {
    return (this.formState.careSettings.value.some(e => e.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY));
  }

  public routeBackTo() {
    this.routeUtils.routeRelativeTo(['../', this.enrollee.id, PaperEnrolmentRoutes.DEMOGRAPHIC]);
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';

    const canDeactivate = (this.formState.form.dirty && !this.allowRoutingWhenDirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;

    return (canDeactivate instanceof Observable)
      ? canDeactivate.pipe(tap(() => this.removeIncompleteCareSettings()))
      : canDeactivate;
  }


  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  public ngOnDestroy() {
    this.removeIncompleteCareSettings();
  }

  protected createFormInstance(): void {
    this.formState = new CareSettingFormState(this.fb, this.configService);
  }

  protected initForm() {
    // Always have at least one care setting ready for
    // the enrollee to fill out
    if (!this.formState.careSettings.length) {
      this.addCareSetting();
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

          const careSettings = enrolleeCareSettings

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

    const payload = this.formState.convertCareSettingFormToJson(this.enrollee.id);
    // If an individual health authority was deselected, its Obo Sites should be removed as well
    const oboSites = this.removeUnselectedHAOboSites(payload.healthAuthorities, this.enrollee.oboSites);

    return this.paperEnrolmentResource.updateCareSettings(this.enrollee.id, payload)
      .pipe(
        exhaustMap(() => {
          return this.enrollee.oboSites.length !== oboSites.length
            ? this.paperEnrolmentResource.updateOboSites(this.enrollee.id, oboSites)
            : of(null)
        }),
        map(() => this.enrollee.id)
      );
  }

  private removeUnselectedHAOboSites(healthAuthorities: number[], oboSites: OboSite[]): OboSite[] {

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

  protected afterSubmitIsSuccessful(enrolleeId: number) {
    const oboSites = this.paperEnrolmentFormStateService.jobsFormState.oboSites.value as OboSite[];

    let nextRoutePath = PaperEnrolmentRoutes.REGULATORY;
    if (oboSites?.length) {
      // Should edit existing Job/OboSites next
      nextRoutePath = PaperEnrolmentRoutes.OBO_SITES;
    }
    this.routeUtils.routeRelativeTo(['../', enrolleeId, nextRoutePath]);
  }

  private removeIncompleteCareSettings() {
    this.formState.careSettings.controls
      .forEach((control: FormGroup, index: number) => {
        const value = control.get('careSettingCode').value;

        // Remove if care setting is empty or the group is invalid
        if (!value || control.invalid) {
          this.removeCareSetting(index);
        }
      });

    // Always have a single care setting available, and it prevents
    // the page from jumping too much when routing
    if (!this.formState.careSettings.controls.length) {
      this.addCareSetting();
    }
  }

  /**
   * @description
   * Remove obo sites by care setting if a care setting was removed from the enrolment
   */
  private removeOboSites(careSettingCode: number): void {
    const form = this.paperEnrolmentFormStateService.jobsFormState;
    const oboSites = form.oboSites as FormArray;

    oboSites.value?.forEach((site: OboSite, index: number) => {
      if (site.careSettingCode === careSettingCode) {
        oboSites.removeAt(index);
      }
    });

    const clear = (fa: FormArray) => {
      fa.clear();
      fa.clearValidators();
      fa.updateValueAndValidity();
    };

    switch (careSettingCode) {
      case CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE: {
        return clear(form.communityHealthSites);
      }
      case CareSettingEnum.COMMUNITY_PHARMACIST: {
        return clear(form.communityPharmacySites);
      }
      case CareSettingEnum.HEALTH_AUTHORITY: {
        const healthAuthoritySites = form.healthAuthoritySites as FormGroup;
        Object.keys(healthAuthoritySites.controls).forEach(healthAuthorityCode => {
          const sitesOfHealthAuthority = healthAuthoritySites.get(`${healthAuthorityCode}`) as FormArray;
          sitesOfHealthAuthority.clearValidators();
          sitesOfHealthAuthority.updateValueAndValidity();
          healthAuthoritySites.removeControl(healthAuthorityCode);
        });
      }
    }
  }
}
