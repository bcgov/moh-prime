import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { RemoteUser } from '@lib/models/remote-user.model';
import { SiteResource } from '@core/resources/site-resource.service';
import { Site } from '@registration/shared/models/site.model';

@Component({
  selector: 'app-site-remote-users',
  templateUrl: './site-remote-users.component.html',
  styleUrls: ['./site-remote-users.component.scss']
})
export class SiteRemoteUsersComponent implements OnInit {
  public title: string;
  public hasActions: boolean;
  public remoteUsers: Observable<RemoteUser[]>;

  constructor(
    private route: ActivatedRoute,
    private siteResource: SiteResource
  ) {
    this.title = this.route.snapshot.data.title;
    this.hasActions = false;
  }

  public ngOnInit(): void {
    this.getRemoteUsers();
  }

  private getRemoteUsers() {
    const siteId = this.route.snapshot.params.sid;
    this.remoteUsers = this.siteResource.getSiteById(siteId)
      .pipe(
        map((site: Site) => site.remoteUsers)
      );
  }
}
