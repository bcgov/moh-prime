import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';

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
        {{ firstName | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Middle Name">
        {{ givenNames | replace: firstName : '' | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Last Name">
        {{ lastName | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Date of Birth">
        {{ dateOfBirth | formatDate }}
      </app-enrollee-property>
    </app-page-section>

    <app-address-view title="Physical Address"
                      [address]="physicalAddress"
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
        {{ phone | phone | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Optional Extension Number">
        {{ phoneExtension | default }}
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
        {{ email | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Optional SMS Phone Number">
        {{ smsPhone | phone | default }}
      </app-enrollee-property>
    </app-page-section>
  `,
  styles: [
  ]
})
export class DemographicOverviewComponent implements OnInit {

  @Input() firstName = '';
  @Input() givenNames = '';
  @Input() lastName = '';
  @Input() dateOfBirth = '';
  @Input() physicalAddress = '';
  @Input() phone = '';
  @Input() phoneExtension = '';
  @Input() email = '';
  @Input() smsPhone = '';
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
