import { ChangeDetectionStrategy, Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

import { HoursOperationForm } from './hours-operation-form.model';

@Component({
  selector: 'app-hours-operation-overview',
  template: `
    <app-overview-section title="Hours of Operation"
                          [showEditRedirect]="showEditRedirect"
                          [editRoute]="HealthAuthSiteRegRoutes.HOURS_OPERATION"
                          (route)="onRoute($event)">
      <ng-container *ngFor="let businessDay of hoursOperation?.businessHours">
        <app-enrollee-property [title]="businessDay.day | weekday">
          <ng-container *ngIf="businessDay.startTime && businessDay.endTime">
            {{ businessDay.startTime }} to {{ businessDay.endTime }}
          </ng-container>
        </app-enrollee-property>
      </ng-container>
    </app-overview-section>
  `,
  styles: [],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class HoursOperationOverviewComponent extends AbstractOverview implements OnInit {
  @Input() public hoursOperation: HoursOperationForm;
  public HealthAuthSiteRegRoutes = HealthAuthSiteRegRoutes;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }

  public ngOnInit(): void { }
}
