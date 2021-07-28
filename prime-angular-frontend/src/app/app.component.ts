import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute, RouterEvent } from '@angular/router';
import { Title } from '@angular/platform-browser';
import { DOCUMENT } from '@angular/common';

import { map, mergeMap } from 'rxjs/operators';
import { Observable } from 'rxjs';

import { RouteStateService } from '@core/services/route-state.service';
import { UtilsService } from '@core/services/utils.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  constructor(
    @Inject(DOCUMENT) private document: Document,
    private activatedRoute: ActivatedRoute,
    private titleService: Title,
    private routeStateService: RouteStateService,
    private utilsService: UtilsService
  ) { }

  public ngOnInit(): void {
    // const onNavStart = this.routeStateService.onNavigationStart();
    // const onNavStop = this.routeStateService.onNavigationStop();
    const onNavEnd = this.routeStateService.onNavigationEnd();

    this.scrollTop(onNavEnd);
    this.setPageTitle(onNavEnd);
  }

  /**
   * @description
   * Scroll the page to the top on route event.
   */
  private scrollTop(routeEvent: Observable<RouterEvent>) {
    routeEvent.subscribe(() => this.utilsService.scrollTop());
  }

  /**
   * @description
   * Set the HTML page <title> on route event.
   */
  private setPageTitle(routeEvent: Observable<RouterEvent>) {
    routeEvent
      .pipe(
        // Swap what is being observed to the activated route
        map(() => this.activatedRoute),
        // Find the last activated route by traversing over the state tree, and
        // then return it to the stream
        map((route: ActivatedRoute) => {
          while (route.firstChild) { route = route.firstChild; }
          return route;
        }),
        // Only update the page title if the primary route changes
        // filter(route => route.outlet === 'primary'),
        mergeMap((route: ActivatedRoute) => route.data)
      )
      .subscribe((routeData: any) => {
        this.titleService.setTitle(routeData.title);
      });
  }
}
