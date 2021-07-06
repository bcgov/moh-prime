import { Component, Inject, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { map } from 'rxjs/operators';
import { forkJoin } from 'rxjs';

import { PermissionService } from '@auth/shared/services/permission.service';
import { ToastService } from '@core/services/toast.service';
import { UtilsService } from '@core/services/utils.service';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { Enrolment, HttpEnrollee } from '@shared/models/enrolment.model';
import { EnrolleeNavigation } from '@shared/models/enrollee-navigation-model';
import { AdjudicationContainerComponent } from '@adjudication/shared/components/adjudication-container/adjudication-container.component';
import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';

@Component({
  selector: 'app-enrollee-overview',
  templateUrl: './enrollee-overview.component.html',
  styleUrls: ['./enrollee-overview.component.scss']
})
export class EnrolleeOverviewComponent extends AdjudicationContainerComponent implements OnInit {
  public enrollee: HttpEnrollee;
  public enrolment: Enrolment;
  public enrolleeNavigation: EnrolleeNavigation;
  public showAdjudication: boolean;

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

  public onNavigateEnrollee(enrolleeId: number) {
    this.onRoute([enrolleeId, RouteUtils.currentRoutePath(this.router.url)]);
  }

  public ngOnInit(): void {
    this.route.params
      .subscribe(params => this.loadEnrollee(params.id));

    this.action.subscribe(() => this.loadEnrollee(this.route.snapshot.params.id));
  }

  private loadEnrollee(enrolleeId: number): void {
    this.busy =
      forkJoin({
        enrollee: this.adjudicationResource.getEnrolleeById(enrolleeId)
          .pipe(
            map(enrollee => ({
              enrollee: enrollee,
              enrolleeView: this.toEnrolleeListViewModel(enrollee),
              enrolment: this.enrolmentAdapter(enrollee)
            }))
          ),
        enrolleeNavigation: this.adjudicationResource.getAdjacentEnrolleeId(enrolleeId)
      })
        .subscribe(({ enrollee, enrolleeNavigation }) => {
          this.enrollee = enrollee.enrollee;
          this.enrollees = [enrollee.enrolleeView];
          this.enrolment = enrollee.enrolment;
          this.enrolleeNavigation = enrolleeNavigation;
          // hide the adjudication card if enrolment is editable and no 'reason for adjudication'
          this.showAdjudication = !(enrollee.enrollee.currentStatus.statusCode === EnrolmentStatusEnum.EDITABLE
            && !enrollee.enrollee.currentStatus.enrolmentStatusReasons?.length);
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
