import { Component, Input, ChangeDetectionStrategy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { DemographicForm } from './demographic-form.model';

@Component({
  selector: 'app-demographic-overview',
  template: `
    <app-page-section>
      <app-page-subheader>
        <ng-container appPageSubheaderTitle>Enrollee Information</ng-container>

        <button mat-icon-button
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

        <button mat-icon-button
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

        <button mat-icon-button
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
  styles: ['mat-icon { font-size: 1.2em; }'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class DemographicOverviewComponent extends AbstractOverview {
  @Input() public demographic: DemographicForm;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }
}
