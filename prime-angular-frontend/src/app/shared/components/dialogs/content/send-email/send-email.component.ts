import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { SiteResource } from '@core/resources/site-resource.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-send-email',
  templateUrl: './send-email.component.html',
  styleUrls: ['./send-email.component.scss']
})
export class SendEmailComponent implements OnInit {
  public title: string;
  public readonly contacts: { label: string, email: string }[];

  constructor(
    @Inject(MAT_DIALOG_DATA) public options: DialogOptions,
    private siteResource: SiteResource,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
  ) {
    this.title = options.title;
    this.contacts = options.data.contacts;
  }

  public onCancel(): void {
    this.dialogRef.close();
  }

  public onSelect(email: string): void {
    this.dialogRef.close(email);
  }

  public ngOnInit(): void { }
}
