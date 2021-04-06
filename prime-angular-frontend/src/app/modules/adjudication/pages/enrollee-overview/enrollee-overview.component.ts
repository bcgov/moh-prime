import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs/operators';

import { PermissionService } from '@auth/shared/services/permission.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';
import { AdjudicationContainerComponent } from '@adjudication/shared/components/adjudication-container/adjudication-container.component';

@Component({
  selector: 'app-enrollee-overview',
  templateUrl: './enrollee-overview.component.html',
  styleUrls: ['./enrollee-overview.component.scss']
})
export class EnrolleeOverviewComponent extends AdjudicationContainerComponent implements OnInit {
  public enrollee: HttpEnrollee;
  public enrolment: Enrolment;

  constructor(
    @Inject(DIALOG_DEFAULT_OPTION) defaultOptions: DialogDefaultOptions,
    protected route: ActivatedRoute,
    protected router: Router,
    protected adjudicationResource: AdjudicationResource,
    permissionService: PermissionService,
    dialog: MatDialog,
    utilsService: UtilsService,
    toastService: ToastService,
  ) {
    super(defaultOptions,
      route,
      router,
      adjudicationResource,
      permissionService,
      dialog,
      utilsService,
      toastService);

    this.hasActions = true;
  }

  public ngOnInit(): void {
    super.ngOnInit();

    const enrolleeId = this.route.snapshot.params.id;

    this.busy = this.adjudicationResource.getEnrolleeById(enrolleeId)
      .pipe(
        map((enrollee: HttpEnrollee) => [enrollee, this.enrolmentAdapter(enrollee)])
      )
      .subscribe(([enrollee, enrolment]: [HttpEnrollee, Enrolment]) => {
        this.enrollee = enrollee;
        this.enrolment = enrolment;
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
      verifiedAddress,
      mailingAddress,
      physicalAddress,
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
        verifiedAddress,
        mailingAddress,
        physicalAddress,
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
