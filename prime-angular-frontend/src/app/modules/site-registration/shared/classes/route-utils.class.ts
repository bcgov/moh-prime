import { Router, NavigationExtras, ActivatedRoute } from '@angular/router';

export class RouteUtils {
  private route: ActivatedRoute;
  private router: Router;
  private baseRoutePath: string;

  constructor(
    route: ActivatedRoute,
    router: Router,
    baseRoutePath: string
  ) {
    this.route = route;
    this.router = router;
    this.baseRoutePath = baseRoutePath;
  }

  /**
   * @description
   * Route from a base route.
   */
  public routeTo(routePath: string | (string | number)[], navigationExtras: NavigationExtras = {}): void {
    const commands = (Array.isArray(routePath)) ? routePath : [routePath];
    this.router.navigate(commands, {
      ...navigationExtras
    });
  }

  /**
   * @description
   * Route relative to the active route.
   */
  public routeRelativeTo(routePath: string | (string | number)[], navigationExtras: NavigationExtras = {}) {
    this.routeTo(routePath, {
      relativeTo: this.route.parent,
      ...navigationExtras
    });
  }

  /**
   * @description
   * Route within a specified base path, for example within a module, otherwise uses root.
   */
  public routeWithin(routePath: string | (string | number)[], navigationExtras: NavigationExtras = {}) {
    let commands = (Array.isArray(routePath)) ? routePath : [routePath];
    commands = (this.baseRoutePath) ? [...this.baseRoutePath, ...commands] : commands;
    this.routeTo(commands, {
      ...navigationExtras
    });
  }
}
