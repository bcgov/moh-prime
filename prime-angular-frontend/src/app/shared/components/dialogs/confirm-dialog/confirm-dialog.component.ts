import { Component, OnInit, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

import { DialogDefaultContent as DialogDefaultOptions } from '../dialog-default-content.model';
import { DialogDefaultOptions as DialogOptions } from '../dialog-default-options.model';
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
    // if (typeof customOptions === 'string') {
    //   const content = defaultOptions[customOptions]();
    // } else {
    const icon = (customOptions.actionType === 'warn') ? 'warning' : 'help';
    this.options = {
      icon,
      actionType: 'primary',
      actionText: 'Confirm',
      cancelText: 'Cancel',
      cancelHide: false,
      ...customOptions
    };
    // }
  }

  public ngOnInit() { }
}
