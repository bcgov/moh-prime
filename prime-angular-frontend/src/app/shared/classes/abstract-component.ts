import { ActivatedRoute, Router, NavigationExtras } from '@angular/router';

export interface IAbstractPage {
  routeTo(routePath: string | string[], navigationExtras: NavigationExtras): void;
}

// TODO remove and update views to use FormUtils
export abstract class AbstractComponent implements IAbstractPage {
  protected baseRoutePath: (string | number)[];

  constructor(
    protected route: ActivatedRoute,
    protected router: Router
  ) { }

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
   * Route within a specified base path, for example within a module.
   */
  public routeWithin(routePath: string | (string | number)[], navigationExtras: NavigationExtras = {}) {
    let commands = (Array.isArray(routePath)) ? routePath : [routePath];
    commands = (this.baseRoutePath) ? [...this.baseRoutePath, ...commands] : commands;
    this.routeTo(commands, {
      ...navigationExtras
    });
  }
}
