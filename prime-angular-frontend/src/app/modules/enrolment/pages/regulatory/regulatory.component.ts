import { Component, OnInit, ViewChild, ElementRef, OnDestroy } from '@angular/core';
import { FormGroup, FormArray, FormControl, AbstractControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentStateService } from '../../shared/services/enrolment-state.service';
import { EnrolmentResource } from '../../shared/services/enrolment-resource.service';

@Component({
  selector: 'app-regulatory',
  templateUrl: './regulatory.component.html',
  styleUrls: ['./regulatory.component.scss']
})
export class RegulatoryComponent implements OnInit, OnDestroy {

  public form: FormGroup;
  public jobForm: FormGroup;
  public colleges: Config<number>[];
  public licenses: Config<number>[];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private configService: ConfigService,
    private enrolmentStateService: EnrolmentStateService,
    private enrolmentResource: EnrolmentResource,
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
      this.clearEmptyCertifications();
      this.clearJobForm();
      const payload = this.enrolmentStateService.enrolment;
      this.enrolmentResource.updateEnrolment(payload)
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Regulatory information has been saved');
            this.form.markAsPristine();
            this.certifications.clear();
            this.router.navigate(['device-provider'], { relativeTo: this.route.parent });
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
    const data = 'unsaved';
    this.clearEmptyCertifications();
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
    // Initialize form changes before patching
    this.initForm();

    // TODO: detect enrolment already exists and don't reload
    // TODO: apply guard if not enrolment is found to redirect to profile
    this.enrolmentResource.enrolments()
      .subscribe((enrolment: Enrolment) => {
        if (enrolment) {
          this.enrolmentStateService.enrolment = enrolment;
        }
      });
  }

  public ngOnDestroy() {
    this.clearEmptyCertifications();
  }

  public clearEmptyCertifications() {
    let i = 0;
    this.certifications.controls.forEach((control: AbstractControl) => {
      if (control instanceof FormGroup) {
        if (control.get('collegeNumber') || control.get('licenseNumber').value === null) {
          this.removeCertification(i);
        }
      }
      i++;
    });
  }

  public clearJobForm() {
    if (this.enrolmentStateService.enrolment.jobs.length > 0) {
      this.jobForm = this.enrolmentStateService.jobsForm;
      const jobs = this.jobForm.get('jobs') as FormArray;
      jobs.clear();
    }
  }

  public canAddCertification(): boolean {
    return this.certifications.controls
      .every((control: FormGroup) => control.get('collegeCode').value);
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

}
