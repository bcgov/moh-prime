import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormArray } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/enrolment-page.class';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { OboSite } from '@enrolment/shared/models/obo-site.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentFormStateService } from '@paper-enrolment/services/paper-enrolment-form-state.service';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { PaperEnrolmentService } from '@paper-enrolment/services/paper-enrolment.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { Observable, pipe } from 'rxjs';
import { map, tap, catchError } from 'rxjs/operators';
import { CareSettingFormState } from './care-setting-form-state.class';

@Component({
  selector: 'app-care-setting',
  templateUrl: './care-setting.component.html',
  styleUrls: ['./care-setting.component.scss']
})
export class CareSettingComponent extends BaseEnrolmentPage implements OnInit, OnDestroy {

  public form: FormGroup;
  public formState: CareSettingFormState;
  public enrolment: Enrolment;
  public careSettingCtrl: FormControl;
  public careSettingTypes: Config<number>[];
  public filteredCareSettingTypes: Config<number>[];
  public healthAuthorities: Config<number>[];
  public routeUtils: RouteUtils;
  private allowRoutingWhenDirty: boolean;

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
  ) {
    super(
      route,
      router
    );

    this.careSettingTypes = this.configService.careSettings;
    this.healthAuthorities = this.configService.healthAuthorities;

    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);

    this.allowRoutingWhenDirty = false;
  }

  public onSubmit(): void {
    this.nextRouteAfterSubmit();

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

    // If an individual health authority was deselected, its Obo Sites should be removed as well
    this.paperEnrolmentFormStateService.removeUnselectedHAOboSites();

    // if (this.formUtilsService.checkValidity(this.form)) {
    //   this.handleSubmission();
    // } else {
    //   this.utilService.scrollToErrorSection();
    // }
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
    // this.routeTo(['../', this.enrolment.id, PaperEnrolmentRoutes.DEMOGRAPHIC]);
    this.routeUtils.routeRelativeTo(['../', '1', PaperEnrolmentRoutes.DEMOGRAPHIC]);
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';

    const canDeactivate = (this.form.dirty && !this.allowRoutingWhenDirty)
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

  private createFormInstance(): void {
    this.formState = this.paperEnrolmentFormStateService.careSettingFormState;
    this.form = this.formState.form;
  }

  private initForm() {
    // Always have at least one care setting ready for
    // the enrollee to fill out
    if (!this.formState.careSettings.length) {
      this.addCareSetting();
    }
  }

  /**
   * @description
   * Patch the form with enrollee information.
   */
  private patchForm(): void {
    // Will be null if enrolment has not been created
    const enrolment = this.paperEnrolmentService.enrolment;
    // this.paperEnrolmentFormStateService.setForm(enrolment);
    this.formState.patchValue(enrolment);
  }

  private handleSubmission() {
    // Update using the form which could contain changes, and ensure identity
    const enrolment = this.paperEnrolmentFormStateService.json;
    this.busy = this.performHttpRequest(enrolment).subscribe();
  }

  private performHttpRequest(enrolment: Enrolment): Observable<void> {
    const enrollee = this.form.getRawValue();
    return this.paperEnrolmentResource.updateEnrollee(enrolment)
      .pipe(this.handleResponse());
  }

  /**
   * @description
   * Generic handler for the HTTP response. By default this covers update, and can
   * also be used for create actions, or extended for any response.
   */
  private handleResponse() {
    return pipe(
      map(() => {
        this.toastService.openSuccessToast('Enrolment information has been saved');
        this.form.markAsPristine();

        this.nextRouteAfterSubmit();
      }),
      catchError((error: any) => {
        this.toastService.openErrorToast('Enrolment information could not be saved');
        this.logger.error('[Enrolment] Submission error has occurred: ', error);

        throw error;
      })
    );
  }

  private nextRouteAfterSubmit(): void {
    const oboSites = this.paperEnrolmentFormStateService.jobsForm.get('oboSites').value as OboSite[];

    let nextRoutePath = PaperEnrolmentRoutes.REGULATORY;
    if (oboSites?.length) {
      // Should edit existing Job/OboSites next
      nextRoutePath = PaperEnrolmentRoutes.JOB;
    }
    // this.routeTo(['../', this.enrolment.id, nextRouthPath]);
    this.routeUtils.routeRelativeTo(['../', '1', nextRoutePath]);
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
    const form = this.paperEnrolmentFormStateService.jobsForm;
    const oboSites = form.get('oboSites') as FormArray;

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
        return clear(form.get('communityHealthSites') as FormArray);
      }
      case CareSettingEnum.COMMUNITY_PHARMACIST: {
        return clear(form.get('communityPharmacySites') as FormArray);
      }
      case CareSettingEnum.HEALTH_AUTHORITY: {
        const healthAuthoritySites = form.get('healthAuthoritySites') as FormGroup;
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
