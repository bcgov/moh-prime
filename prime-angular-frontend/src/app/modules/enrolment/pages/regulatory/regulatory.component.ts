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
import { map } from 'rxjs/operators';

@Component({
  selector: 'app-regulatory',
  templateUrl: './regulatory.component.html',
  styleUrls: ['./regulatory.component.scss']
})
export class RegulatoryComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
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
      // TODO remove from payload instead so view doesn't jump
      this.removeIncompleteCertifications();

      const payload = this.enrolmentStateService.enrolment;
      this.busy = this.enrolmentResource.updateEnrolment(payload)
        .subscribe(
          () => {
            this.form.markAsPristine();
            this.toastService.openSuccessToast('Regulatory information has been saved');
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

  /**
   * @description
   * Removes a certification from the list in response to an
   * emitted event from college certifications. Does not allow
   * the list of certifications to empty.
   *
   * @param index to be removed
   */
  public removeCertification(index: number) {
    this.certifications.removeAt(index);
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
        .pipe(
          map((result: boolean) => {
            if (!result) {
              // Only remove incomplete certifications when the
              // enrollee decides to discard their changes
              this.removeIncompleteCertifications();
            }

            return result;
          })
        )
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
    this.enrolmentStateService.enrolment = this.enrolmentService.enrolment;
  }

  private createFormInstance() {
    this.form = this.enrolmentStateService.regulatoryForm;
  }

  private initForm() {
    // Always have at least one certification ready for
    // the enrollee to fill out
    this.addCertification();
  }

  /**
   * @description
   * Removes incomplete certifications from the list in preparation
   * for submission, and allows for an empty list of certifications.
   */
  private removeIncompleteCertifications() {
    this.certifications.controls
      .forEach((control: FormGroup, index: number) => {
        if (!control.get('collegeCode').value) {
          this.removeCertification(index);
        }
      });
  }

  /**
   * @description
   * Remove jobs from the enrolment as enrollees can not have
   * certificate(s), as well as, job(s).
   */
  private removeJobs() {
    const jobs = this.enrolmentStateService.jobsForm.get('jobs') as FormArray;
    jobs.clear();
  }
}
