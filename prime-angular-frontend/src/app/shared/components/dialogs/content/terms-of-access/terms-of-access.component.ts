import { Component, Inject, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { AgreementType, termsOfAccessAgreements } from '@shared/enums/agreement-type.enum';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '../../dialog-options.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

@Component({
  selector: 'app-terms-of-access',
  templateUrl: './terms-of-access.component.html',
  styleUrls: ['./terms-of-access.component.scss']
})
export class ChangeTermsOfAccessComponent implements OnInit {
  public enrollee: HttpEnrollee;

  public form: UntypedFormGroup;

  public termsOfAccessAgreements: { type: AgreementType, name: string }[];

  constructor(
    private fb: UntypedFormBuilder,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
    protected adjudicationResource: AdjudicationResource,
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
  ) {
    this.enrollee = data.data.enrollee;
    this.termsOfAccessAgreements = termsOfAccessAgreements.filter((a) => a.type !== 0 && a.type !== this.enrollee?.assignedTOAType);
  }

  public get note(): UntypedFormControl {
    return this.form.get('note') as UntypedFormControl;
  }

  public get assignedTOAType(): UntypedFormControl {
    return this.form.get('assignedTOAType') as UntypedFormControl;
  }

  ngOnInit(): void {
    this.createFormInstance();
  }

  private createFormInstance() {
    this.form = this.fb.group({
      note: [null, [Validators.required]],
      assignedTOAType: [null, [Validators.required]]
    });
  }

  public onCancel(): void {
    this.dialogRef.close();
  }

  public onSubmit(): void {
    if (this.form.valid) {
      this.adjudicationResource.changeAgreementType(this.enrollee.id, this.note.value, this.assignedTOAType.value)
        .subscribe(() => this.dialogRef.close({ reload: true }));
    } else {
      this.form.markAllAsTouched();
    }
  }
}
