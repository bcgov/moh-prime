import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Role } from '@auth/shared/enum/role.enum';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { AccessStatusEnum } from '@health-auth/shared/enums/access-status.enum';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { AuthorizedUser } from '@shared/models/authorized-user.model';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-authorized-user-review',
  templateUrl: './authorized-user-review.component.html',
  styleUrls: ['./authorized-user-review.component.scss']
})
export class AuthorizedUserReviewComponent implements OnInit {
  public routeUtils: RouteUtils;
  public busy: Subscription;
  public user: AuthorizedUser;
  public healthAuthorities: Config<number>[];

  public Role = Role;
  public AccessStatusEnum = AccessStatusEnum;

  constructor(
    private route: ActivatedRoute,
    private healthAuthorityResource: HealthAuthorityResource,
    private configService: ConfigService,
    private router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.HEALTH_AUTHORITIES);
    this.healthAuthorities = this.configService.healthAuthorities;
  }

  public onApprove() {
    this.busy = this.healthAuthorityResource
      .approveAuthorizedUser(this.route.snapshot.params.auid)
      .subscribe(() => this.routeUtils.routeRelativeTo(['../', AdjudicationRoutes.AUTHORIZED_USERS]));
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(['../', AdjudicationRoutes.AUTHORIZED_USERS]);
  }

  public ngOnInit(): void {
    this.getAuthorizedUser();
  }

  protected getAuthorizedUser() {
    this.busy = this.healthAuthorityResource
      .getAuthorizedUserById(this.route.snapshot.params.auid)
      .subscribe((user: AuthorizedUser) => this.user = user);
  }
}
