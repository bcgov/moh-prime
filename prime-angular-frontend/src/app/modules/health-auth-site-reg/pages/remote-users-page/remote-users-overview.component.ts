import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';
import { HealthAuthSiteRegRoutes } from '@health-auth/health-auth-site-reg.routes';

import { RemoteUsersForm } from './remote-users-form.model';

@Component({
  selector: 'app-remote-users-overview',
  template: `
    <!--    <div *ngIf="remoteUsers?.length"-->
    <!--         class="row mt-4">-->
    <!--      <div class="col-6">-->
    <!--        <app-page-subheader>-->
    <!--          <ng-container appPageSubheaderTitle>-->
    <!--            <mat-icon class="mr-2">account_circle</mat-icon>-->
    <!--            Remote Practitioners-->
    <!--          </ng-container>-->
    <!--          <button mat-icon-button-->
    <!--                  matTooltip="View Remote Practitioners"-->
    <!--                  (click)="onRoute([organization.id, AdjudicationRoutes.SITE_REGISTRATION, site.id, AdjudicationRoutes.SITE_REMOTE_USERS])">-->
    <!--            <mat-icon>navigate_next icon</mat-icon>-->
    <!--          </button>-->
    <!--        </app-page-subheader>-->
    <!--        <mat-card class="enrolmentCard">-->
    <!--          <mat-card-content>-->
    <!--            <app-remote-user-review [remoteUsers]="site?.remoteUsers"></app-remote-user-review>-->
    <!--          </mat-card-content>-->
    <!--        </mat-card>-->
    <!--      </div>-->
    <!--    </div>-->
  `,
  styles: [],
  encapsulation: ViewEncapsulation.None
})
export class RemoteUsersOverviewComponent extends AbstractOverview implements OnInit {
  @Input() remoteUsers: RemoteUsersForm;
  public HealthAuthSiteRegRoutes = HealthAuthSiteRegRoutes;

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, HealthAuthSiteRegRoutes.MODULE_PATH);
  }

  public ngOnInit(): void { }
}
