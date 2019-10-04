import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable } from 'rxjs';

import { ConfigKeyValue } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { FormControlValidators } from '@shared/validators/form-control.validators';
import { ConfirmDiscardChangesDialogComponent } from '@shared/components/dialogs/confirm-discard-changes-dialog/confirm-discard-changes-dialog.component';

@Component({
  selector: 'app-professional-info',
  templateUrl: './professional-info.component.html',
  styleUrls: ['./professional-info.component.scss']
})
export class ProfessionalInfoComponent implements OnInit, OnDestroy {
  public form: FormGroup;
  public decisions: { code: boolean, name: string }[] = [
    { code: false, name: 'No' }, { code: true, name: 'Yes' }
  ];
  public colleges: ConfigKeyValue[];
  public licenses: ConfigKeyValue[];
  public defaultJobs: ConfigKeyValue[];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private configService: ConfigService
  ) {
    this.colleges = this.configService.colleges;
    this.licenses = this.configService.licenses;
    this.defaultJobs = this.configService.jobs;
  }

  public get hasCertification(): FormGroup {
    return this.form.get('hasCertification') as FormGroup;
  }

  public get isDeviceProvider(): FormGroup {
    return this.form.get('isDeviceProvider') as FormGroup;
  }

  public get isInsulinPumpProvider(): FormGroup {
    return this.form.get('isInsulinPumpProvider') as FormGroup;
  }

  public get isAccessingPharmaNetOnBehalfOf(): FormGroup {
    return this.form.get('isAccessingPharmaNetOnBehalfOf') as FormGroup;
  }

  public get jobs(): FormGroup {
    return this.form.get('jobs') as FormGroup;
  }

  public onSubmit() {
    if (this.form.valid) {
      this.form.markAsPristine();
      this.router.navigate(['declaration'], { relativeTo: this.route.parent });
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
      hasCertification: [null, [FormControlValidators.requiredBoolean]],
      certifications: this.fb.array([]),
      isDeviceProvider: [null, [FormControlValidators.requiredBoolean]],
      deviceProviderNumber: ['', []],
      isInsulinPumpProvider: [null, [FormControlValidators.requiredBoolean]],
      isAccessingPharmaNetOnBehalfOf: [null, [FormControlValidators.requiredBoolean]],
      jobs: [[], []],
    });
  }

  private addCertificationFormGroup() {
    // this.fb.group({
    //   collegeCode: ['', []],
    //   licenseNumber: ['', []],
    //   licenseCode: ['', []],
    //   renewalDate: ['', []],
    //   practiceCode: ['', []]
    // });
  }
}
