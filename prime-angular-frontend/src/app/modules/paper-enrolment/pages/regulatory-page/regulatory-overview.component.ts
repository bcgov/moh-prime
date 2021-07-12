import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { RegulatoryForm } from './regulatory-form.model';

@Component({
  selector: 'app-regulatory-overview',
  template: `
    <app-page-section>
      <app-page-subheader>
        <ng-container appPageSubheaderTitle>College Licence Information</ng-container>

        <button *ngIf="true"
                mat-icon-button
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
  encapsulation: ViewEncapsulation.Emulated
})
export class RegulatoryOverviewComponent implements OnInit {
  @Input() public regulatory: RegulatoryForm;
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
