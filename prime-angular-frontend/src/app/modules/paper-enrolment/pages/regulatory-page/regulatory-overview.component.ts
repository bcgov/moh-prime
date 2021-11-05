import { Component, Input, ChangeDetectionStrategy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { RegulatoryForm } from './regulatory-form.model';

@Component({
  selector: 'app-regulatory-overview',
  template: `
    <app-page-section>
      <app-page-subheader>
        <ng-container appPageSubheaderTitle>Device Provider ID</ng-container>

        <button *ngIf="showEditRedirect"
                mat-icon-button
                matTooltip="Edit Device Provider ID"
                (click)="onRoute(PaperEnrolmentRoutes.REGULATORY)">
          <mat-icon>edit</mat-icon>
        </button>
      </app-page-subheader>

      <app-enrollee-property title="Device Provider ID"
                            [makeBold]="true">
        {{ deviceProviderIdentifier | default }}
      </app-enrollee-property>
    </app-page-section>

    <app-page-section>
      <app-page-subheader>
        <ng-container appPageSubheaderTitle>College Licence Information</ng-container>

        <button mat-icon-button
                matTooltip="Edit College Licenses"
                (click)="onRoute(PaperEnrolmentRoutes.REGULATORY)">
          <mat-icon>edit</mat-icon>
        </button>
      </app-page-subheader>

      <ng-container *ngFor="let certification of regulatory?.certifications; let i = index;">

        <app-enrollee-property title="College Licence"
                               [makeBold]="true">
          {{ certification?.collegeCode | configCode: 'colleges' | default }}
        </app-enrollee-property>

        <app-enrollee-property title="Licence Class">
          {{ certification?.licenseCode | configCode: 'licenses' | default }}
        </app-enrollee-property>

        <app-enrollee-property title="Licence Number">
          {{ certification?.licenseNumber | default }}
        </app-enrollee-property>

        <app-enrollee-property title="Prescriber ID">
          {{ certification?.practitionerId | default }}
        </app-enrollee-property>

        <app-enrollee-property title="Renewal Date">
          {{ certification?.renewalDate | formatDate | default }}
        </app-enrollee-property>

        <app-enrollee-property title="Advanced Practices">
          {{ certification?.practiceCode | configCode: 'practices' | default }}
        </app-enrollee-property>

      </ng-container>

      <app-enrollee-property *ngIf="!regulatory?.certifications?.length"
                             title="College Certification"
                             [makeBold]="true">
        None
      </app-enrollee-property>
    </app-page-section>
  `,
  styles: ['mat-icon { font-size: 1.2em; }'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RegulatoryOverviewComponent extends AbstractOverview {
  @Input() public regulatory: RegulatoryForm;
  @Input() public deviceProviderIdentifier: string;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }
}
