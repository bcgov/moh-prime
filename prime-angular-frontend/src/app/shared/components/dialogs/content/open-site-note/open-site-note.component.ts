import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { SiteResource } from '@core/resources/site-resource.service';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '../../dialog-options.model';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-open-site-note',
  templateUrl: './open-site-note.component.html',
  styleUrls: ['./open-site-note.component.scss']
})
export class OpenSiteNoteComponent implements OnInit {
  public form: FormGroup;
  public siteId: number;

  constructor(
    private siteResource: SiteResource,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
  ) {
    this.siteId = data.data.siteId;
  }

  public get note(): FormControl {
    return this.form.get('note') as FormControl;
  }

  public onCancel(): void {
    this.dialogRef.close();
  }

  public onOpenSite(): void {
    if (this.form.valid) {
      this.siteResource.openSite(this.siteId, this.note.value)
        .subscribe(() => {
          this.dialogRef.close({ reload: true });
        });
    }
    this.note.markAsTouched();
  }

  public ngOnInit(): void {
    this.createFormInstance();
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

}
