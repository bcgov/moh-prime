import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material';

import { Subscription } from 'rxjs';

import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';

import { EnrolmentProfileVersion } from '@shared/models/enrollee-profile-history.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-enrollee-profile-versions',
  templateUrl: './enrollee-profile-versions.component.html',
  styleUrls: ['./enrollee-profile-versions.component.scss']
})
export class EnrolleeProfileVersionsComponent implements OnInit {
  public busy: Subscription;
  public columns: string[];
  public dataSource: MatTableDataSource<EnrolmentProfileVersion>;

  constructor(
    private route: ActivatedRoute,
    private adjudicationResource: AdjudicationResource,
    private toastService: ToastService,
    private logger: LoggerService
  ) {
    this.columns = ['name', 'createdDate', 'actions'];
  }

  public ngOnInit() {
    const enrolleeId = this.route.snapshot.params.id;
    this.busy = this.adjudicationResource
      .enrolleeProfileVersions(enrolleeId)
      .subscribe(
        (enrolmentProfileVersions: EnrolmentProfileVersion[]) =>
          this.dataSource = new MatTableDataSource<EnrolmentProfileVersion>(enrolmentProfileVersions),
        (error: any) => {
          this.toastService.openErrorToast('Enrollee history could not be retrieved');
          this.logger.error('[Adjudication] EnrolleeProfileHistories::ngOnInit error has occurred: ', error);
        }
      );
  }
}
