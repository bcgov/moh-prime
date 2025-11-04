import { Component, OnInit, Inject } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { FormUtilsService } from '@core/services/form-utils.service';
import { DialogOptions } from '../../dialog-options.model';
import { Config } from '@config/config.model';
import { SiteResource } from '@core/resources/site-resource.service';

export enum SiteActionEnum {
  Archive = 0,
  Restore = 1,
}

@Component({
  selector: 'app-site-archive-restore',
  templateUrl: './site-archive-restore.component.html',
  styleUrls: ['./site-archive-restore.component.scss']
})
export class SiteArchiveRestoreComponent implements OnInit {
  public form: UntypedFormGroup;
  public siteCloseReasons: Config<number>[];
  public siteId: number;
  public action: SiteActionEnum;

  public title: string;
  public actionText: string;
  public message: string;

  constructor(
    private fb: UntypedFormBuilder,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
    protected formUtilsService: FormUtilsService,
    protected siteResource: SiteResource,
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
  ) {
    this.siteId = data.data.siteId;
    this.action = data.data.action;

    this.title = this.isArchive ? 'Archive Site Registration' :
      this.isRestore ? 'Restore Archived Site' : 'NA';
    this.message = this.isArchive ? 'Are you sure you want to archive this Site Registration?' :
      this.isRestore ? 'Are you sure you want to restore this Site Registration?' : 'NA';
    this.actionText = this.isArchive ? 'Archive Site' :
      this.isRestore ? 'Restore Site' : 'NA';
  }

  public get note(): UntypedFormControl {
    return this.form.get('note') as UntypedFormControl;
  }

  public get isArchive(): boolean {
    return this.action === SiteActionEnum.Archive;
  }

  public get isRestore(): boolean {
    return this.action === SiteActionEnum.Restore;
  }

  public ngOnInit(): void {
    this.createFormInstance();
  }

  public onCancel(): void {
    this.dialogRef.close();
  }

  public onSubmit(): void {
    if (this.form.valid) {
      if (this.isArchive) {
        this.siteResource.archiveSite(this.siteId, this.note.value)
          .subscribe(() => this.dialogRef.close({ reload: true }));
      } else if (this.isRestore) {
        this.siteResource.restoreArchivedSite(this.siteId, this.note.value)
          .subscribe(() => this.dialogRef.close({ reload: true }));
      }
    }
    this.note.markAsTouched();
  }

  private createFormInstance(): void {
    this.form = this.fb.group({
      note: [, [Validators.required]]
    });

  }
}
