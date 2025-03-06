import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationExtras } from '@angular/router';
import { FormGroup, ValidationErrors } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable, of, noop, EMPTY } from 'rxjs';
import { exhaustMap, map, tap } from 'rxjs/operators';
import moment from 'moment';

import { Address } from '@lib/models/address.model';
import { BUSY_SUBMISSION_MESSAGE } from '@lib/constants';
import { DateUtils } from '@lib/utils/date-utils.class';
import { ToastService } from '@core/services/toast.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { BusyService } from '@lib/modules/ngx-busy/busy.service';
import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { ConfigService } from '@config/config.service';
import { PrescriberIdTypeEnum } from '@shared/enums/prescriber-id-type.enum';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/enrolment-page.class';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { EnrolleeAbsence } from '@shared/models/enrollee-absence.model';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { CertSearch } from '@enrolment/shared/models/cert-search.model';
import { RemoteAccessSearch } from '@enrolment/shared/models/remote-access-search.model';
import { SiteResource } from '@core/resources/site-resource.service';


@Component({
  selector: 'app-overview',
  templateUrl: './overview.component.html',
  styleUrls: ['./overview.component.scss']
})
export class OverviewComponent extends BaseEnrolmentPage implements OnInit {
  public busy: Subscription;
  public enrolment: Enrolment;
  public enrolmentErrors: ValidationErrors;
  public currentStatus: EnrolmentStatusEnum;
  public demographicRoutePath: string;
  public identityProvider: IdentityProviderEnum;
  public withinDaysOfRenewal: boolean;
  public isMatchingPaperEnrollee: boolean;
  public paperEnrolleeGpid: string;
  public enrolleeAbsence: EnrolleeAbsence;
  public IdentityProviderEnum = IdentityProviderEnum;
  public EnrolmentStatus = EnrolmentStatusEnum;
  public hasOboToRuAgreementTypeChange: boolean;
  public hasMatchingRemoteUser: boolean;

  protected allowRoutingWhenDirty: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private dialog: MatDialog,
    private authService: AuthService,
    private enrolmentService: EnrolmentService,
    private enrolmentResource: EnrolmentResource,
    private enrolmentFormStateService: EnrolmentFormStateService,
    private toastService: ToastService,
    private formUtilsService: FormUtilsService,
    private busyService: BusyService,
    private configService: ConfigService,
    protected siteResource: SiteResource,
  ) {
    super(route, router);

    this.currentStatus = null;
    this.allowRoutingWhenDirty = true;

    this.authService.identityProvider$()
      .subscribe((identityProvider: IdentityProviderEnum) => {
        this.identityProvider = identityProvider;
        this.demographicRoutePath = (identityProvider === IdentityProviderEnum.BCEID)
          ? EnrolmentRoutes.BCEID_DEMOGRAPHIC
          : EnrolmentRoutes.BCSC_DEMOGRAPHIC;
      });
  }

  public onSubmit(): void {
    if (!this.enrolmentFormStateService.isValidSubmission || this.isMissingPharmaNetId(this.enrolment.certifications)) {
      this.enrolmentFormStateService.forms.forEach((form: FormGroup) => this.formUtilsService.logFormErrors(form));
      this.toastService.openErrorToast('Your enrolment has an error that needs to be corrected before you will be able to submit');
      return;
    }

    const enrolment = this.enrolmentFormStateService.json;
    const data: DialogOptions = {
      title: 'Submit Enrolment',
      message: 'When your enrolment is submitted for adjudication, it can no longer be updated. Are you ready to submit your enrolment?',
      actionText: 'Submit Enrolment'
    };

    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: boolean) => (result) ? of(noop()) : EMPTY),
        this.busyService.showMessagePipe(BUSY_SUBMISSION_MESSAGE, this.enrolmentResource.submitApplication(enrolment))
      )
      .subscribe(() => {
        this.toastService.openSuccessToast('Enrolment has been submitted');
        this.routeTo(EnrolmentRoutes.CHANGES_SAVED);
      });
  }

  public showRequestRemoteAccessButton(): boolean {
    return this.hasMatchingRemoteUser && this.currentStatus === this.EnrolmentStatus.EDITABLE;
  }

  public routeTo(routePath: EnrolmentRoutes, navigationExtras: NavigationExtras = {}): void {
    this.allowRoutingWhenDirty = true;
    super.routeTo(routePath, navigationExtras);
  }

  // TODO split out deactivation and allowRoutingWhenDirty into separate base class
  // since it has common use @see BaseEnrolmentProfilePage
  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.enrolmentFormStateService.isDirty && !this.allowRoutingWhenDirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  public get GPID(): string {
    return this.enrolment?.enrollee?.gpid;
  }

  public onCopy(): void {
    this.toastService.openSuccessToast('Your GPID has been copied to clipboard');
  }

  public hasErrors(): boolean {
    return (this.enrolmentErrors) ? Object.values(this.enrolmentErrors).some(value => value) : false;
  }

  public requireLicenceUpdate(): boolean {
    return (this.enrolmentErrors) ? this.enrolmentErrors.requiresLicenceUpdate : false;
  }

  public ngOnInit(): void {
    this.isMatchingPaperEnrollee = this.enrolmentService.isMatchingPaperEnrollee;
    this.authService.getUser$()
      .pipe(
        map(({ firstName, lastName, givenNames, dateOfBirth, verifiedAddress }: BcscUser) => {
          // Initial assumption is a user has authenticated, been redirected to
          // this view, and not made any changes to the state of their enrolment
          // so use the source of truth that is populated from the server
          let enrolment = this.enrolmentService.enrolment;

          // Store current status as it will be truncated for initial enrolment
          this.currentStatus = enrolment.currentStatus.statusCode;

          // Form being patched indicates that there is possibly changes that reside
          // in the form for submission, and they should be reflected in the view
          if (this.enrolmentFormStateService.isPatched) {
            // Replace enrolment with the version from the form for the user
            // to review, but maintain a subset of immutable properties
            const { selfDeclarationDocuments,
              selfDeclarationCompletedDate,
              requireRedoSelfDeclaration,
              appliedDate } = enrolment;

            const stateSelfDeclarationCompletedDate = this.enrolmentFormStateService.selfDeclarationCompletedDate;

            enrolment = {
              ...this.enrolmentFormStateService.json,
              selfDeclarationDocuments,
              selfDeclarationCompletedDate: stateSelfDeclarationCompletedDate && selfDeclarationCompletedDate < stateSelfDeclarationCompletedDate ?
                stateSelfDeclarationCompletedDate : selfDeclarationCompletedDate,
              requireRedoSelfDeclaration: !stateSelfDeclarationCompletedDate && requireRedoSelfDeclaration,
              expiryDate: this.enrolmentService.enrolment.expiryDate,
              appliedDate,
            };
            enrolment.enrollee.gpid = this.enrolmentService.enrolment.enrollee.gpid;
          }

          // Allow for BCSC information to be updated on each submission of the enrolment
          // regardless of whether they visited the demographic view to make adjustments
          const form = this.enrolmentFormStateService.bcscDemographicFormState.form;
          if (!verifiedAddress) {
            this.formUtilsService.resetAndClearValidators(form.get('verifiedAddress') as FormGroup);
            verifiedAddress = new Address();
          }

          // Merge current BCSC information that may not be stored in the form
          // or in the enrolment for use within the view
          enrolment.enrollee = { ...enrolment.enrollee, firstName, lastName, givenNames, dateOfBirth, verifiedAddress };

          // Store a local copy of the enrolment for views
          this.enrolment = enrolment;
          this.isInitialEnrolment = this.enrolmentService.isInitialEnrolment;

          // Attempt to patch the form if not already patched
          this.enrolmentFormStateService.setForm(enrolment);

          this.enrolmentErrors = this.getEnrolmentErrors(enrolment);

          this.withinDaysOfRenewal = DateUtils.withinRenewalPeriod(this.enrolment?.expiryDate);

          const { careSettings } = enrolment;

          if (careSettings.some(cs => cs.careSettingCode === CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE)) {

            const certSearch: CertSearch[] = enrolment.certifications
              .map(c => ({
                collegeCode: c.collegeCode,
                licenseCode: c.licenseCode,
                licenceNumber: c.licenseNumber,
                practitionerId: c.practitionerId
              }));

            if (certSearch.length) {
              this.siteResource.getSitesByRemoteUserInfo(certSearch)
                .subscribe(
                  (remoteAccessSearch: RemoteAccessSearch[]) => {
                    if (remoteAccessSearch.length) {
                      this.hasMatchingRemoteUser = true
                    } else {
                      this.hasMatchingRemoteUser = this.hasMatchingRemoteUser || false
                    }
                  });
            } else {
              this.hasMatchingRemoteUser = false;
            }
          }

        }),
        exhaustMap(_ =>
          (this.isMatchingPaperEnrollee)
            ? this.enrolmentResource.getLinkedGpid(this.enrolmentService.enrolment.id)
              .pipe(tap((paperEnrolleeGpid: string) => this.paperEnrolleeGpid = paperEnrolleeGpid))
            : of(noop())
        ),
        exhaustMap(() => this.enrolmentResource.getIsOboToRuAgreementTypeChange(this.enrolment.id)
          .pipe(
            tap((hasChanged: boolean) => this.hasOboToRuAgreementTypeChange = hasChanged)
          )
        ),
        exhaustMap(() => this.enrolmentResource.getCurrentEnrolleeAbsence(this.enrolment.id))
      ).subscribe((enrolleeAbsence: EnrolleeAbsence) => this.enrolleeAbsence = enrolleeAbsence);

  }

  /**
   * @description
   * Get a set of enrolment errors.
   *
   * NOTE: Not possible to validate some form states due to validators
   * being dynamically applied when the view is loaded. Use the passed
   * enrolment for checking validation instead of form state.
   */
  private getEnrolmentErrors(enrolment: Enrolment): ValidationErrors {
    return {
      certificateOrOboSite: !enrolment.certifications?.length && !enrolment.oboSites?.length
        && !enrolment.careSettings.some((careSetting) => careSetting.careSettingCode === CareSettingEnum.DEVICE_PROVIDER),
      deviceProvider: enrolment.careSettings.some((careSetting) => careSetting.careSettingCode === CareSettingEnum.DEVICE_PROVIDER)
        && (!enrolment.enrolleeDeviceProviders || enrolment.enrolleeDeviceProviders.length === 0),
      missingHAOboSite: enrolment.oboSites?.length && enrolment.oboSites.some(s => s.careSettingCode == CareSettingEnum.HEALTH_AUTHORITY && s.healthAuthorityCode === null),
      missingOboSite: this.isMissingOboSite(enrolment),
      missingPharmaNetId: this.isMissingPharmaNetId(enrolment.certifications),
      missingHealthAuthorityCareSetting: enrolment.careSettings.some(cs => cs.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY)
        && !enrolment.enrolleeHealthAuthorities?.some(ha => ha.healthAuthorityCode),
      expiredCertification: enrolment.certifications.some(cert => moment(cert.renewalDate).isBefore(moment())),
      requiresLicenceUpdate: enrolment.certifications.some((cert: CollegeCertification) =>
        this.configService.licenses.some(l => l.code === cert.licenseCode && l.collegeLicenses.some(cl => cl.collegeCode === cert.collegeCode && cl.discontinued))),
      requireRedoSelfDeclaration: enrolment.requireRedoSelfDeclaration,
    };
  }

  private isMissingPharmaNetId(certifications: CollegeCertification[]): boolean {
    return certifications.some((cert: CollegeCertification) => {
      const prescriberIdType = this.enrolmentService.getPrescriberIdType(cert.licenseCode);
      if (prescriberIdType === PrescriberIdTypeEnum.Mandatory) {
        return cert.practitionerId === null;
      }
      else { return false; }
    });
  }

  private isMissingOboSite(enrolment: Enrolment): boolean {
    if (!enrolment.careSettings.some((careSetting) => careSetting.careSettingCode === CareSettingEnum.DEVICE_PROVIDER)) {
      if (!enrolment.certifications?.length) {
        let missingOboJob = false;
        enrolment.careSettings.forEach(cs => {
          if (cs.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY) {
            enrolment.enrolleeHealthAuthorities.forEach(ha => {
              missingOboJob = missingOboJob || !enrolment.oboSites.some(s => ha.healthAuthorityCode === s.healthAuthorityCode);
            });
          } else {
            missingOboJob = missingOboJob || !enrolment.oboSites.some(s => s.careSettingCode === cs.careSettingCode);
          }
        })
        if (missingOboJob) return true;
      }
    }
    return false;
  }
}
