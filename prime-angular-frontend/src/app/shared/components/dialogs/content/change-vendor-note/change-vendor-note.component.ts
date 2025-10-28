import { Component, Inject, OnInit, Output, EventEmitter } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { SiteResource } from '@core/resources/site-resource.service';

import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '../../dialog-options.model';
import { VendorConfig } from '@config/config.model';

@Component({
  selector: 'app-change-vendor-note',
  templateUrl: './change-vendor-note.component.html',
  styleUrls: ['./change-vendor-note.component.scss']
})
export class ChangeVendorNoteComponent implements OnInit {
  @Output() public vendorChanged: EventEmitter<any>;

  public siteId: number;
  public title: string;
  public vendorChangeText: string;
  public vendorCode: number;
  public form: UntypedFormGroup;
  public changeVendorClicked: boolean;
  public siteVendors: VendorConfig[];

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
    this.siteVendors = data.data.siteVendors;
    this.vendorChanged = new EventEmitter<any>();
  }

  public get note(): UntypedFormControl {
    return this.form.get('note') as UntypedFormControl;
  }

  public get requester(): UntypedFormControl {
    return this.form.get('requester') as UntypedFormControl;
  }

  public get vendor(): UntypedFormControl {
    return this.form.get('vendor') as UntypedFormControl;
  }

  public onCancel() {
    this.dialogRef.close();
    this.vendorChanged.emit(false);
  }

  public onChangeVendor() {
    if (this.form.valid) {
      this.changeVendorClicked = true;
      this.vendorCode = this.vendor.value;

      this.siteResource.updateVendor(this.siteId, this.vendorCode, this.getOutputString())
        .subscribe(() => {
          this.dialogRef.close({ result: true, vendorCode: this.vendorCode });
          this.vendorChanged.emit({ result: true, vendorCode: this.vendorCode });
        });
    }
    this.requester.markAsTouched();
    this.note.markAsTouched();
  }

  public ngOnInit(): void {
    this.createFormInstance();
    // To accommodate lengthy instruction text
    this.dialogRef.updateSize('750px', '580px');
    this.changeVendorClicked = false;
    if (this.vendorCode) {
      this.vendor.setValue(this.vendorCode);
    }
  }

  protected createFormInstance() {
    this.form = this.fb.group({
      note: [null, [Validators.required]],
      requester: [null, [Validators.required]],
      vendor: [null, [Validators.required]],
    });
  }

  private getOutputString(): string {
    return `${this.vendorChangeText}, Requestor: ${this.requester.value}, Note: ${this.note.value}.`;
  }
}
