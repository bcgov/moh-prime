import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Admin } from '@auth/shared/models/admin.model';
import { BehaviorSubject } from 'rxjs';
import { Enrolment } from '@shared/models/enrolment.model';
import { MatDialogRef } from '@angular/material';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';

export class ClaimEnrolleeAction {
  public action: ClaimActionEnum;
  public adjudicatorId?: number;
}

export enum ClaimActionEnum {
  UnClaim = 0,
  Claim = 1,
}

@Component({
  selector: 'app-claim-enrollee',
  templateUrl: './claim-enrollee.component.html',
  styleUrls: ['./claim-enrollee.component.scss']
})
export class ClaimEnrolleeComponent implements OnInit {
  public adjudicators: BehaviorSubject<Admin[]>;

  constructor(
    private adjudicationResource: AdjudicationResource,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
  ) {
    this.adjudicators = new BehaviorSubject<Admin[]>([]);
  }

  public unClaim(): void {
    const output = new ClaimEnrolleeAction();
    output.action = ClaimActionEnum.UnClaim;
    this.dialogRef.close({ output, });
  }

  public claim(adminId: number): void {
    const output = new ClaimEnrolleeAction();
    output.action = ClaimActionEnum.Claim;
    output.adjudicatorId = adminId;
    this.dialogRef.close({ output, });
  }

  async ngOnInit() {
    this.getAdjudicators();
  }

  private async getAdjudicators(): Promise<void> {
    await this.adjudicationResource.getAdjudicators()
      .subscribe((adjudicators: Admin[]) => {
        this.adjudicators.next(adjudicators);
      });
  }

}
