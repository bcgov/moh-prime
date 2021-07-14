import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';

import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';
import { AuthorizedUserService } from '@health-auth/shared/services/authorized-user.service';

@Component({
  selector: 'app-authorized-user-approved-page',
  templateUrl: './authorized-user-approved-page.component.html',
  styleUrls: ['./authorized-user-approved-page.component.scss']
})
export class AuthorizedUserApprovedPageComponent implements OnInit {
  constructor(
    private router: Router,
    private authorizedUserService: AuthorizedUserService,
    private healthAuthorityResource: HealthAuthorityResource
  ) { }

  public onContinue(): void {
    const authorizedUserId = this.authorizedUserService.authorizedUser.id;
    this.healthAuthorityResource.activateAuthorizedUser(authorizedUserId)
      .subscribe(() => this.router.navigate([HealthAuthSiteRegRoutes.MODULE_PATH]));
  }

  public ngOnInit(): void { }
}
