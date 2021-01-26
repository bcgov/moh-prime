import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { BehaviorSubject } from 'rxjs';

import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { Admin } from '@auth/shared/models/admin.model';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { DialogOptions } from '../../dialog-options.model';

export enum ClaimActionEnum {
  Disclaim = 0,
  Claim = 1
}

export class ClaimEnrolleeAction {
  public action: ClaimActionEnum;
  public adjudicatorId?: number;
}

@Component({
  selector: 'app-claim-enrollee',
  templateUrl: './claim-enrollee.component.html',
  styleUrls: ['./claim-enrollee.component.scss']
})
export class ClaimEnrolleeComponent implements OnInit {
  public adjudicators$: BehaviorSubject<Admin[]>;
  private possibleAction: ClaimActionEnum;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
    private adjudicationResource: AdjudicationResource,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
  ) {
    this.possibleAction = data.data?.possibleAction;
    this.adjudicators$ = new BehaviorSubject<Admin[]>([]);
  }

  /**
   * @returns Whether to display as a "claim" form (`true`) or as a "disclaim" form (`false`)
   */
  public displayAsClaimPrompt(): boolean {
    return this.possibleAction == ClaimActionEnum.Claim;
  }

  public getTitle(): string {
    return (this.possibleAction == ClaimActionEnum.Claim ? "Claim" : "Disclaim") + " Enrollee";
  }

  public getPromptText(): string {
    return (this.possibleAction == ClaimActionEnum.Claim ?
      "Claim this enrolment or send it to a colleague, include a note if needed." :
      "You can unclaim the enrollee or assign them to a colleague.");
  }

  public onDisclaim(): void {
    const output = new ClaimEnrolleeAction();
    output.action = ClaimActionEnum.Disclaim;
    this.dialogRef.close({ output });
  }

  public onClaim(adminId?: number): void {
    const output = new ClaimEnrolleeAction();
    output.action = ClaimActionEnum.Claim;
    output.adjudicatorId = adminId;
    this.dialogRef.close({ output });
  }

  public ngOnInit() {
    this.getAdjudicators();
  }

  private getAdjudicators() {
    this.adjudicationResource.getAdjudicators()
      .subscribe((adjudicators: Admin[]) => this.adjudicators$.next(adjudicators));
  }
}
