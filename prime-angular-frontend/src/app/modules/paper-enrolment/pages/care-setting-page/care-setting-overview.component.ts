import { Component, OnInit, Input } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { CareSettingForm } from './care-setting-form.model';

@Component({
  selector: 'app-care-setting-overview',
  template: `
    <app-page-section>
      <app-page-subheader>
        <ng-container appPageSubheaderTitle>Care Setting</ng-container>

        <button *ngIf="true"
                mat-icon-button
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
    </app-page-section>

    <app-page-section *ngIf="careSettings?.enrolleeHealthAuthorities.length">
      <app-page-subheader>
        <ng-container appPageSubheaderTitle>Health Authority</ng-container>

        <button *ngIf="true"
                mat-icon-button
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
    </app-page-section>
  `,
  styles: [
  ]
})
export class CareSettingOverviewComponent implements OnInit {

  @Input() careSettings: CareSettingForm;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;
  public routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public ngOnInit(): void {
    // console.log(`caresettings ${this.careSettings}`);
  }

  public onRoute(routePath: string | string[]) {
    routePath = (Array.isArray(routePath)) ? routePath : [routePath];
    this.routeUtils.routeRelativeTo(routePath);
  }
}
