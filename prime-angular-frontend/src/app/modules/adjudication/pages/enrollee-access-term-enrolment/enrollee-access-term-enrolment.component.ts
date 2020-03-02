import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { EnrolmentProfileVersion } from '@shared/models/enrollee-profile-history.model';
import { ActivatedRoute, Router } from '@angular/router';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';

@Component({
  selector: 'app-enrollee-access-term-enrolment',
  templateUrl: './enrollee-access-term-enrolment.component.html',
  styleUrls: ['./enrollee-access-term-enrolment.component.scss']
})
export class EnrolleeAccessTermEnrolmentComponent implements OnInit {
  public busy: Subscription;
  public enrolmentProfileHistory: EnrolmentProfileVersion;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private adjudicationResource: AdjudicationResource,
    private toastService: ToastService,
    private logger: LoggerService,
  ) { }

  public routeTo() {
    this.router.navigate(
      ['../'],
      { relativeTo: this.route.parent }
    );
  }

  public ngOnInit() {
    const enrolleeId = this.route.snapshot.params.id;
    const accessTermId = this.route.snapshot.params.hid;
    this.busy = this.adjudicationResource
      .getEnrolmentProfileForAccessTerm(enrolleeId, accessTermId)
      .subscribe(
        (enrolmentProfileVersion: EnrolmentProfileVersion) => this.enrolmentProfileHistory = enrolmentProfileVersion,
        (error: any) => {
          this.toastService.openErrorToast('Enrollee history could not be retrieved');
          this.logger.error('[ADJUDICATION] EnrolleeAccessTermEnrolmentComponent::ngOnInit error has occurred: ', error);
        }
      );
  }
}
