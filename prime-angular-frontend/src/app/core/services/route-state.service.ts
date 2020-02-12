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
import { filter, tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class RouteStateService {
  private routeState: {
    currentRoutePath: string
  };

  constructor(
    private router: Router
  ) {
    this.routeState = {
      currentRoutePath: ''
    };
  }

  public get currentRoutePath(): string {
    return this.routeState.currentRoutePath;
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
      filter((event: RouterEvent) => event instanceof NavigationEnd),
      // Perform a side-effect to store the previous route
      tap((event: RouterEvent) => this.routeState.currentRoutePath = event.url)
    );
  }
}
