import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import * as moment from 'moment';

import { ConfigService } from '@config/config.service';
import { ViewportService } from '@core/services/viewport.service';
import { ConfigKeyValue } from '@config/config.model';

import { ConfirmDiscardChangesDialogComponent } from '@shared/components/dialogs/confirm-discard-changes-dialog/confirm-discard-changes-dialog.component';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit, OnDestroy {
  public form: FormGroup;
  public hasSameMailingAddress: boolean;
  public hasPreferredName: boolean;
  public provinces: ConfigKeyValue[];
  public subheadings: { [key: string]: { subheader: string, help: string } };

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private viewportService: ViewportService,
    private configService: ConfigService
  ) {
    this.hasSameMailingAddress = true;
    this.hasPreferredName = false;
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
  }

  public ngOnDestroy() {

  }

  private createFormInstance() {
    this.form = this.fb.group({
      firstName: [{ value: '', disabled: true }, [Validators.required]],
      middleName: [{ value: '', disabled: true }, []],
      lastName: [{ value: '', disabled: true }, [Validators.required]],
      dateOfBirth: [{ value: moment(), disabled: true }, []],
      preferredFirstName: ['', []],
      preferredMiddleName: ['', []],
      preferredLastName: ['', []],
      physicalAddress: this.fb.group({
        country: [{ value: '', disabled: true }, []],
        province: [{ value: '', disabled: true }, []],
        street: [{ value: '', disabled: true }, []],
        city: [{ value: '', disabled: true }, []],
        postal: [{ value: '', disabled: true }, []]
      }),
      mailingAddress: this.fb.group({
        country: ['', []],
        province: ['', []],
        street: ['', []],
        city: ['', []],
        postal: ['', []]
      })
    });
  }
}
