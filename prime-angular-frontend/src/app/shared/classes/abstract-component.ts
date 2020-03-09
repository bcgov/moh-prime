import { ActivatedRoute, Router, NavigationExtras } from '@angular/router';

export interface IAbstractPage {
  routeTo(routePath: string | string[], navigationExtras: NavigationExtras): void;
}

export abstract class AbstractComponent implements IAbstractPage {
  constructor(
    protected route: ActivatedRoute,
    protected router: Router
  ) { }

  public routeTo(routePath: string | (string | number)[], navigationExtras: NavigationExtras = {}): void {
    const commands = (Array.isArray(routePath)) ? routePath : [routePath];
    this.router.navigate(commands, {
      relativeTo: this.route.parent,
      ...navigationExtras
    });
  }
}
