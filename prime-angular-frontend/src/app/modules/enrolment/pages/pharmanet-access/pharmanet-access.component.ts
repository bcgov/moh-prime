import { Component, OnInit } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable } from 'rxjs';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { ViewportService } from '@core/services/viewport.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentStateService } from '../../shared/services/enrolment-state.service';
import { EnrolmentResource } from '../../shared/services/enrolment-resource.service';

@Component({
  selector: 'app-pharmanet-access',
  templateUrl: './pharmanet-access.component.html',
  styleUrls: ['./pharmanet-access.component.scss']
})
export class PharmanetAccessComponent implements OnInit {
  public form: FormGroup;
  public organizationNames: Config[];
  public organizationTypes: Config[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private configService: ConfigService,
    private viewportService: ViewportService,
    private enrolmentStateService: EnrolmentStateService,
    private enrolmentResource: EnrolmentResource,
    private toastService: ToastService,
    private logger: LoggerService
  ) {
    this.organizationNames = this.configService.organizationNames;
    // TODO: not used until requirements flushed out
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
      const payload = this.enrolmentStateService.enrolment;
      this.enrolmentResource.updateEnrolment(payload)
        .subscribe(
          () => {
            this.toastService.openSuccessToast('PharmaNet access has been saved');
            this.form.markAsPristine();
            this.router.navigate(['review'], { relativeTo: this.route.parent });
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
    const organization = this.enrolmentStateService.buildOrganizationForm();

    this.organizations.push(organization);
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

    // TODO: detect enrolment already exists and don't reload
    // TODO: apply guard if not enrolment is found to redirect to profile
    this.enrolmentResource.enrolments()
      .subscribe((enrolment: Enrolment) => {
        if (enrolment) {
          this.enrolmentStateService.enrolment = enrolment;
        }

        this.initForm();
      });
  }

  private createFormInstance() {
    this.form = this.enrolmentStateService.pharmaNetAccessForm;
  }

  private initForm() {

  }
}
