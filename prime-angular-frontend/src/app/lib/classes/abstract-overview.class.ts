import { Directive, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';

/**
 * @description
 * Class is used to provide a set of methods for
 * overview components.
 */
@Directive()
// eslint-disable-next-line @angular-eslint/directive-class-suffix
export abstract class AbstractOverview {
  @Input() public showEditRedirect: boolean;
  public routeUtils: RouteUtils;

  protected constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    baseRoute: string | (string | number)[]
  ) {
    this.showEditRedirect = true;
    this.routeUtils = new RouteUtils(route, router, baseRoute);
  }

  public onRoute(routePath: string): void {
    this.routeUtils.routeRelativeTo(routePath);
  }
}
