import { Injectable } from '@angular/core';
import {
  Router,
  // NOTE: Import as RouterEvent to avoid confusion with the DOM Event
  Event as RouterEvent,
  NavigationStart,
  NavigationEnd,
  NavigationCancel,
  NavigationError
} from '@angular/router';

import { Observable } from 'rxjs';
import { filter } from 'rxjs/operators';

@Injectable()
export class RouteStateService {

  constructor(
    private router: Router
  ) { }

  /**
   * Listener for the route navigation start event.
   *
   * @returns {Observable<RouterEvent>}
   * @memberof RouteStateService
   */
  public onNavigationStart(): Observable<RouterEvent> {
    return this.router.events.pipe(
      filter(event => event instanceof NavigationStart)
    );
  }

  /**
   * Listener for the route navigation stop events.
   *
   * @returns {Observable<RouterEvent>}
   * @memberof RouteStateService
   */
  public onNavigationStop(): Observable<RouterEvent> {
    return this.router.events.pipe(
      filter(event =>
        event instanceof NavigationEnd ||
        event instanceof NavigationCancel ||
        event instanceof NavigationError
      )
    );
  }

  /**
   * Listener for the route navigation end event.
   *
   * @returns {Observable<RouterEvent>}
   * @memberof RoutingStateService
   */
  public onNavigationEnd(): Observable<RouterEvent> {
    return this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    );
  }
}
