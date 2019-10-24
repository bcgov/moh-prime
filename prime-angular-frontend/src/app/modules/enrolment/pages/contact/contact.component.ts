import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { ConfirmDiscardChangesDialogComponent } from '@shared/components/dialogs/confirm-discard-changes-dialog/confirm-discard-changes-dialog.component';
import { Enrolment } from '../../shared/models/enrolment.model';
import { EnrolmentStateService } from '../../shared/services/enrolment-state.service';
import { EnrolmentResource } from '../../shared/services/enrolment-resource.service';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent implements OnInit {
  public form: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private enrolmentStateService: EnrolmentStateService,
    private enrolmentResource: EnrolmentResource,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public get voicePhone(): FormGroup {
    return this.form.get('voicePhone') as FormGroup;
  }

  public get voiceExtension(): FormGroup {
    return this.form.get('voiceExtension') as FormGroup;
  }

  public get hasContactEmail(): FormGroup {
    return this.form.get('hasContactEmail') as FormGroup;
  }

  public get contactEmail(): FormGroup {
    return this.form.get('contactEmail') as FormGroup;
  }

  public get hasContactPhone(): FormGroup {
    return this.form.get('hasContactPhone') as FormGroup;
  }

  public get contactPhone(): FormGroup {
    return this.form.get('contactPhone') as FormGroup;
  }

  public onSubmit() {
    if (this.form.valid) {
      const payload = this.enrolmentStateService.enrolment;
      this.enrolmentResource.updateEnrolment(payload)
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Contact information has been saved');
            this.form.markAsPristine();
            this.router.navigate(['professional'], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openErrorToast('Contact information could not be saved');
            this.logger.error('[Enrolment] Contact::onSubmit error has occurred: ', error);
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
    this.form = this.enrolmentStateService.contactForm;
  }

  private initForm() {
    // TODO: update to eliminate controls in form
    if (this.contactEmail.value) {
      this.form.get('hasContactEmail').patchValue(true);
    }

    if (this.contactPhone.value) {
      this.form.get('hasContactPhone').patchValue(true);
    }

    this.form.markAsPristine();

    this.hasContactEmail.valueChanges.subscribe((value: boolean) => {
      if (!value) {
        this.contactEmail.reset();
      }
    });
    this.hasContactPhone.valueChanges.subscribe((value: boolean) => {
      if (!value) {
        this.contactPhone.reset();
      }
    });
  }
}
