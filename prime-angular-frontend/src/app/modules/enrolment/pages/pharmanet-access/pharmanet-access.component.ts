import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable, Subscription } from 'rxjs';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { ViewportService } from '@core/services/viewport.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { EnrolmentRoutes } from '@enrolment/enrolent.routes';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { Organization } from '@enrolment/shared/models/organization.model';

@Component({
  selector: 'app-pharmanet-access',
  templateUrl: './pharmanet-access.component.html',
  styleUrls: ['./pharmanet-access.component.scss']
})
export class PharmanetAccessComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public organizationCtrl: FormControl;
  public organizationTypes: Config<number>[];
  public filteredOrganizationTypes: Config<number>[];
  public EnrolmentRoutes = EnrolmentRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private configService: ConfigService,
    private viewportService: ViewportService,
    private enrolmentStateService: EnrolmentStateService,
    private enrolmentResource: EnrolmentResource,
    private enrolmentService: EnrolmentService,
    private toastService: ToastService,
    private logger: LoggerService
  ) {
    this.organizationTypes = this.configService.organizationTypes;
  }

  public get organizations(): FormArray {
    return this.form.get('organizations') as FormArray;
  }

  public get isMobile() {
    return this.viewportService.isMobile;
  }

  public onSubmit() {
    if (this.form.valid) {
      // TODO create rxjs pipe for updating enrolment submissions
      const payload = this.enrolmentStateService.enrolment;
      this.busy = this.enrolmentResource.updateEnrolment(payload)
        .subscribe(
          () => {
            this.toastService.openSuccessToast('PharmaNet access has been saved');
            this.form.markAsPristine();
            this.router.navigate([EnrolmentRoutes.REVIEW], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openErrorToast('PharmaNet access could not be saved');
            this.logger.error('[Enrolment] PharmanetAccess::onSubmit error has occurred: ', error);
          });
    } else {
      this.form.markAllAsTouched();
    }
  }

  public addOrganization() {
    const code = this.organizationCtrl.value.code;
    const organization = this.enrolmentStateService.buildOrganizationForm(code);

    this.organizations.push(organization);
    this.organizationCtrl.reset();
    this.filterOrganizationTypes();
  }

  public removeOrganization(index: number) {
    this.organizations.removeAt(index);
    this.filterOrganizationTypes();
  }

  public organizationLookup(organizationTypeCode: number): string {
    return this.organizationTypes
      .find((c: Config<number>) => c.code === organizationTypeCode)
      .name;
  }

  public displayOrganization(organizationType?: Config<number>): string {
    return (organizationType) ? organizationType.name : null;
  }

  public disableOrganization(organizationTypeCode: number) {
    // Omit organizations types that are not "Community Practices" for ComPap
    return (organizationTypeCode !== 2);
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
    // TODO no enrolment resource, but still not the best solution
    // 1) set the enrolment state service in the guard
    // 2) pass the BehaviourSubject into the enrolment state service
    this.enrolmentStateService.enrolment = this.enrolmentService.enrolment;
    this.filterOrganizationTypes();
  }

  private createFormInstance() {
    this.form = this.enrolmentStateService.pharmaNetAccessForm;
    this.organizationCtrl = new FormControl();
  }

  private filterOrganizationTypes() {
    const selectedOrgTypeCodes: number[] = this.organizations.value
      .map((o: Organization) => o.organizationTypeCode);

    this.filteredOrganizationTypes = this.organizationTypes
      .filter((c: Config<number>) => !selectedOrgTypeCodes.includes(c.code));
  }
}
