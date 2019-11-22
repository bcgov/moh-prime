import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable, Subscription } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { EnrolmentStateService } from '@enrolment/shared/services/enrolment-state.service';
import { FormUtilsService } from '@enrolment/shared/services/form-utils.service';

@Component({
  selector: 'app-self-declaration',
  templateUrl: './self-declaration.component.html',
  styleUrls: ['./self-declaration.component.scss']
})
export class SelfDeclarationComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public decisions: { code: boolean, name: string }[] = [
    { code: false, name: 'No' }, { code: true, name: 'Yes' }
  ];
  public EnrolmentRoutes = EnrolmentRoutes;
  public error = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private enrolmentService: EnrolmentService,
    private enrolmentResource: EnrolmentResource,
    private enrolmentStateService: EnrolmentStateService,
    private formUtilsService: FormUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public get hasConviction(): FormControl {
    return this.form.get('hasConviction') as FormControl;
  }

  public get hasConvictionDetails(): FormControl {
    return this.form.get('hasConvictionDetails') as FormControl;
  }

  public get hasRegistrationSuspended(): FormControl {
    return this.form.get('hasRegistrationSuspended') as FormControl;
  }

  public get hasRegistrationSuspendedDetails(): FormControl {
    return this.form.get('hasRegistrationSuspendedDetails') as FormControl;
  }

  public get hasDisciplinaryAction(): FormControl {
    return this.form.get('hasDisciplinaryAction') as FormControl;
  }

  public get hasDisciplinaryActionDetails(): FormControl {
    return this.form.get('hasDisciplinaryActionDetails') as FormControl;
  }

  public get hasPharmaNetSuspended(): FormControl {
    return this.form.get('hasPharmaNetSuspended') as FormControl;
  }

  public get hasPharmaNetSuspendedDetails(): FormControl {
    return this.form.get('hasPharmaNetSuspendedDetails') as FormControl;
  }

  public onSubmit() {
    if (this.form.valid) {
      this.error = false;
      const payload = this.enrolmentStateService.enrolment;
      this.busy = this.enrolmentResource.updateEnrolment(payload)
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Self declaration has been saved');
            this.form.markAsPristine();
            this.router.navigate([EnrolmentRoutes.ORGANIZATION], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openErrorToast('Self declaration could not be saved');
            this.logger.error('[Enrolment] SelfDeclaration::onSubmit error has occurred: ', error);
          });
    } else {
      this.error = true;
      this.form.markAllAsTouched();
    }
  }

  public onBack() {
    const currentEnrolment = this.enrolmentStateService.enrolment;
    if (currentEnrolment.certifications.length === 0) {
      this.router.navigate([EnrolmentRoutes.JOB], { relativeTo: this.route.parent });
    } else {
      this.router.navigate([EnrolmentRoutes.DEVICE_PROVIDER], { relativeTo: this.route.parent });
    }
  }

  public isRequired(path: string) {
    this.formUtilsService.isRequired(this.form, path);
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.enrolmentStateService.enrolment = this.enrolmentService.enrolment;
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.enrolmentStateService.selfDeclarationForm;
  }

  private initForm() {
    // TODO make YES/NO into own component to encapsulate toggling and markup
    this.hasConviction.valueChanges.subscribe((value: boolean) => this.toggleValidators(value, this.hasConvictionDetails));
    this.hasRegistrationSuspended.valueChanges.subscribe((value: boolean) => this.toggleValidators(value, this.hasRegistrationSuspendedDetails));
    this.hasDisciplinaryAction.valueChanges.subscribe((value: boolean) => this.toggleValidators(value, this.hasDisciplinaryActionDetails));
    this.hasPharmaNetSuspended.valueChanges.subscribe((value: boolean) => this.toggleValidators(value, this.hasPharmaNetSuspendedDetails));
  }

  private toggleValidators(value: boolean, control: FormControl) {
    if (!value) {
      this.formUtilsService.resetAndClearValidators(control);
    } else {
      this.formUtilsService.setValidators(control, [Validators.required]);
    }
  }
}
