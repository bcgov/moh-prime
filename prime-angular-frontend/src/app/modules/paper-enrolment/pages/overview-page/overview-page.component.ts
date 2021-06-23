import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationExtras } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { Subscription } from 'rxjs';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { HttpEnrollee } from '@shared/models/enrolment.model';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';

@Component({
  selector: 'app-overview-page',
  templateUrl: './overview-page.component.html',
  styleUrls: ['./overview-page.component.scss']
})
export class OverviewPageComponent extends AbstractEnrolmentPage implements OnInit {
  public busy: Subscription;
  public enrollee: HttpEnrollee;


  public currentStatus: EnrolmentStatus;
  public EnrolmentStatus = EnrolmentStatus;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private enrolmentResource: EnrolmentResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.currentStatus = null;
  }

  public onSubmit() {
    // if (this.enrolmentFormStateService.isValid) {
    //   const enrolment = this.enrolmentFormStateService.json;
    //   const data: DialogOptions = {
    //     title: 'Submit Enrolment',
    //     message: 'When your enrolment is submitted for adjudication, it can no longer be updated. Are you ready to submit your enrolment?',
    //     actionText: 'Submit Enrolment'
    //   };
    //   this.busy = this.dialog.open(ConfirmDialogComponent, { data })
    //     .afterClosed()
    //     .pipe(
    //       exhaustMap((result: boolean) =>
    //         (result)
    //           ? this.enrolmentResource.submitApplication(enrolment)
    //           : EMPTY
    //       )
    //     )
    //     .subscribe(() => {
    //       this.toastService.openSuccessToast('Enrolment has been submitted');
    //       this.routeTo(EnrolmentRoutes.CHANGES_SAVED);
    //     });
    // } else {
    //   this.enrolmentFormStateService.forms.forEach((form: FormGroup) => this.formUtilsService.logFormErrors(form));
    //   this.toastService.openErrorToast('Your enrolment has an error that needs to be corrected before you will be able to submit');
    // }
  }

  public canRequestRemoteAccess(): boolean {
    // const certifications = this.enrolmentFormStateService.regulatoryFormState.collegeCertifications;
    // const careSettings = this.enrolmentFormStateService.careSettingsForm.get('careSettings').value;
    //
    // return this.enrolmentService
    //   .canRequestRemoteAccess(certifications, careSettings);
  }

  public hasRegOrJob(): boolean {
    // return this.enrolmentFormStateService.hasCertificateOrJob();
  }

  public routeTo(routePath: EnrolmentRoutes, navigationExtras: NavigationExtras = {}) {
    // this.allowRoutingWhenDirty = true;
    // super.routeTo(routePath, navigationExtras);
  }

  public ngOnInit(): void {
    // this.authService.getUser$()
    //   .pipe(
    //     map(({ firstName, lastName, givenNames, dateOfBirth, verifiedAddress }: BcscUser) => {
    //       // Initial assumption is a user has authenticated, been redirected to
    //       // this view, and not made any changes to the state of their enrolment
    //       // so use the source of truth that is populated from the server
    //       let enrolment = this.enrolmentService.enrolment;
    //
    //       // Store current status as it will be truncated for initial enrolment
    //       this.currentStatus = enrolment.currentStatus.statusCode;
    //
    //       // Form being patched indicates that there is possibly changes that reside
    //       // in the form for submission, and they should be reflected in the view
    //       if (this.enrolmentFormStateService.isPatched) {
    //         // Replace enrolment with the version from the form
    //         enrolment = this.enrolmentFormStateService.json;
    //       }
    //
    //       // Allow for BCSC information to be updated on each submission of the enrolment
    //       // regardless of whether they visited the demographic view to make adjustments
    //       const form = this.enrolmentFormStateService.bcscDemographicFormState.form;
    //       if (!verifiedAddress) {
    //         this.formUtilsService.resetAndClearValidators(form.get('verifiedAddress') as FormGroup);
    //         verifiedAddress = new Address();
    //       }
    //
    //       // Merge current BCSC information that may not be stored in the form
    //       // or in the enrolment for use within the view
    //       enrolment.enrollee = { ...enrolment.enrollee, firstName, lastName, givenNames, dateOfBirth, verifiedAddress };
    //
    //       // Store a local copy of the enrolment for views
    //       this.enrollee = enrolment;
    //       this.isInitialEnrolment = this.enrolmentService.isInitialEnrolment;
    //
    //       // Attempt to patch the form if not already patched
    //       this.enrolmentFormStateService.setForm(enrolment);
    //     })
    //   ).subscribe();
  }
}
