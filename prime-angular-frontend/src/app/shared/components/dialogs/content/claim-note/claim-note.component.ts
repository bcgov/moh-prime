import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { BehaviorSubject } from 'rxjs';

import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';

import { Admin } from '@auth/shared/models/admin.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '@auth/shared/services/auth.service';

export class AssignAction {
  public action: AssignActionEnum;
  public adjudicatorId?: number;
  public note?: string;
}

export enum AssignActionEnum {
  Disclaim = 0,
  Assign = 1
}

export enum ClaimType {
  ENROLLEE = 'enrollee',
  SITE = 'site'
}

@Component({
  selector: 'app-claim-note',
  templateUrl: './claim-note.component.html',
  styleUrls: ['./claim-note.component.scss']
})
export class ClaimNoteComponent implements OnInit {
  public title: string;
  public type: ClaimType;
  public reassign: boolean;
  public form: FormGroup;
  public adjudicators$: BehaviorSubject<Admin[]>;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
    private adjudicationResource: AdjudicationResource,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
    private fb: FormBuilder,
    private authService: AuthService,
  ) {
    this.title = data.title;
    this.reassign = data.data.reassign;
    this.type = data.data.type;
    this.adjudicators$ = new BehaviorSubject<Admin[]>([]);
  }

  public get note(): FormControl {
    return this.form.get('note') as FormControl;
  }

  public onDisclaim(): void {
    const output = new AssignAction();
    output.action = AssignActionEnum.Disclaim;
    this.dialogRef.close({ output });
  }

  public onClaim(): void {
    this.authService.getAdmin$()
      .subscribe((admin: Admin) => {
        const output = new AssignAction();
        output.action = AssignActionEnum.Assign;
        output.adjudicatorId = this.adjudicators$.value.find(a => a.userId === admin.userId).id;
        output.note = this.note.value;
        this.dialogRef.close({ output });
      });
  }

  public onAssign(adminId: number): void {
    if (this.form.valid) {
      const output = new AssignAction();
      output.action = AssignActionEnum.Assign;
      output.adjudicatorId = adminId;
      output.note = this.note.value;
      this.dialogRef.close({ output });
    }

    this.note.markAsTouched();
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.getAdjudicators();
  }

  private createFormInstance(): void {
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

  private getAdjudicators(): void {
    this.adjudicationResource.getAdjudicators()
      .subscribe((adjudicators: Admin[]) => this.adjudicators$.next(adjudicators));
  }
}
