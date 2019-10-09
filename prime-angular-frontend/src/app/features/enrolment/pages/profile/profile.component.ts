import { Component, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { ConfigKeyValue } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { ViewportService } from '@core/services/viewport.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { ConfirmDiscardChangesDialogComponent } from '@shared/components/dialogs/confirm-discard-changes-dialog/confirm-discard-changes-dialog.component';
import { Enrolment } from '../../shared/models/enrolment.model';
import { EnrolmentStateService } from '../../shared/services/enrolment-state.service';
import { EnrolmentResource } from '../../shared/services/enrolment-resource.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  public form: FormGroup;
  public hasPreferredName: boolean;
  public hasMailingAddress: boolean;
  public provinces: ConfigKeyValue[];
  public subheadings: { [key: string]: { subheader: string, help: string } };

  private isNewEnrolment: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private viewportService: ViewportService,
    private configService: ConfigService,
    private enrolmentStateService: EnrolmentStateService,
    private enrolmentResource: EnrolmentResource,
    private toastService: ToastService,
    private logger: LoggerService
  ) {
    this.provinces = this.configService.provinces;
    this.isNewEnrolment = true;
  }

  public get firstName(): FormGroup {
    return this.form.get('firstName') as FormGroup;
  }

  public get lastName(): FormGroup {
    return this.form.get('lastName') as FormGroup;
  }

  public get dateOfBirth(): FormGroup {
    return this.form.get('dateOfBirth') as FormGroup;
  }

  public get isMobile() {
    return this.viewportService.isMobile;
  }

  public onSubmit() {
    if (this.form.valid) {
      const payload = this.enrolmentStateService.enrolment;
      const request$ = (this.isNewEnrolment)
        // TODO: temporarily use raw enrolment for creation
        ? this.enrolmentResource.createEnrolment(this.enrolmentStateService.getRawEnrolment())
          .pipe(map((enrolment: Enrolment) => this.enrolmentStateService.enrolment = enrolment))
        : this.enrolmentResource.updateEnrolment(payload);

      request$
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Profile information has been saved');
            this.form.markAsPristine();
            this.router.navigate(['contact'], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openSuccessToast('Profile information could not be saved');
            this.logger.error('[Enrolment] Profile::onSubmit error has occurred: ', error);
          }
        );
    } else {
      this.form.markAllAsTouched();
    }
  }

  public onPreferredNameChange() {
    this.hasPreferredName = !this.hasPreferredName;

    if (!this.hasPreferredName) {
      this.form.get('preferredFirstName').reset();
      this.form.get('preferredMiddleName').reset();
      this.form.get('preferredLastName').reset();
    }
  }

  public onMailingAddressChange() {
    this.hasMailingAddress = !this.hasMailingAddress;

    if (!this.hasMailingAddress) {
      const mailingAddress = this.form.get('mailingAddress');
      mailingAddress.get('country').reset();
      mailingAddress.get('province').reset();
      mailingAddress.get('street').reset();
      mailingAddress.get('city').reset();
      mailingAddress.get('postal').reset();
    }
  }

  public canDeactivate(): Observable<boolean> | boolean {
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDiscardChangesDialogComponent).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();

    this.enrolmentResource.enrolments()
      .subscribe((enrolment: Enrolment) => {
        if (enrolment) {
          this.isNewEnrolment = false;
          this.enrolmentStateService.enrolment = enrolment;
        }

        // TODO: temporary to test creation of an enrolment
        // this.enrolmentStateService.enrolment = this.enrolmentStateService.getRawEnrolment();

        this.initForm();
      });
  }

  private createFormInstance() {
    this.form = this.enrolmentStateService.profileForm;
  }

  private initForm() {
    // Show preferred name if it exists
    this.hasPreferredName = !!(
      this.form.get('preferredFirstName').value ||
      this.form.get('preferredMiddleName').value ||
      this.form.get('preferredLastName').value
    );

    const mailingAddress = this.form.get('mailingAddress');

    // Show mailing address if it exists
    this.hasMailingAddress = !!(
      mailingAddress.get('country').value ||
      mailingAddress.get('province').value ||
      mailingAddress.get('street').value ||
      mailingAddress.get('city').value ||
      mailingAddress.get('postal').value
    );
  }
}
