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

  public onNavigationStart(): Observable<RouterEvent> {
    return this.router.events.pipe(
      filter(event => event instanceof NavigationStart)
    );
  }

  public onNavigationStop(): Observable<RouterEvent> {
    return this.router.events.pipe(
      filter(event =>
        event instanceof NavigationEnd ||
        event instanceof NavigationCancel ||
        event instanceof NavigationError
      )
    );
  }

  public onNavigationEnd(): Observable<RouterEvent> {
    return this.router.events.pipe(
      filter(event => event instanceof NavigationEnd)
    );
  }
}
