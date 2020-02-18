import { Injectable } from '@angular/core';
import {
  Router,
  RouterEvent,
  NavigationStart,
  NavigationEnd,
  NavigationCancel,
  NavigationError
} from '@angular/router';

import { Observable } from 'rxjs';
import { filter, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RouteStateService {
  constructor(
    private router: Router
  ) { }

  public get routePath$(): Observable<string> {
    return this.onNavigationEnd()
      .pipe(
        map((event: RouterEvent) => event.url)
      );
  }

  /**
   * @description
   * Listener for the route navigation start event.
   */
  public onNavigationStart(): Observable<RouterEvent> {
    return this.router.events.pipe(
      filter((event: RouterEvent) => event instanceof NavigationStart)
    );
  }

  /**
   * @description
   * Listener for the route navigation stop events.
   */
  public onNavigationStop(): Observable<RouterEvent> {
    return this.router.events.pipe(
      filter((event: RouterEvent) =>
        event instanceof NavigationEnd ||
        event instanceof NavigationCancel ||
        event instanceof NavigationError
      )
    );
  }

  /**
   * @description
   * Listener for the route navigation end event.
   */
  public onNavigationEnd(): Observable<RouterEvent> {
    return this.router.events.pipe(
      filter((event: RouterEvent) => event instanceof NavigationEnd)
    );
  }
}
