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
        {{ demographic?.firstName | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Middle Name">
        {{ demographic?.middleName | replace: demographic?.firstName : '' | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Last Name">
        {{ demographic?.lastName | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Date of Birth">
        {{ demographic?.dateOfBirth | formatDate }}
      </app-enrollee-property>
    </app-page-section>

    <app-address-view title="Physical Address"
                      [address]="demographic?.physicalAddress"
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
        {{ demographic?.phone | phone | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Optional Extension Number">
        {{ demographic?.phoneExtension | default }}
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
        {{ demographic?.email | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Optional SMS Phone Number">
        {{ demographic?.smsPhone | phone | default }}
      </app-enrollee-property>
    </app-page-section>
  `,
  styles: [],
  encapsulation: ViewEncapsulation.None
})
export class DemographicOverviewComponent implements OnInit {
  @Input() public demographic: DemographicForm;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;
  private routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public ngOnInit(): void { }

  public onRoute(routePath: string | string[]) {
    routePath = (Array.isArray(routePath)) ? routePath : [routePath];
    this.routeUtils.routeRelativeTo(routePath);
  }
}
