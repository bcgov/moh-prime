import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormArray, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { AuthService } from '@auth/shared/services/auth.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';

@Component({
  selector: 'app-care-setting',
  templateUrl: './care-setting.component.html',
  styleUrls: ['./care-setting.component.scss']
})
export class CareSettingComponent extends BaseEnrolmentProfilePage implements OnInit, OnDestroy {
  public careSettingCtrl: FormControl;
  public careSettingTypes: Config<number>[];
  public filteredCareSettingTypes: Config<number>[];

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    private configService: ConfigService,
    private authService: AuthService
  ) {
    super(
      route,
      router,
      dialog,
      enrolmentService,
      enrolmentResource,
      enrolmentFormStateService,
      toastService,
      logger,
      utilService,
      formUtilsService
    );

    this.careSettingTypes = this.configService.careSettings;
  }

  public get careSettings(): FormArray {
    return this.form.get('careSettings') as FormArray;
  }

  public addCareSetting() {
    const careSetting = this.enrolmentFormStateService.buildCareSettingForm();
    this.careSettings.push(careSetting);
  }

  public disableCareSetting(careSettingCode: number): boolean {
    let disabled = true;
    // If feature flagged enabled "Community Pharmacist"
    if (this.authService.hasCommunityPharmacist() && careSettingCode === CareSettingEnum.COMMUNITY_PHARMACIST) {
      disabled = false;
    }
    // If feature flagged enabled "Health Authority"
    if (this.authService.hasHealthAuthority() && careSettingCode === CareSettingEnum.HEALTH_AUTHORITY) {
      disabled = false;
    }
    if (careSettingCode === CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE) {
      disabled = false;
    }

    return disabled;
  }

  public removeCareSetting(index: number) {
    this.careSettings.removeAt(index);
  }

  public filterCareSettingTypes(careSetting: FormGroup) {
    // Create a list of filtered care settings
    if (this.careSettings.length) {
      // All the currently chosen care settings
      const selectedCareSettingCodes = this.careSettings.value
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

  public canDeactivate(): Observable<boolean> | boolean {
    const canDeactivate = super.canDeactivate();

    return (canDeactivate instanceof Observable)
      ? canDeactivate.pipe(tap(() => this.removeIncompleteOrganizations()))
      : canDeactivate;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  public ngOnDestroy() {
    this.removeIncompleteOrganizations();
  }

  protected createFormInstance() {
    this.form = this.enrolmentFormStateService.careSettingsForm;
  }

  protected initForm() {
    // Always have at least one care setting ready for
    // the enrollee to fill out
    if (!this.careSettings.length) {
      this.addCareSetting();
    }
  }

  protected nextRouteAfterSubmit() {
    const certifications = this.enrolmentFormStateService.regulatoryForm
      .get('certifications').value as CollegeCertification[];
    const careSettings = this.enrolmentFormStateService.careSettingsForm
      .get('careSettings').value as CareSetting[];

    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = (
        this.enrolmentService
          .canRequestRemoteAccess(certifications, careSettings)
      )
        ? EnrolmentRoutes.REMOTE_ACCESS
        : EnrolmentRoutes.SELF_DECLARATION;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }

  private removeIncompleteOrganizations() {
    this.careSettings.controls
      .forEach((control: FormGroup, index: number) => {
        const value = control.get('careSettingCode').value;

        // Remove if care setting is empty or the group is invalid
        if (!value || control.invalid) {
          this.removeCareSetting(index);
        }
      });

    // Always have a single care setting available, and it prevents
    // the page from jumping too much when routing
    if (!this.careSettings?.controls.length) {
      this.addCareSetting();
    }
  }

  public routeBackTo() {
    const routePath = (this.enrolmentFormStateService.json?.certifications.length)
      ? EnrolmentRoutes.REGULATORY
      : EnrolmentRoutes.JOB;

    this.routeTo(routePath);
  }
}
