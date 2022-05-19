import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, of } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { FormControlValidators } from '@lib/validators/form-control.validators';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { ConfigService } from '@config/config.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { OboSite } from '@enrolment/shared/models/obo-site.model';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { PaperEnrolmentResource } from '@paper-enrolment/shared/services/paper-enrolment-resource.service';
import { RegulatoryFormState } from './regulatory-form-state.class';
import { ToggleContentChange } from '@shared/components/toggle-content/toggle-content.component';

@Component({
  selector: 'app-regulatory-page',
  templateUrl: './regulatory-page.component.html',
  styleUrls: ['./regulatory-page.component.scss']
})
export class RegulatoryPageComponent extends AbstractEnrolmentPage implements OnInit, OnDestroy {
  public formState: RegulatoryFormState;
  public routeUtils: RouteUtils;
  public enrollee: HttpEnrollee;
  public isDeviceProvider: boolean;
  public hasUnlistedCertification: boolean;

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

  public onUnlistedCertification({ checked }: ToggleContentChange) {
    if (!checked) {
      this.hasUnlistedCertification = false;
      this.formState.json.unlistedCertifications = [];
    } else {
      this.hasUnlistedCertification = true;
      this.formState.addEmptyUnlistedCollegeCertification();
    }
  }

  public onBack(): void {
    const backRoutePath = (this.enrollee.profileCompleted)
      ? PaperEnrolmentRoutes.OVERVIEW
      : PaperEnrolmentRoutes.CARE_SETTING;
    this.routeUtils.routeRelativeTo(backRoutePath);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.patchForm().subscribe(() => this.initForm());
  }

  public ngOnDestroy(): void {
    this.formState.removeIncompleteCertifications(true);
  }

  protected createFormInstance(): void {
    this.formState = new RegulatoryFormState(this.fb, this.configService);
      this.form = this.fb.group({
        unlistedCertifications: this.fb.array([])
      });
  }

  protected initForm(): void {
    this.hasUnlistedCertification = !!(this.formState.unlistedCertifications.controls.values);
    this.toggleUnlistedCertificationsValidators(
      this.hasUnlistedCertification,
      this.formState.unlistedCertifications
    )
      // this.formState.unlistedCertifications.controls[0]['controls'].unlistedCollegeName,
      // this.formState.unlistedCertifications.controls[0]['controls'].unlistedCollegeCode,
      // this.formState.unlistedCertifications.controls[0]['controls'].unlistedRenewalDate)
    // Always have at least one certification ready for
    // the enrollee to fill out
    if (!this.formState.certifications.length) {
      this.formState.addEmptyCollegeCertification();
    }
  }

  protected patchForm(): Observable<void> {
    const enrolleeId = +this.route.snapshot.params.eid;
    if (!enrolleeId) {
      throw new Error('No enrollee ID was provided');
    }

    return this.paperEnrolmentResource.getEnrolleeById(enrolleeId)
      .pipe(
        map((enrollee: HttpEnrollee) => {
          if (enrollee) {
            this.enrollee = enrollee;
            // Attempt to patch the form if not already patched
            const { certifications, deviceProviderIdentifier, unlistedCertifications } = enrollee;
            this.isDeviceProvider = enrollee.enrolleeCareSettings.some((careSetting) =>
              careSetting.careSettingCode === CareSettingEnum.DEVICE_PROVIDER);
            this.enableDeviceProviderValidator();
            this.formState.patchValue({ certifications, deviceProviderIdentifier, unlistedCertifications });
          }
        })
      );
  }

  protected performSubmission(): Observable<number> {
    this.formState.removeIncompleteCertifications(true);
    this.formState.form.markAsPristine();

    const certifications = this.formState.json.certifications;
    const unlistedCertifications = this.formState.json.unlistedCertifications;
    const deviceProviderIdentifier = this.formState.json.deviceProviderIdentifier;
    const oboSites = this.removeOboSites(this.enrollee.oboSites);

    return this.paperEnrolmentResource.updateCertifications(this.enrollee.id, certifications)
      .pipe(
        exhaustMap(() =>
          this.paperEnrolmentResource.updateUnlistedCertifications(this.enrollee.id, unlistedCertifications)
        ),
        exhaustMap(() =>
          this.paperEnrolmentResource.updateDeviceProvider(this.enrollee.id, deviceProviderIdentifier)
        ),
        exhaustMap(() =>
          (this.enrollee.oboSites.length !== oboSites.length)
            ? this.paperEnrolmentResource.updateOboSites(this.enrollee.id, oboSites)
            : of(null)
        )
      );
  }

  protected afterSubmitIsSuccessful(): void {
    const collegeCertifications = this.formState.collegeCertifications;
    const isDeviceProviderWithNoIdentifier = this.isDeviceProvider && !this.formState.deviceProviderIdentifier.value;

    // Force obo sites to always be checked regardless of the profile being
    // completed so validations are applied prior to overview pushing the
    // responsibility of validation to obo sites
    const nextRoutePath = (!collegeCertifications.length || isDeviceProviderWithNoIdentifier)
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

  private enableDeviceProviderValidator(): void {
    this.isDeviceProvider
      ? this.formUtilsService.setValidators(this.formState.deviceProviderIdentifier, [
        FormControlValidators.requiredLength(5),
        FormControlValidators.numeric
      ])
      : this.formUtilsService.resetAndClearValidators(this.formState.deviceProviderIdentifier);
  }
}
