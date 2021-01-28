import { Component, Inject, Input, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { BehaviorSubject } from 'rxjs';

import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { Admin } from '@auth/shared/models/admin.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '@auth/shared/services/auth.service';
import { DialogOptions } from '../../dialog-options.model';

export enum AssignActionEnum {
  Disclaim = 0,
  Assign = 1
}

export class AssignEnrolleeAction {
  public action: AssignActionEnum;
  public adjudicatorId?: number;
  public note?: string;
}

@Component({
  selector: 'app-claim-enrollee',
  templateUrl: './claim-enrollee.component.html',
  styleUrls: ['./claim-enrollee.component.scss']
})
export class ClaimEnrolleeComponent implements OnInit {
  public adjudicators$: BehaviorSubject<Admin[]>;
  public form: FormGroup;
  public reassign: boolean;
  public title: string;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
    private adjudicationResource: AdjudicationResource,
    private dialogRef: MatDialogRef<ClaimEnrolleeComponent>,
    private fb: FormBuilder,
    private authService: AuthService,

  ) {
    this.adjudicators$ = new BehaviorSubject<Admin[]>([]);
    this.reassign = data.data.reassign;
    this.title = data.title;
  }

  public get note(): FormControl {
    return this.form.get('note') as FormControl;
  }

  public onDisclaim(): void {
    const output = new AssignEnrolleeAction();
    output.action = AssignActionEnum.Disclaim;
    this.dialogRef.close({ output });
  }

  public onClaim(): void {
    this.authService.getAdmin$().subscribe((admin: Admin) => {
      const output = new AssignEnrolleeAction();
      output.action = AssignActionEnum.Assign;
      output.adjudicatorId = admin.id;
      this.dialogRef.close({ output });
    })
  }

  public onAssign(adminId: number): void {
    if (this.form.valid) {
      const output = new AssignEnrolleeAction();
      output.action = AssignActionEnum.Assign;
      output.adjudicatorId = adminId;
      output.note = this.note.value;
      this.dialogRef.close({ output });
    }
    this.note.markAsTouched();
  }

  public ngOnInit() {
    this.getAdjudicators();
    this.createFormInstance();
  }

  private getAdjudicators() {
    this.adjudicationResource.getAdjudicators()
      .subscribe((adjudicators: Admin[]) => this.adjudicators$.next(adjudicators));
  }

  protected createFormInstance() {
    this.form = this.fb.group({
      note: [
        {
          value: '',
          disabled: false,
        },
        [Validators.required]
      ]
    });
  }
}
