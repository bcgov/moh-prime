import { Component, Inject, OnInit, Output, EventEmitter } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
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
  @Output() public vendorChanged: EventEmitter<boolean>;

  public siteId: number;
  public title: string;
  public vendorChangeText: string;
  public vendorCode: number;
  public form: UntypedFormGroup;
  public changeVendorClicked: boolean;

  constructor(
    private siteResource: SiteResource,
    private fb: UntypedFormBuilder,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
  ) {
    this.siteId = data.data.siteId;
    this.title = data.data.title;
    this.vendorChangeText = data.data.vendorChangeText;
    this.vendorCode = data.data.vendorCode;
    this.vendorChanged = new EventEmitter<boolean>();
  }

  public get note(): UntypedFormControl {
    return this.form.get('note') as UntypedFormControl;
  }

  public get requester(): UntypedFormControl {
    return this.form.get('requester') as UntypedFormControl;
  }

  public onCancel() {
    this.dialogRef.close();
    this.vendorChanged.emit(false);
  }

  public onChangeVendor() {
    if (this.form.valid) {
      this.changeVendorClicked = true;

      this.siteResource.updateVendor(this.siteId, this.vendorCode, this.getOutputString())
        .subscribe(() => {
          this.dialogRef.close({ reload: true });
          this.vendorChanged.emit(true);
        });
    }
    this.requester.markAsTouched();
    this.note.markAsTouched();
  }

  public ngOnInit(): void {
    this.createFormInstance();
    // To accommodate lengthy instruction text
    this.dialogRef.updateSize('750px', '50%');
    this.changeVendorClicked = false;
  }

  protected createFormInstance() {
    this.form = this.fb.group({
      note: [null, [Validators.required]],
      requester: [null, [Validators.required]],
    });
  }

  private getOutputString(): string {
    return `${this.vendorChangeText}, Requestor: ${this.requester.value}, Note: ${this.note.value}.`;
  }
}
