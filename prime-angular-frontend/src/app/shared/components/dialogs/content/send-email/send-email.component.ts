import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { Site } from '@registration/shared/models/site.model';

import { SiteResource } from '@core/resources/site-resource.service';

@Component({
  selector: 'app-send-email',
  templateUrl: './send-email.component.html',
  styleUrls: ['./send-email.component.scss']
})
export class SendEmailComponent implements OnInit {
  public title: string;
  public site: Site;
  private siteId: number;

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
    private siteResource: SiteResource,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
  ) {
    this.title = data.title;
    this.siteId = data.data.siteId;
  }

  public onCancel(): void {
    this.dialogRef.close();
  }

  public onSelect(email: string): void {
    this.dialogRef.close(email);
  }

  public ngOnInit(): void {
    this.getSite(this.siteId);
  }

  private getSite(siteId: number): void {
    this.siteResource.getSiteById(siteId)
      .subscribe((site: Site) => this.site = site);
  }
}
