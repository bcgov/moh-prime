import { Component, Inject, OnInit, Output, EventEmitter } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { SiteResource } from '@core/resources/site-resource.service';

import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '../../dialog-options.model';

@Component({
  selector: 'app-change-vendor-note',
  templateUrl: './change-vendor-note.component.html',
  styleUrls: ['./change-vendor-note.component.scss']
})
export class ChangeVendorNoteComponent implements OnInit {
  @Output() public vendorChnaged: EventEmitter<boolean>;

  public siteId: number;
  public title: string;
  public vendorChangeText: string;
  public vendorCode: number;
  public form: FormGroup;
  constructor(
    private siteResource: SiteResource,
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
  ) {
    this.siteId = data.data.siteId;
    this.title = data.data.title;
    this.vendorChangeText = data.data.vendorChangeText;
    this.vendorCode = data.data.vendorCode;
    this.vendorChnaged = new EventEmitter<boolean>();
  }

  public get note(): FormControl {
    return this.form.get('note') as FormControl;
  }

  public get requester(): FormControl {
    return this.form.get('requester') as FormControl;
  }

  public onCancel() {
    this.dialogRef.close();
    this.vendorChnaged.emit(false);
  }

  public onChangeVendor() {
    if (this.form.valid) {
      this.siteResource.updateVendor(this.siteId, this.vendorCode, this.getOutputString())
        .subscribe(() => {
          this.dialogRef.close({ reload: true });
          this.vendorChnaged.emit(true);
        });
    }
    this.requester.markAsTouched();
  }

  public ngOnInit(): void {
    this.createFormInstance();
  }

  protected createFormInstance() {
    this.form = this.fb.group({
      note: [
        {
          value: '',
          disabled: false,
        },
        []
      ],
      requester: [null, [Validators.required]],
    });
  }

  private getOutputString(): string {
    return `${this.vendorChangeText}, Requester:${this.requester.value}, Note:${this.note.value}`;
  }
}
