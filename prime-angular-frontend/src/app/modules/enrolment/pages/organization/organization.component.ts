import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable, Subscription } from 'rxjs';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { Organization } from '@enrolment/shared/models/organization.model';

@Component({
  selector: 'app-organization',
  templateUrl: './organization.component.html',
  styleUrls: ['./organization.component.scss']
})
export class OrganizationComponent implements OnInit {
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

  public onSubmit() {
    if (this.form.valid) {
      const payload = this.enrolmentStateService.enrolment;
      this.busy = this.enrolmentResource.updateEnrolment(payload)
        .subscribe(
          () => {
            this.form.markAsPristine();
            this.toastService.openSuccessToast('PharmaNet access has been saved');
            this.router.navigate([EnrolmentRoutes.REVIEW], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openErrorToast('PharmaNet access could not be saved');
            this.logger.error('[Enrolment] Organization::onSubmit error has occurred: ', error);
          });
    } else {
      this.form.markAllAsTouched();
    }
  }

  public addOrganization() {
    const organization = this.enrolmentStateService.buildOrganizationForm();
    this.organizations.push(organization);
  }

  public disableOrganization(organizationTypeCode: number): boolean {
    // Omit organizations types that are not "Community Practices" for ComPap
    return (organizationTypeCode !== 1);
  }

  public removeOrganization(index: number) {
    this.organizations.removeAt(index);
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
    // Initialize form changes before patching
    this.initForm();
    this.enrolmentStateService.enrolment = this.enrolmentService.enrolment;
  }

  private createFormInstance() {
    this.form = this.enrolmentStateService.organizationForm;
  }

  private initForm() {
    if (this.organizations.length === 0) {
      // Always have at least one organization ready for
      // the enrollee to fill out
      this.addOrganization();
    } else {
      // After patched with existing data mark the form
      // group as pristine to avoid dirty check on route
      this.form.markAsPristine();
    }
  }

  private filterOrganizationTypes() {
    const selectedOrgTypeCodes: number[] = this.organizations.value
      .map((o: Organization) => o.organizationTypeCode);

    this.filteredOrganizationTypes = this.organizationTypes
      .filter((c: Config<number>) => !selectedOrgTypeCodes.includes(c.code));
  }
}
