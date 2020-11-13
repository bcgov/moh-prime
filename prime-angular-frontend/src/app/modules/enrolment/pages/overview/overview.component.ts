import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute, NavigationExtras } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { EMPTY, Subscription, Observable } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';
import { Enrolment } from '@shared/models/enrolment.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { IdentityProviderEnum } from '@auth/shared/enum/identity-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { BaseEnrolmentPage } from '@enrolment/shared/classes/BaseEnrolmentPage';
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
  public currentStatus: EnrolmentStatus;
  public demographicRoutePath: string;
  public identityProvider: IdentityProviderEnum;
  public IdentityProviderEnum = IdentityProviderEnum;
  public EnrolmentStatus = EnrolmentStatus;

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
    private logger: LoggerService
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
      this.toastService.openErrorToast('Your enrolment has an error that needs to be corrected before you will be able to submit');
    }
  }

  public canRequestRemoteAccess(): boolean {
    return this.enrolmentService.canRequestRemoteAccess(this.enrolment.certifications, this.enrolment.careSettings);
  }

  public hasRegOrJob(): boolean {
    return this.enrolmentFormStateService.hasCertificateOrJob();
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

  public ngOnInit() {
    let enrolment = this.enrolmentService.enrolment;

    // Store current status as it will be truncated for initial enrolment
    this.currentStatus = enrolment.currentStatus.statusCode;

    if (this.enrolmentFormStateService.isPatched) {
      enrolment = this.enrolmentFormStateService.json;
      // Merge BCSC information in for use within the view
      const {
        firstName,
        lastName,
        dateOfBirth,
        physicalAddress
      } = this.enrolmentService.enrolment.enrollee;
      enrolment.enrollee = { ...enrolment.enrollee, firstName, lastName, dateOfBirth, physicalAddress };
    }

    // Store a local copy of the enrolment for views
    this.enrolment = enrolment;
    this.isInitialEnrolment = this.enrolmentService.isInitialEnrolment;

    // Attempt to patch the form if not already patched
    this.enrolmentFormStateService.setForm(enrolment);
  }
}
