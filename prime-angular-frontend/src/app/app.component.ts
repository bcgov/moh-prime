import { Component, OnInit, Inject } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { DOCUMENT } from '@angular/common';

import { map, mergeMap } from 'rxjs/operators';

import { RouteStateService } from 'src/app/core/services/route-state.service';
import { WindowRefService } from 'src/app/core/services/window-ref.service';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  public title = 'Optimize PRIME: Transforming your services';
  private window: Window;

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private activatedRoute: ActivatedRoute,
    private titleService: Title,
    private routeStateService: RouteStateService,
    private windowRef: WindowRefService,
    private router: Router
  ) {
    this.window = windowRef.nativeWindow;
  }

  public ngOnInit(): void {
    const onNavStart = this.routeStateService.onNavigationStart();
    const onNavStop = this.routeStateService.onNavigationStop();
    const onNavEnd = this.routeStateService.onNavigationEnd();

    this.scrollTop(onNavEnd);
    this.setPageTitle(onNavEnd);
  }

  private scrollTop(routeEvent: any) {
    routeEvent.subscribe(() => {
      const contentContainer = this.document.querySelector('.mat-sidenav-content') || this.window;
      contentContainer.scroll({ top: 0, left: 0, behavior: 'smooth' });
    });
  }

  private setPageTitle(routeEvent: any) {
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
        const title = routeData.title;
        this.titleService.setTitle(title);
      });
  }
}
