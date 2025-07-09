import { Component, Input, ChangeDetectionStrategy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { AbstractOverview } from '@lib/classes/abstract-overview.class';

import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { RegulatoryForm } from './regulatory-form.model';
import { EnrolleeDeviceProvider } from '@shared/models/enrollee-device-provider.model';

@Component({
  selector: 'app-regulatory-overview',
  template: `
    <app-page-section>
      <app-page-subheader>
        <ng-container appPageSubheaderTitle>Device Provider ID</ng-container>

        <button *ngIf="showEditRedirect"
                mat-icon-button
                matTooltip="Edit Device Provider ID"
                (click)="onRoute(PaperEnrolmentRoutes.REGULATORY)">
          <mat-icon>edit</mat-icon>
        </button>
      </app-page-subheader>

      <app-enrollee-property title="User Role"
                            [makeBold]="true">
        {{ deviceProviderUseRoleCode | configCode: 'deviecProviderRoles' | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Device Provider ID"
                            [makeBold]="true">
        {{ deviceProviderId | default }}
      </app-enrollee-property>

      <app-enrollee-property title="Certificate Number"
                            [makeBold]="true">
        {{ certificationNumber | default }}
      </app-enrollee-property>
      <button mat-flat-button
          color="primary"
          (click)="onRoute(PaperEnrolmentRoutes.REGULATORY)">Edit Device Provider ID
      </button>
    </app-page-section>

    <app-page-section>
      <app-page-subheader>
        <ng-container appPageSubheaderTitle>College Licence Information</ng-container>

        <button mat-icon-button
                matTooltip="Edit College Licences"
                (click)="onRoute(PaperEnrolmentRoutes.REGULATORY)">
          <mat-icon>edit</mat-icon>
        </button>
      </app-page-subheader>

      <ng-container *ngFor="let certification of regulatory?.certifications; let i = index;">

        <app-enrollee-property title="College Licence"
                               [makeBold]="true">
          {{ certification?.collegeCode | configCode: 'colleges' | default }}
        </app-enrollee-property>

        <app-enrollee-property title="Licence Class">
          {{ certification?.licenseCode | configCode: 'licenses' | default }}
        </app-enrollee-property>

        <app-enrollee-property [title]="certification?.collegeCode | licenseNumberLabel">
          {{ certification?.licenseNumber | default }}
        </app-enrollee-property>

        <app-enrollee-property *ngIf="certification?.practitionerId" title="PharmaNet ID">
          {{ certification?.practitionerId | default }}
        </app-enrollee-property>

        <app-enrollee-property title="Renewal Date">
          {{ certification?.renewalDate | formatDate | default }}
        </app-enrollee-property>

        <app-enrollee-property *ngIf="certification?.practiceCode"
                              title="Advanced Practices">
          {{ certification?.practiceCode | configCode: 'practices' | default }}
        </app-enrollee-property>

      </ng-container>

      <app-enrollee-property *ngIf="!regulatory?.certifications?.length"
                             title="College Licence"
                             [makeBold]="true">
        <div class="mb-2">
          None
        </div>
      </app-enrollee-property>

      <button mat-flat-button
          color="primary"
          (click)="onRoute(PaperEnrolmentRoutes.REGULATORY)">Edit College Licences
      </button>
    </app-page-section>

    <app-page-section>

      <app-page-subheader>
        <ng-container appPageSubheaderTitle>Other College Licence Information</ng-container>

        <button mat-icon-button
                matTooltip="Edit Other College Licences"
                (click)="onRoute(PaperEnrolmentRoutes.REGULATORY)">
          <mat-icon>edit</mat-icon>
        </button>
      </app-page-subheader>


      <ng-container *ngIf="!regulatory?.unlistedCertifications?.length; else unlistedCerts">
        <div class="mb-2">
          No additional licence information entered
        </div>
      </ng-container>

      <ng-template #unlistedCerts>
        <ng-container *ngFor="let unlistedCertification of regulatory?.unlistedCertifications; let i = index;">

          <app-enrollee-property title="College Name"
            [makeBold]="true">
            {{ unlistedCertification.collegeName | default }}
          </app-enrollee-property>

          <app-enrollee-property title="College Licence"
            [makeBold]="true">
            {{ unlistedCertification.licenceNumber | default }}
          </app-enrollee-property>

          <app-enrollee-property title="Renewal Date"
            [makeBold]="true">
            {{ unlistedCertification.renewalDate | formatDate | default }}
          </app-enrollee-property>

        </ng-container>
      </ng-template>

      <button mat-flat-button
          color="primary"
          (click)="onRoute(PaperEnrolmentRoutes.REGULATORY)">Edit Other College Licences
      </button>
    </app-page-section>
  `,
  styles: ['mat-icon { font-size: 1em; }'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class RegulatoryOverviewComponent extends AbstractOverview {
  @Input() public regulatory: RegulatoryForm;
  public PaperEnrolmentRoutes = PaperEnrolmentRoutes;

  public get deviceProviderUseRoleCode(): string {
    return this.regulatory?.enrolleeDeviceProviders ?
      this.regulatory?.enrolleeDeviceProviders[0].deviceProviderRoleCode : "";
  }

  public get deviceProviderId(): string {
    return this.regulatory?.enrolleeDeviceProviders ?
      this.regulatory?.enrolleeDeviceProviders[0].deviceProviderId : "";
  }

  public get certificationNumber(): string {
    return this.regulatory?.enrolleeDeviceProviders ?
      this.regulatory?.enrolleeDeviceProviders[0].certificationNumber : "";
  }

  constructor(
    route: ActivatedRoute,
    router: Router
  ) {
    super(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }
}
