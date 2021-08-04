import { Component, Input, ChangeDetectionStrategy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { OboSitesForm } from './obo-sites-form.model';

@Component({
  selector: 'app-obo-sites-overview',
  template: `
    <app-page-section *ngIf="oboSites?.oboSites?.length">

      <app-page-subheader>
        <ng-container appPageSubheaderTitle>Job Site Information</ng-container>

        <button mat-icon-button
                matTooltip="Edit Job Site Information"
                (click)="onRoute(PaperEnrolmentRoutes.OBO_SITES)">
          <mat-icon>edit</mat-icon>
        </button>
      </app-page-subheader>

      <ng-container *ngFor="let careSetting of enrolleeCareSettings">

        <ng-container *ngFor="let oboSite of oboSites?.oboSites">
          <ng-container *ngIf="oboSite?.careSettingCode === careSetting?.careSettingCode">
            <app-enrollee-property title="Care Setting"
                                   [makeBold]="true">
              <div class="mb-3">{{ careSetting.careSettingCode | configCode: 'careSettings' }}
                <span *ngIf="oboSite?.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY">
                    ({{ careSetting?.healthAuthorityCode | configCode: 'healthAuthorities' | capitalize: true }})
                  </span>
              </div>

              <app-enrollee-property *ngIf="oboSite?.careSettingCode !== CareSettingEnum.HEALTH_AUTHORITY"
                                     title="Site Name"
                                     [makeBold]="true">
                {{ oboSite.siteName | default }}
              </app-enrollee-property>

              <app-enrollee-property *ngIf="oboSite?.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY"
                                     title="Facility Name"
                                     [makeBold]="true">
                {{ oboSite.facilityName | default }}
              </app-enrollee-property>

              <app-enrollee-property *ngIf="oboSite?.careSettingCode !== CareSettingEnum.HEALTH_AUTHORITY"
                                     title="Site ID"
                                     [makeBold]="true">
                {{ oboSite.pec | default }}
              </app-enrollee-property>

              <app-enrollee-property title="Job Title"
                                     [makeBold]="true">
                {{ oboSite.jobTitle | default }}
              </app-enrollee-property>

              <app-enrollee-property title="Site Address"
                                     [makeBold]="true">
                <app-enrollee-property title="Street">
                  {{ oboSite.physicalAddress?.street | default }}
                </app-enrollee-property>

                <app-enrollee-property title="City">
                  {{ oboSite.physicalAddress?.city | default }}
                </app-enrollee-property>

                <app-enrollee-property title="Province">
                  {{ oboSite.physicalAddress?.provinceCode | configCode: 'provinces' | default }}
                </app-enrollee-property>

                <app-enrollee-property title="Postal Code">
                  {{ oboSite.physicalAddress?.postal | postal | default }}
                </app-enrollee-property>

              </app-enrollee-property>
            </app-enrollee-property>
          </ng-container>
        </ng-container>
      </ng-container>

    </app-page-section>
  `,
  styles: ['mat-icon { font-size: 1.2em; }'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class OboSitesOverviewComponent extends AbstractOverview {
  @Input() public oboSites: OboSitesForm;
  @Input() public enrolleeCareSettings: CareSetting[];
  public CareSettingEnum = CareSettingEnum;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }
}
