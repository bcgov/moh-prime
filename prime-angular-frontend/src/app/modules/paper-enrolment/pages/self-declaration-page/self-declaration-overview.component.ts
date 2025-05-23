import { Component, Input, ChangeDetectionStrategy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { SelfDeclarationForm } from './self-declaration-form.model';

@Component({
  selector: 'app-self-declaration-overview',
  template: `
    <app-enrollee-self-declarations *ngIf="selfDeclarations?.selfDeclarations"
                                    [showRedirect]="true"
                                    [enrolment]="selfDeclarations"
                                    (route)="onRoute(PaperEnrolmentRoutes.SELF_DECLARATION)">
      <button mat-icon-button
              matTooltip="Edit Self-Declaration"
              (click)="onRoute(PaperEnrolmentRoutes.SELF_DECLARATION)">
        <mat-icon>edit</mat-icon>
      </button>
    </app-enrollee-self-declarations>
  `,
  styles: ['mat-icon { font-size: 1em; }'],
  changeDetection: ChangeDetectionStrategy.Default
})
export class SelfDeclarationOverviewComponent extends AbstractOverview {
  @Input() public selfDeclarations: SelfDeclarationForm;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }
}
