import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationExtras } from '@angular/router';
import { FormGroup, ValidationErrors } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { EMPTY, Subscription, Observable } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { Address } from '@shared/models/address.model';
import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/enrolment-page.class';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';

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
  public IdentityProviderEnum = IdentityProviderEnum;
  public EnrolmentStatus = EnrolmentStatusEnum;

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
    private formUtilsService: FormUtilsService
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

  public onSubmit() {
    if (this.enrolmentFormStateService.isValid) {
      const enrolment = this.enrolmentFormStateService.json;
      const data: DialogOptions = {
        title: 'Submit Enrolment',
        message: 'When your enrolment is submitted for adjudication, it can no longer be updated. Are you ready to submit your enrolment?',
        actionText: 'Submit Enrolment'
      };
      this.busy = this.dialog.open(ConfirmDialogComponent, { data })
        .afterClosed()
        .pipe(
          exhaustMap((result: boolean) =>
            (result)
              ? this.enrolmentResource.submitApplication(enrolment)
              : EMPTY
          )
        )
        .subscribe(() => {
          this.toastService.openSuccessToast('Enrolment has been submitted');
          this.routeTo(EnrolmentRoutes.CHANGES_SAVED);
        });
    } else {
      this.enrolmentFormStateService.forms.forEach((form: FormGroup) => this.formUtilsService.logFormErrors(form));
      this.toastService.openErrorToast('Your enrolment has an error that needs to be corrected before you will be able to submit');
    }
  }

  public canRequestRemoteAccess(): boolean {
    const certifications = this.enrolmentFormStateService.regulatoryFormState.collegeCertifications;
    const careSettings = this.enrolmentFormStateService.careSettingsForm.get('careSettings').value;

    return this.enrolmentService
      .canRequestRemoteAccess(certifications, careSettings);
  }

  public routeTo(routePath: EnrolmentRoutes, navigationExtras: NavigationExtras = {}) {
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

  public onCopy() {
    this.toastService.openSuccessToast('Your GPID has been copied to clipboard');
  }

  public ngOnInit() {
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
            // Replace enrolment with the version from the form
            enrolment = this.enrolmentFormStateService.json;
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
        })
      ).subscribe();
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
    };
  }
}
