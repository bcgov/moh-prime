import {Component, Inject, OnInit} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from '@angular/material/dialog';

import {DialogOptions} from '@shared/components/dialogs/dialog-options.model';
import {ConfirmDialogComponent} from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import {BulkEmailType} from '@shared/enums/bulk-email-type';

@Component({
  selector: 'app-send-bulk-email',
  templateUrl: './send-bulk-email.component.html',
  styleUrls: ['./send-bulk-email.component.scss']
})
export class SendBulkEmailComponent implements OnInit {
  public title: string;

  public BulkEmailType = BulkEmailType;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>
  ) {
    this.title = data.title;
  }

  public onCancel(): void {
    this.dialogRef.close();
  }

  public onSelect(bulkEmailType: BulkEmailType): void {
    this.dialogRef.close(bulkEmailType);
  }

  public ngOnInit(): void { }
}
