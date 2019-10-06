import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormBuilder, FormArray, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Observable } from 'rxjs';

import { ConfirmDiscardChangesDialogComponent } from '@shared/components/dialogs/confirm-discard-changes-dialog/confirm-discard-changes-dialog.component';
import { ConfigKeyValue } from '@config/config.model';
import { ConfigService } from '@config/config.service';

@Component({
  selector: 'app-pharmanet-access',
  templateUrl: './pharmanet-access.component.html',
  styleUrls: ['./pharmanet-access.component.scss']
})
export class PharmanetAccessComponent implements OnInit, OnDestroy {
  public form: FormGroup;
  public organizationNames: ConfigKeyValue[];

  constructor(
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private configService: ConfigService
  ) {
    this.organizationNames = this.configService.organizationNames;
  }

  public get organizations(): FormArray {
    return this.form.get('organizations') as FormArray;
  }

  public onSubmit() {
    if (this.form.valid) {
      this.form.markAsPristine();
      this.router.navigate(['review'], { relativeTo: this.route.parent });
    } else {
      this.form.markAllAsTouched();
    }
  }

  public addOrganization() {
    const organization = this.fb.group({
      id: [null, []],
      organizationTypeCode: [null, [Validators.required]],
      name: [null, [Validators.required]],
      city: [null, [Validators.required]],
      startDate: [null, [Validators.required]],
      endDate: [null, []]
    });

    this.organizations.push(organization);
  }

  public removeOrganization(index: number) {
    this.organizations.removeAt(index);
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
      organizations: this.fb.array([])
    });
  }
}
