import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { DemographicForm } from './demographic-form.model';

@Component({
  selector: 'app-demographic-overview',
  template: `
    <app-page-section>
      <app-page-subheader>
        <ng-container appPageSubheaderTitle>Enrollee Information</ng-container>

        <button *ngIf="true"
                mat-icon-button
                matTooltip="Edit Preferred Name"
                (click)="onRoute(PaperEnrolmentRoutes.DEMOGRAPHIC)">
          <mat-icon>edit</mat-icon>
        </button>
      </app-page-subheader>

      <app-enrollee-property title="First Name">
        {{ enrollee.firstName | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Middle Name">
        {{ enrollee.middleName | replace: enrollee.firstName : '' | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Last Name">
        {{ enrollee.lastName | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Date of Birth">
        {{ enrollee.dateOfBirth | formatDate }}
      </app-enrollee-property>
    </app-page-section>

    <app-address-view title="Physical Address"
                      [address]="enrollee.physicalAddress"
                      [showRedirect]="true"
                      [showIfEmpty]="true"
                      (route)="onRoute(PaperEnrolmentRoutes.DEMOGRAPHIC)"></app-address-view>

    <app-page-section>
      <app-page-subheader>
        <ng-container appPageSubheaderTitle>Contact Information</ng-container>

        <button *ngIf="true"
                mat-icon-button
                matTooltip="Edit Contact Information"
                (click)="onRoute(PaperEnrolmentRoutes.DEMOGRAPHIC)">
          <mat-icon>edit</mat-icon>
        </button>
      </app-page-subheader>

      <app-enrollee-property title="Phone Number">
        {{ enrollee.phone | phone | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Optional Extension Number">
        {{ enrollee.phoneExtension | default }}
      </app-enrollee-property>
    </app-page-section>

    <app-page-section>
      <app-page-subheader>
        <ng-container appPageSubheaderTitle>Notification Information</ng-container>

        <button *ngIf="true"
                mat-icon-button
                matTooltip="Edit Notification Information"
                (click)="onRoute(PaperEnrolmentRoutes.DEMOGRAPHIC)">
          <mat-icon>edit</mat-icon>
        </button>
      </app-page-subheader>

      <app-enrollee-property title="Email">
        {{ enrollee.email | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Optional SMS Phone Number">
        {{ enrollee.smsPhone | phone | default }}
      </app-enrollee-property>
    </app-page-section>
  `,
  styles: [
  ],
  encapsulation: ViewEncapsulation.None
})
export class DemographicOverviewComponent implements OnInit {

  @Input() enrollee: DemographicForm;
  public routeUtils: RouteUtils;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;

  constructor(
    private route: ActivatedRoute,
    router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }


  public ngOnInit(): void {
  }

  public onRoute(routePath: string | string[]) {
    routePath = (Array.isArray(routePath)) ? routePath : [routePath];
    this.routeUtils.routeRelativeTo(routePath);
  }
}
