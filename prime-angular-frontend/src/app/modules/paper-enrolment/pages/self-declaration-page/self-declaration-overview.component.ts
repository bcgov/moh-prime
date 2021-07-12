import { Component, OnInit, ViewEncapsulation, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { SelfDeclarationForm } from './self-declaration-form.model';

@Component({
  selector: 'app-self-declaration-overview',
  template: `
      <app-enrollee-self-declarations *ngIf="selfDeclarations?.selfDeclarations"
                                      [enrolment]="selfDeclarations"
                                      [showAllSelfDeclarationsQuestions]="true">
        <button *ngIf="true"
                mat-icon-button
                matTooltip="Edit Self-declaration"
                (click)="onRoute(PaperEnrolmentRoutes.SELF_DECLARATION)">
          <mat-icon>edit</mat-icon>
        </button>
      </app-enrollee-self-declarations>
  `,
  styles: [],
  encapsulation: ViewEncapsulation.None
})
export class SelfDeclarationOverviewComponent implements OnInit {
  @Input() public selfDeclarations: SelfDeclarationForm;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;
  private routeUtils: RouteUtils;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public ngOnInit(): void { }

  public onRoute(routePath: string | string[]) {
    routePath = (Array.isArray(routePath)) ? routePath : [routePath];
    this.routeUtils.routeRelativeTo(routePath);
  }
}
