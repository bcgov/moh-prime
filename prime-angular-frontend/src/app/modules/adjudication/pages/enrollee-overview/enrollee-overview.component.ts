import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '@auth/shared/services/auth.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';
import { AdjudicationContainerComponent } from '@adjudication/shared/components/adjudication-container/adjudication-container.component';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-enrollee-overview',
  templateUrl: './enrollee-overview.component.html',
  styleUrls: ['./enrollee-overview.component.scss']
})
export class EnrolleeOverviewComponent extends AdjudicationContainerComponent
  implements OnInit {
  public enrollee: HttpEnrollee;
  public enrolment: Enrolment;

  constructor(
    @Inject(DIALOG_DEFAULT_OPTION) defaultOptions: DialogDefaultOptions,
    protected route: ActivatedRoute,
    protected router: Router,
    protected adjudicationResource: AdjudicationResource,
    authService: AuthService,
    dialog: MatDialog,
    utilsService: UtilsService,
    toastService: ToastService,
  ) {
    super(defaultOptions,
      route,
      router,
      adjudicationResource,
      authService,
      dialog,
      utilsService,
      toastService);

    this.hasActions = true;
  }

  public ngOnInit(): void {
    super.ngOnInit();

    const enrolleeId = this.route.snapshot.params.id;

    this.busy = this.adjudicationResource.getEnrolleeById(enrolleeId)
      .subscribe((enrollee: HttpEnrollee) => {
        this.enrollee = enrollee;
        this.enrolment = this.enrolmentAdapter(enrollee);
      });
  }

  private enrolmentAdapter(enrollee: HttpEnrollee): Enrolment {
    const {
      userId,
      firstName,
      lastName,
      givenNames,
      preferredFirstName,
      preferredMiddleName,
      preferredLastName,
      dateOfBirth,
      gpid,
      hpdid,
      physicalAddress,
      mailingAddress,
      email,
      smsPhone,
      phone,
      phoneExtension,
      ...remainder
    } = enrollee;

    return {
      enrollee: {
        userId,
        firstName,
        lastName,
        givenNames,
        preferredFirstName,
        preferredMiddleName,
        preferredLastName,
        dateOfBirth,
        gpid,
        hpdid,
        physicalAddress,
        mailingAddress,
        email,
        smsPhone,
        phone,
        phoneExtension
      },
      // Provide the default and allow it to be overridden
      collectionNoticeAccepted: false,
      careSettings: enrollee.enrolleeCareSettings,
      ...remainder
    };
  }
}
