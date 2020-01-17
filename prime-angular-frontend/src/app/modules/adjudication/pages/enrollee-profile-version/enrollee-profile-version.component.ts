import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';

import { EnrolmentProfileVersion } from '@adjudication/shared/models/enrollee-profile-history.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-enrollee-profile-version',
  templateUrl: './enrollee-profile-version.component.html',
  styleUrls: ['./enrollee-profile-version.component.scss']
})
export class EnrolleeProfileVersionComponent implements OnInit {
  public busy: Subscription;
  public enrolmentProfileHistory: EnrolmentProfileVersion;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private adjudicationResource: AdjudicationResource,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public routeTo() {
    this.router.navigate(['./'], { relativeTo: this.route.parent });
  }

  public ngOnInit() {
    const enrolleeId = this.route.snapshot.params.id;
    const enrolleeProfileVersionId = this.route.snapshot.params.hid;
    this.busy = this.adjudicationResource
      .enrolleeProfileVersion(enrolleeId, enrolleeProfileVersionId)
      .subscribe(
        (enrolmentProfileVersion: EnrolmentProfileVersion) => this.enrolmentProfileHistory = enrolmentProfileVersion,
        (error: any) => {
          this.toastService.openErrorToast('Enrollee history could not be retrieved');
          this.logger.error('[Adjudication] EnrolleeProfileHistory::ngOnInit error has occurred: ', error);
        }
      );
  }
}
