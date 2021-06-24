import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormGroup } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { pipe } from 'rxjs';
import { catchError, map } from 'rxjs/operators';

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
import { Enrolment } from '@shared/models/enrolment.model';
import { OboSite } from '@enrolment/shared/models/obo-site.model';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { PaperEnrolmentService } from '@paper-enrolment/services/paper-enrolment.service';
import { OboSiteFormState } from './obo-sites-form-state.class';

@Component({
  selector: 'app-obo-sites-page',
  templateUrl: './obo-sites-page.component.html',
  styleUrls: ['./obo-sites-page.component.scss']
})
export class OboSitesPageComponent extends AbstractEnrolmentPage implements OnInit, OnDestroy {
  public form: FormGroup;
  public formState: OboSiteFormState;
  public enrolment: Enrolment;
  public routeUtils: RouteUtils;
  public defaultOptionLabel: string;
  public jobNames: Config<number>[];
  public allowDefaultOption: boolean;

  public CareSettingEnum = CareSettingEnum;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected paperEnrolmentService: PaperEnrolmentService,
    protected paperEnrolmentResource: PaperEnrolmentResource,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    private configService: ConfigService
  ) {
    super(dialog, formUtilsService);

    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
    this.jobNames = this.configService.jobNames;
    this.allowDefaultOption = false;
    this.defaultOptionLabel = 'None';
  }

  public get careSettings() {
    return (this.enrolment?.careSettings)
      ? this.enrolment.careSettings
      : null;
  }

  public onSubmit(): void {
    this.nextRouteAfterSubmit();

    // if (this.formUtilsService.checkValidity(this.form)) {
    //   this.handleSubmission();
    // } else {
    //   this.utilService.scrollToErrorSection();
    // }
  }

  public routeBackTo() {
    // this.routeTo(['../', this.enrolment.id, PaperEnrolmentRoutes.REGULATORY]);
    this.routeUtils.routeRelativeTo(['../', '1', PaperEnrolmentRoutes.REGULATORY]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  public ngOnDestroy() {
    this.removeIncompleteOboSites(true);
    this.formState.removeCareSettingSites();
  }

  protected createFormInstance(): void {
    // this.formState = this.paperEnrolmentFormStateService.jobsFormState;
    // this.form = this.formState.form;
  }

  protected initForm() {
    // Initialize listeners before patching
    this.patchForm();

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
          this.enrolment.enrolleeHealthAuthorities.forEach(ha => {
            const sitesOfHealthAuthority = this.formState.healthAuthoritySites.get(`${ha.healthAuthorityCode}`) as FormArray;
            if (!sitesOfHealthAuthority) {
              this.formState.addOboSite(careSetting.careSettingCode, ha.healthAuthorityCode);
            }
          });
          break;
        }
      }
    });
  }

  protected patchForm(): void {
    // Will be null if enrolment has not been created
    const enrollee = this.paperEnrolmentService.enrollee;
    this.formState.patchValue(enrollee);
  }

  protected performSubmission(): NoContent {
    // Update using the form which could contain changes, and ensure identity
    // const enrolment = this.paperEnrolmentFormStateService.json;
    return void (0);
    // return this.paperEnrolmentResource.updateEnrollee(enrolment)
    //   .pipe(this.handleResponse());
  }

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

  protected onSubmitFormIsValid() {
    // Enrollees can not have jobs and certifications
    this.removeCollegeCertifications();
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
  }

  private nextRouteAfterSubmit(): void {
    // this.routeTo(['../', this.enrolment.id, PaperEnrolmentRoutes.SELF_DECLARATION]);
    this.routeUtils.routeRelativeTo(['../', '1', PaperEnrolmentRoutes.SELF_DECLARATION]);
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

  /**
   * @description
   * Remove college certifications from the enrolment as enrollees can not have
   * job(s), as well as, college certification(s).
   */
  private removeCollegeCertifications() {
    // this.paperEnrolmentFormStateService.regulatoryFormState.removeCollegeCertifications();
  }
}
