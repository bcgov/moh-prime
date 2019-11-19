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

@Injectable({
  providedIn: 'root'
})
export class RouteStateService {

  constructor(
    private router: Router
  ) { }

  /**
   * @description
   * Listener for the route navigation start event.
   */
  public onNavigationStart(): Observable<RouterEvent> {
    return this.router.events.pipe(
      filter(event => event instanceof NavigationStart)
    );
  }

  /**
   * @description
   * Listener for the route navigation stop events.
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
   * @description
   * Listener for the route navigation end event.
   */
  public onNavigationEnd(): Observable<RouterEvent> {
    return this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    );
  }
}
