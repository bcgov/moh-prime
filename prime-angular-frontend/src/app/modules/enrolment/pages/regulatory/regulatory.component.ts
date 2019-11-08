import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormArray, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable } from 'rxjs';

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
export class RegulatoryComponent implements OnInit {

  public form: FormGroup;
  public jobCtrl: FormControl;
  @ViewChild('jobInput', { static: false }) jobInput: ElementRef<HTMLInputElement>;
  public decisions: { code: boolean, name: string }[] = [
    { code: false, name: 'No' }, { code: true, name: 'Yes' }
  ];
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
      console.log('Form is valid', this.form);
      const payload = this.enrolmentStateService.enrolment;
      console.log(payload);
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
      console.log('not valid', this.form);
      this.form.markAllAsTouched();
    }


    // FOR TESTING
    this.router.navigate(['device-provider'], { relativeTo: this.route.parent });
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
