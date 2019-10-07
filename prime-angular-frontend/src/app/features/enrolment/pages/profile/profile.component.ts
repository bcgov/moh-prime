import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable } from 'rxjs';

import { ConfigService } from '@config/config.service';
import { ViewportService } from '@core/services/viewport.service';
import { ConfigKeyValue } from '@config/config.model';
import { ConfirmDiscardChangesDialogComponent } from '@shared/components/dialogs/confirm-discard-changes-dialog/confirm-discard-changes-dialog.component';
import { EnrolmentStateService } from '../../shared/services/enrolment-state.service';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit, OnDestroy {
  public form: FormGroup;
  public hasPreferredName: boolean;
  public hasMailingAddress: boolean;
  public provinces: ConfigKeyValue[];
  public subheadings: { [key: string]: { subheader: string, help: string } };

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private viewportService: ViewportService,
    private configService: ConfigService,
    private enrolmentStateService: EnrolmentStateService
  ) {
    this.provinces = this.configService.provinces;
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
      this.form.markAsPristine();
      this.router.navigate(['contact'], { relativeTo: this.route.parent });
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

    const enrolment = this.enrolmentStateService.getRawEnrolment();
    this.form.patchValue(enrolment.enrollee);

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

  public ngOnDestroy() {

  }

  private createFormInstance() {
    this.form = this.enrolmentStateService.profileForm;
  }
}
