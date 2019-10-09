import { Component, OnInit } from '@angular/core';
import { FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { ConfirmDiscardChangesDialogComponent } from '@shared/components/dialogs/confirm-discard-changes-dialog/confirm-discard-changes-dialog.component';
import { Enrolment } from '../../shared/models/enrolment.model';
import { EnrolmentStateService } from '../../shared/services/enrolment-state.service';
import { EnrolmentResource } from '../../shared/services/enrolment-resource.service';

// TODO: make YesNo into a component and use projection for content
@Component({
  selector: 'app-self-declaration',
  templateUrl: './self-declaration.component.html',
  styleUrls: ['./self-declaration.component.scss']
})
export class SelfDeclarationComponent implements OnInit {
  public form: FormGroup;
  public decisions: { code: boolean, name: string }[] = [
    { code: false, name: 'No' }, { code: true, name: 'Yes' }
  ];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private enrolmentStateService: EnrolmentStateService,
    private enrolmentResource: EnrolmentResource,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public get hasConviction(): FormGroup {
    return this.form.get('hasConviction') as FormGroup;
  }

  public get hasConvictionDetails(): FormGroup {
    return this.form.get('hasConvictionDetails') as FormGroup;
  }

  public get hasRegistrationSuspended(): FormGroup {
    return this.form.get('hasRegistrationSuspended') as FormGroup;
  }

  public get hasRegistrationSuspendedDetails(): FormGroup {
    return this.form.get('hasRegistrationSuspendedDetails') as FormGroup;
  }

  public get hasDisciplinaryAction(): FormGroup {
    return this.form.get('hasDisciplinaryAction') as FormGroup;
  }

  public get hasDisciplinaryActionDetails(): FormGroup {
    return this.form.get('hasDisciplinaryActionDetails') as FormGroup;
  }

  public get hasPharmaNetSuspended(): FormGroup {
    return this.form.get('hasPharmaNetSuspended') as FormGroup;
  }

  public get hasPharmaNetSuspendedDetails(): FormGroup {
    return this.form.get('hasPharmaNetSuspendedDetails') as FormGroup;
  }

  public onSubmit() {
    if (this.form.valid) {
      const payload = this.enrolmentStateService.enrolment;
      this.enrolmentResource.updateEnrolment(payload)
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Self declaration has been saved');
            this.form.markAsPristine();
            this.router.navigate(['access'], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openSuccessToast('Self declaration could not be saved');
            this.logger.error('[Enrolment] SelfDeclaration::onSubmit error has occurred: ', error);
          });
    } else {
      this.form.markAllAsTouched();
    }
  }

  public canDeactivate(): Observable<boolean> | boolean {
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDiscardChangesDialogComponent).afterClosed()
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
    this.form = this.enrolmentStateService.selfDeclarationForm;
  }

  private initForm() {
    // TODO: make YES/NO into own component to encapsulate toggling
    this.hasConviction.valueChanges.subscribe((value) => {
      this.toggleValidators(value, 'convictionDetails');
    });
    this.hasRegistrationSuspended.valueChanges.subscribe((value) => {
      this.toggleValidators(value, 'registrationSuspendedDetails');
    });
    this.hasDisciplinaryAction.valueChanges.subscribe((value) => {
      this.toggleValidators(value, 'disciplinaryActionDetails');
    });
    this.hasPharmaNetSuspended.valueChanges.subscribe((value) => {
      this.toggleValidators(value, 'pharmaNetSuspendedDetails');
    });
  }

  private toggleValidators(value: boolean, controlName: string) {
    if (!value) {
      this.form.get(controlName).clearValidators();
      this.form.get(controlName).updateValueAndValidity();
      this.form.get(controlName).reset();
    } else {
      this.form.get(controlName).setValidators([Validators.required]);
      this.form.get(controlName).updateValueAndValidity();
    }
  }
}
