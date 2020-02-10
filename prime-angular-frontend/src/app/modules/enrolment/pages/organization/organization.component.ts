import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormArray, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable } from 'rxjs';
import { tap } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { Organization } from '@enrolment/shared/models/organization.model';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/BaseEnrolmentProfilePage';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { UtilsService } from '@core/services/utils.service';

@Component({
  selector: 'app-organization',
  templateUrl: './organization.component.html',
  styleUrls: ['./organization.component.scss']
})
export class OrganizationComponent extends BaseEnrolmentProfilePage implements OnInit, OnDestroy {
  public organizationCtrl: FormControl;
  public organizationTypes: Config<number>[];
  public filteredOrganizationTypes: Config<number>[];

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentStateService: EnrolmentStateService,
    protected toastService: ToastService,
    protected logger: LoggerService,
    private configService: ConfigService
  ) {
    super(route, router, dialog, enrolmentService, enrolmentResource, enrolmentStateService, toastService, logger);

    this.organizationTypes = this.configService.organizationTypes;
  }

  public get organizations(): FormArray {
    return this.form.get('organizations') as FormArray;
  }

  public addOrganization() {
    const organization = this.enrolmentStateService.buildOrganizationForm();
    this.organizations.push(organization);
  }

  public disableOrganization(organizationTypeCode: number): boolean {
    // Omit organizations types that are not "Community Practices" for ComPap
    return (organizationTypeCode !== 2);
  }

  public removeOrganization(index: number) {
    this.organizations.removeAt(index);
  }

  public filterOrganizationTypes(organization: FormGroup) {
    // Create a list of filtered organization types
    if (this.organizations.length) {
      // All the currently chosen organizations
      const selectedOrganizationTypeCodes = this.organizations.value
        .map((o: Organization) => o.organizationTypeCode);
      // Current organization type selected
      const currentOrganization = this.organizationTypes
        .find(o => o.code === organization.get('organizationTypeCode').value);
      // Filter the list of possible organizations using the selected organizations
      const filteredOrganizationTypes = this.organizationTypes
        .filter((c: Config<number>) => !selectedOrganizationTypeCodes.includes(c.code));

      if (currentOrganization) {
        // Add the current organization to the list of filtered
        // organizations so it remains visible
        filteredOrganizationTypes.unshift(currentOrganization);
      }

      return filteredOrganizationTypes;
    }

    // Otherwise, provide the entire list of organization types
    return this.organizationTypes;
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const canDeactivate = super.canDeactivate();

    return (canDeactivate instanceof Observable)
      ? canDeactivate.pipe(tap(() => this.removeIncompleteOrganizations()))
      : canDeactivate;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  public ngOnDestroy() {
    this.removeIncompleteOrganizations();
  }

  protected createFormInstance() {
    this.form = this.enrolmentStateService.organizationForm;
  }

  protected initForm() {
    // Always have at least one organization ready for
    // the enrollee to fill out
    if (!this.organizations.length) {
      this.addOrganization();
    }
  }

  protected nextRouteAfterSubmit() {
    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = EnrolmentRoutes.SELF_DECLARATION;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }

  private removeIncompleteOrganizations() {
    this.organizations.controls
      .forEach((control: FormGroup, index: number) => {
        const value = control.get('organizationTypeCode').value;

        // Remove if organization is empty or the group is invalid
        if (!value || control.invalid) {
          this.removeOrganization(index);
        }
      });

    // Always have a single organization available, and it prevents
    // the page from jumping too much when routing
    if (!this.organizations.controls.length) {
      this.addOrganization();
    }
  }

  public routeBackTo() {
    const routePath = (this.enrolmentStateService.enrolment.certifications.length)
      ? EnrolmentRoutes.REGULATORY
      : EnrolmentRoutes.JOB;

    this.routeTo(routePath);
  }
}
