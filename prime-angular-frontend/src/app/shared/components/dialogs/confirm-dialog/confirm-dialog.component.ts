import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

import { DialogOptions } from '../dialog-options.model';
import { DialogDefaultOptions } from '../dialog-default-options.model';
import { DIALOG_DEFAULT_OPTION } from '../dialogs-properties.provider';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss']
})
export class ConfirmDialogComponent implements OnInit {
  public options: DialogOptions;

  constructor(
    public dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public customOptions: DialogOptions,
    @Inject(DIALOG_DEFAULT_OPTION) public defaultOptions: DialogDefaultOptions
  ) {
    this.options = (typeof customOptions === 'string')
      ? this.getOptions(defaultOptions[customOptions]())
      : this.getOptions(customOptions);
  }

  public ngOnInit() { }

  private getOptions(dialogOptions: DialogOptions) {
    const options: DialogOptions = {
      actionType: 'primary',
      actionText: 'Confirm',
      cancelText: 'Cancel',
      cancelHide: false,
      ...dialogOptions
    };

    return {
      icon: (options.actionType === 'warn') ? 'warning' : 'help',
      ...options
    };
  }
}
