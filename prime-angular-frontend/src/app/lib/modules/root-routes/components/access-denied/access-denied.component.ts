import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RouteUtils } from '@lib/utils/route-utils.class';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';

@Component({
  selector: 'app-access-denied',
  templateUrl: './access-denied.component.html',
  styleUrls: ['./access-denied.component.scss']
})
export class AccessDeniedComponent implements OnInit {
  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private route: ActivatedRoute,
    private router: Router
  ) { }

  public routeToRoot() {
    this.router.navigateByUrl(RouteUtils.currentModulePath(this.route));
  }

  public ngOnInit() { }
}
