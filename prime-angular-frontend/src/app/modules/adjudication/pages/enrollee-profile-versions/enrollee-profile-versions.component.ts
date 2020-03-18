import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatTableDataSource } from '@angular/material';

import { Subscription } from 'rxjs';

import { HttpEnrolleeProfileVersion } from '@shared/models/enrollee-profile-history.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-enrollee-profile-versions',
  templateUrl: './enrollee-profile-versions.component.html',
  styleUrls: ['./enrollee-profile-versions.component.scss']
})
export class EnrolleeProfileVersionsComponent implements OnInit {
  public busy: Subscription;
  public dataSource: MatTableDataSource<HttpEnrolleeProfileVersion>;
  public columns: string[];

  constructor(
    private route: ActivatedRoute,
    private adjudicationResource: AdjudicationResource,
  ) {
    this.columns = ['name', 'createdDate', 'actions'];
  }

  public ngOnInit() {
    const enrolleeId = this.route.snapshot.params.id;
    this.busy = this.adjudicationResource.getEnrolleeProfileVersions(enrolleeId)
      .subscribe((enrolleeProfileVersions: HttpEnrolleeProfileVersion[]) =>
        this.dataSource = new MatTableDataSource<HttpEnrolleeProfileVersion>(enrolleeProfileVersions)
      );
  }
}
