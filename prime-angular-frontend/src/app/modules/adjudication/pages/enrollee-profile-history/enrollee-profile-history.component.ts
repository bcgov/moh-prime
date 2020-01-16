import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { Enrolment } from '@shared/models/enrolment.model';

import { EnrolmentProfileHistory } from '@adjudication/shared/models/enrollee-profile-history.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-enrollee-profile-history',
  templateUrl: './enrollee-profile-history.component.html',
  styleUrls: ['./enrollee-profile-history.component.scss']
})
export class EnrolleeProfileHistoryComponent implements OnInit {
  public busy: Subscription;
  public enrolmentProfileHistory: EnrolmentProfileHistory;

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
    const enrolleeHistoryId = this.route.snapshot.params.hid;
    this.busy = this.adjudicationResource
      .enrolleeProfileHistory(enrolleeId, enrolleeHistoryId)
      .subscribe(
        (enrolmentProfileHistory: EnrolmentProfileHistory) => this.enrolmentProfileHistory = enrolmentProfileHistory,
        (error: any) => {
          this.toastService.openErrorToast('Enrollee history could not be retrieved');
          this.logger.error('[Adjudication] EnrolleeProfileHistory::ngOnInit error has occurred: ', error);
        }
      );
  }
}
