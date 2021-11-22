import { Router, NavigationExtras, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

export type RoutePath = string | RouteSegments;
export type RouteSegments = (string | number)[];

export class RouteUtils {
  private route: ActivatedRoute;
  private router: Router;
  private readonly baseRoutePath: string;
  private readonly location: Location;

  constructor(
    route: ActivatedRoute,
    router: Router,
    baseRoutePath: RoutePath,
    // TODO temporary default to reduce application wide refactor, and should be removed in separate PR
    location: Location = null
  ) {
    this.route = route;
    this.router = router;
    this.baseRoutePath = (Array.isArray(baseRoutePath))
      ? baseRoutePath.join('/')
      : baseRoutePath;
    this.location = location;
  }

  /**
   * @description
   * Determine the current module route path, or provide a default
   * if the current route path is the module path.
   */
  public static currentModulePath(route: ActivatedRoute, defaultRoutePath: string = '/'): string {
    const urlSegments = route.snapshot.url;

    // Prevent cyclical routing
    return (urlSegments.length > 1)
      ? urlSegments[0].path
      : defaultRoutePath;
  }

  /**
   * @description
   * Determine the current route path of a URL by removing query and
   * URI params that can't be mapped to existing module routes.
   */
  public static currentRoutePath(url: string): string {
    // Truncate query parameters
    return url.split('?')
      .shift()
      // List the remaining URI params
      .split('/')
      // Remove URI params that are numbers
      .filter(p => !/^\d+$/.test(p))
      // Remove blacklisted URI params
      .filter(p => !['new'].includes(p))
      .pop(); // Current route is the last index
  }

  /**
   * @description
   * Route from a base route.
   */
  public routeTo(routePath: RoutePath, navigationExtras: NavigationExtras = {}): void {
    const commands = (Array.isArray(routePath)) ? routePath : [routePath];
    this.router.navigate(commands, {
      ...navigationExtras
    });
  }

  /**
   * @description
   * Route relative to the active route.
   */
  public routeRelativeTo(routePath: RoutePath, navigationExtras: NavigationExtras = {}): void {
    this.routeTo(routePath, {
      relativeTo: this.route.parent,
      ...navigationExtras
    });
  }

  /**
   * @description
   * Route within a specified base path, for example within a
   * module, otherwise uses root.
   */
  public routeWithin(routePath: RoutePath, navigationExtras: NavigationExtras = {}): void {
    let commands = (Array.isArray(routePath)) ? routePath : [routePath];
    commands = (this.baseRoutePath) ? [this.baseRoutePath, ...commands] : commands;
    this.routeTo(commands, {
      ...navigationExtras
    });
  }

  /**
   * @description
   * Update the query parameters on the current route without routing
   * to a view. Query parameters are merged, but can be removed by
   * setting the keys value to `null`.
   */
  public updateQueryParams(queryParams: { [key: string]: any }): void {
    // Passing `null` values removes the query parameter from the URL
    queryParams = { ...this.route.snapshot.queryParams, ...queryParams };
    this.router.navigate([], { queryParams });
  }

  /**
   * @description
   * Remove every query parameter on the current route without routing
   * to a view.
   */
  public removeQueryParams(queryParams: { [key: string]: any } = {}): void {
    this.router.navigate([], { queryParams });
  }

  /**
   * @description
   * Replace the current URL and prevent it from being pushed onto
   * browser history, and then route accordingly.
   *
   * Chainable with other RouteUtil methods since routing
   * would commonly occur after invocation.
   *
   * NOTE: Replaces the URL, but does not update route state, which
   * may require the route-level to be moved up using '../' and then
   * down to current route-level to have it be overridden.
   *
   * @example
   * this.routeUtils
   *   .replaceState(['module', 'resource', id, 'action'])
   *   .routeRelativeTo(['../', id, 'path']);
   */
  public replaceState(replacementRoutePath: RoutePath): RouteUtils {
    if (!this.location) {
      throw Error('Location service was not provided to RouteUtils');
    }

    replacementRoutePath = (Array.isArray(replacementRoutePath))
      ? replacementRoutePath.join('/')
      : replacementRoutePath;
    this.location.replaceState(replacementRoutePath);

    return this;
  }
}
