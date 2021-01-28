import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { BehaviorSubject } from 'rxjs';

import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';

import { Admin } from '@auth/shared/models/admin.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';

export enum AssignActionEnum {
  Disclaim = 0,
  Assign = 1
}

export class AssignSiteAction {
  public action: AssignActionEnum;
  public adjudicatorId?: number;
}

@Component({
  selector: 'app-claim-site',
  templateUrl: './claim-site.component.html',
  styleUrls: ['./claim-site.component.scss']
})
export class ClaimSiteComponent implements OnInit {
  public title: string;
  public adjudicators$: BehaviorSubject<Admin[]>;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
    private adjudicationResource: AdjudicationResource,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
  ) {
    this.title = data.title;
    this.adjudicators$ = new BehaviorSubject<Admin[]>([]);
  }

  public onDisclaim(): void {
    const output = new AssignSiteAction();
    output.action = AssignActionEnum.Disclaim;
    this.dialogRef.close({ output });
  }

  public onClaim(adminId: number): void {
    const output = new AssignSiteAction();
    output.action = AssignActionEnum.Assign;
    output.adjudicatorId = adminId;
    this.dialogRef.close({ output });
  }

  public ngOnInit(): void {
    this.getAdjudicators();
  }

  private getAdjudicators(): void {
    this.adjudicationResource.getAdjudicators()
      .subscribe((adjudicators: Admin[]) => this.adjudicators$.next(adjudicators));
  }
}
