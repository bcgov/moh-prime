import { Component, Input, ChangeDetectionStrategy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { SelfDeclarationForm } from './self-declaration-form.model';

@Component({
  selector: 'app-self-declaration-overview',
  template: `
    <app-enrollee-self-declarations *ngIf="selfDeclarations?.selfDeclarations"
                                    [enrolment]="selfDeclarations"
                                    [showAllSelfDeclarationsQuestions]="true">
      <button mat-icon-button
              matTooltip="Edit Self-declaration"
              (click)="onRoute(PaperEnrolmentRoutes.SELF_DECLARATION)">
        <mat-icon>edit</mat-icon>
      </button>
    </app-enrollee-self-declarations>
  `,
  styles: ['mat-icon { font-size: 1.2em; }'],
  changeDetection: ChangeDetectionStrategy.OnPush
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
