import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable, Subscription } from 'rxjs';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';

@Component({
  selector: 'app-regulatory',
  templateUrl: './regulatory.component.html',
  styleUrls: ['./regulatory.component.scss']
})
export class RegulatoryComponent implements OnInit, OnDestroy {
  public busy: Subscription;
  public form: FormGroup;
  public jobForm: FormGroup;
  public colleges: Config<number>[];
  public licenses: Config<number>[];
  public EnrolmentRoutes = EnrolmentRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private configService: ConfigService,
    private enrolmentService: EnrolmentService,
    private enrolmentResource: EnrolmentResource,
    private enrolmentStateService: EnrolmentStateService,
    private toastService: ToastService,
    private logger: LoggerService
  ) {
    this.colleges = this.configService.colleges;
    this.licenses = this.configService.licenses;
  }

  public get certifications(): FormArray {
    return this.form.get('certifications') as FormArray;
  }

  public onSubmit() {
    if (this.form.valid) {
      this.removeJobs();
      this.removeEmptyCertifications();
      const payload = this.enrolmentStateService.enrolment;
      this.busy = this.enrolmentResource.updateEnrolment(payload)
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Regulatory information has been saved');
            this.form.markAsPristine();
            this.certifications.clear();
            this.router.navigate([EnrolmentRoutes.DEVICE_PROVIDER], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openErrorToast('Regulatory information could not be saved');
            this.logger.error('[Enrolment] Regulatory::onSubmit error has occurred: ', error);
          }
        );
      this.form.markAsPristine();
    } else {
      this.form.markAllAsTouched();
    }
  }

  public addCertification() {
    const certification = this.enrolmentStateService.buildCollegeCertificationForm();
    this.certifications.push(certification);
  }

  public removeCertification(index: number) {
    if (index >= 0) {
      this.certifications.removeAt(index);
    }
  }

  public canDeactivate(): Observable<boolean> | boolean {
    this.removeEmptyCertifications();

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

  public ngOnDestroy() {
    this.removeEmptyCertifications();
  }

  private createFormInstance() {
    this.form = this.enrolmentStateService.regulatoryForm;
  }

  private initForm() {
    if (this.certifications.length === 0) {
      this.addCertification();
    } else {
      this.form.markAsPristine();
    }
  }

  private removeEmptyCertifications() {
    this.certifications.controls
      .forEach((control: FormGroup, index: number) => {
        console.log(control.get('collegeNumber'));

        if (control.get('collegeNumber') || control.get('licenseNumber').value === null) {
          this.removeCertification(index);
        }
      });
  }

  private removeJobs() {
    if (this.enrolmentStateService.enrolment.jobs.length > 0) {
      this.jobForm = this.enrolmentStateService.jobsForm;
      const jobs = this.jobForm.get('jobs') as FormArray;
      jobs.clear();
    }
  }
}
