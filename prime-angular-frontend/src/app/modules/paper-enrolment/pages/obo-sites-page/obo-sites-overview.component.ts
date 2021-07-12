import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { OboSitesFormModel } from './obo-sites-form.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

@Component({
  selector: 'app-obo-sites-overview',
  template: `
    <app-page-section *ngIf="oboSiteForm?.oboSites?.length">

      <app-page-subheader>
        <ng-container appPageSubheaderTitle>Job Site Information</ng-container>

        <button *ngIf="true"
                mat-icon-button
                matTooltip="Edit Job Site Information"
                (click)="onRoute(PaperEnrolmentRoutes.OBO_SITES)">
          <mat-icon>edit</mat-icon>
        </button>
      </app-page-subheader>

      <ng-container *ngFor="let careSetting of enrolleeCareSettings">

        <ng-container *ngFor="let oboSite of oboSiteForm?.oboSites">
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
  styles: [],
  encapsulation: ViewEncapsulation.None
})
export class OboSitesOverviewComponent implements OnInit {
  @Input() public oboSiteForm: OboSitesFormModel;
  @Input() public enrolleeCareSettings: CareSetting[];
  public CareSettingEnum = CareSettingEnum;
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
