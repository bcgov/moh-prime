import { Component, OnInit, ViewChild, ElementRef } from '@angular/core';
import { FormGroup, FormArray, FormControl } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog, MatChipInputEvent, MatAutocompleteSelectedEvent } from '@angular/material';

import { Observable } from 'rxjs';
import { startWith, map } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '@shared/models/enrolment.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { Job } from '../../shared/models/job.model';
import { EnrolmentStateService } from '../../shared/services/enrolment-state.service';
import { EnrolmentResource } from '../../shared/services/enrolment-resource.service';

@Component({
  selector: 'app-device-provider',
  templateUrl: './device-provider.component.html',
  styleUrls: ['./device-provider.component.scss']
})
export class DeviceProviderComponent implements OnInit {

  public form: FormGroup;
  public decisions: { code: boolean, name: string }[] = [
    { code: false, name: 'No' }, { code: true, name: 'Yes' }
  ];

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
  }


  public get isDeviceProvider(): FormControl {
    return this.form.get('isDeviceProvider') as FormControl;
  }

  public get deviceProviderNumber(): FormControl {
    return this.form.get('deviceProviderNumber') as FormControl;
  }

  public get isInsulinPumpProvider(): FormControl {
    return this.form.get('isInsulinPumpProvider') as FormControl;
  }

  public get isAccessingPharmaNetOnBehalfOf(): FormControl {
    return this.form.get('isAccessingPharmaNetOnBehalfOf') as FormControl;
  }

  public onSubmit() {
    if (this.form.valid) {
      const payload = this.enrolmentStateService.enrolment;
      this.enrolmentResource.updateEnrolment(payload)
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Professional information has been saved');
            this.form.markAsPristine();
            this.router.navigate(['declaration'], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openErrorToast('Professional information could not be saved');
            this.logger.error('[Enrolment] Professional::onSubmit error has occurred: ', error);
          }
        );
      this.form.markAsPristine();
    } else {
      this.form.markAllAsTouched();
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
    this.form = this.enrolmentStateService.professionalInfoForm;
  }

  private initForm() {
    this.isDeviceProvider.valueChanges.subscribe((value) => {
      if (!value) {
        this.deviceProviderNumber.reset();

        // Device providers can be an insulin providers, otherwise disabled
        this.isInsulinPumpProvider.reset(null, { emitEvent: false });
        this.isInsulinPumpProvider.disable({ emitEvent: false });
      } else {
        this.isInsulinPumpProvider.enable({ emitEvent: false });
      }
    });
  }
}
