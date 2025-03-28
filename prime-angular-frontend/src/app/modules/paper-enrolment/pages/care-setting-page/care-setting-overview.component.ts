import { Component, Input, ChangeDetectionStrategy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { CareSettingForm } from './care-setting-form.model';

@Component({
  selector: 'app-care-setting-overview',
  template: `
    <app-page-section>
      <app-page-subheader>
        <ng-container appPageSubheaderTitle>Care Setting</ng-container>

        <button mat-icon-button
                matTooltip="Edit Care Setting"
                (click)="onRoute(PaperEnrolmentRoutes.CARE_SETTING)">
          <mat-icon>edit</mat-icon>
        </button>
      </app-page-subheader>

      <ng-container *ngFor="let careSetting of careSettings?.enrolleeCareSettings">

        <app-enrollee-property title="Care Setting"
                               [makeBold]="true">
          {{ careSetting.careSettingCode | configCode: 'careSettings' }}
        </app-enrollee-property>

      </ng-container>

      <app-enrollee-property *ngIf="!careSettings?.enrolleeCareSettings.length"
                             title="Care Setting"
                             [makeBold]="true">
        None
      </app-enrollee-property>

      <button mat-flat-button
          color="primary"
          (click)="onRoute(PaperEnrolmentRoutes.CARE_SETTING)">Edit Care Setting
      </button>
    </app-page-section>

    <app-page-section *ngIf="careSettings?.enrolleeHealthAuthorities.length">
      <app-page-subheader>
        <ng-container appPageSubheaderTitle>Health Authority</ng-container>

        <button mat-icon-button
                matTooltip="Edit Health Authority"
                (click)="onRoute(PaperEnrolmentRoutes.CARE_SETTING)">
          <mat-icon>edit</mat-icon>
        </button>
      </app-page-subheader>

      <ng-container *ngFor="let healthAuthority of careSettings?.enrolleeHealthAuthorities">
        <app-enrollee-property title="Health Authority"
                               [makeBold]="true">
          <div class="mb-3">{{ healthAuthority.healthAuthorityCode | configCode: 'healthAuthorities' }}</div>
        </app-enrollee-property>
      </ng-container>

      <button mat-flat-button
          color="primary"
          (click)="onRoute(PaperEnrolmentRoutes.CARE_SETTING)">Edit Health Authority
      </button>
    </app-page-section>
  `,
  styles: ['mat-icon { font-size: 1em; }'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class CareSettingOverviewComponent extends AbstractOverview {
  @Input() public careSettings: CareSettingForm;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }
}
