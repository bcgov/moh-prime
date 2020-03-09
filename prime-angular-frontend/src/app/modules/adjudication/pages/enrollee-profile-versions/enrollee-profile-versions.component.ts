import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatTableDataSource } from '@angular/material';

import { Subscription } from 'rxjs';

import { AbstractComponent } from '@shared/classes/abstract-component';
import { HttpEnrolleeProfileVersion } from '@shared/models/enrollee-profile-history.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-enrollee-profile-versions',
  templateUrl: './enrollee-profile-versions.component.html',
  styleUrls: ['./enrollee-profile-versions.component.scss']
})
export class EnrolleeProfileVersionsComponent extends AbstractComponent implements OnInit {
  public busy: Subscription;
  public columns: string[];
  public dataSource: MatTableDataSource<HttpEnrolleeProfileVersion>;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    private adjudicationResource: AdjudicationResource,
  ) {
    super(route, router);

    this.columns = ['name', 'createdDate', 'actions'];
  }

  // TODO update to pass in route from template
  public routeTo() {
    super.routeTo('../../');
  }

  public ngOnInit() {
    const enrolleeId = this.route.snapshot.params.id;
    this.busy = this.adjudicationResource.getEnrolleeProfileVersions(enrolleeId)
      .subscribe((enrolleeProfileVersions: HttpEnrolleeProfileVersion[]) =>
        this.dataSource = new MatTableDataSource<HttpEnrolleeProfileVersion>(enrolleeProfileVersions)
      );
  }
}
