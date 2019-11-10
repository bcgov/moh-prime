import { Component, OnInit, Inject, ComponentFactoryResolver, ViewChild, ViewContainerRef, AfterViewInit, ChangeDetectionStrategy } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material';

import { DialogDefaultOptions } from '../dialog-default-options.model';
import { IDialogContent } from '../dialog-content.model';
import { DialogOptions } from '../dialog-options.model';
import { DIALOG_DEFAULT_OPTION } from '../dialogs-properties.provider';
import { DialogContentDirective } from '../dialog-content.directive';

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ConfirmDialogComponent implements OnInit, AfterViewInit {
  public options: DialogOptions;
  @ViewChild(DialogContentDirective, { static: true }) public dialogContentHost: DialogContentDirective;

  constructor(
    public dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public customOptions: DialogOptions,
    @Inject(DIALOG_DEFAULT_OPTION) public defaultOptions: DialogDefaultOptions,
    private componentFactoryResolver: ComponentFactoryResolver
  ) {
    this.options = (typeof customOptions === 'string')
      ? this.getOptions(defaultOptions[customOptions]())
      : this.getOptions(customOptions);
  }

  public ngOnInit() { }

  public ngAfterViewInit() {
    if (this.options.component) {
      this.loadDialogContentComponent(this.options.component, this.options.data);
    }
  }

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

  private loadDialogContentComponent(component: any, data: any) {
    const componentFactory = this.componentFactoryResolver.resolveComponentFactory(component);

    const viewContainerRef = this.dialogContentHost.viewContainerRef;
    viewContainerRef.clear();

    const componentRef = viewContainerRef.createComponent(componentFactory);
    (componentRef.instance as IDialogContent).data = data;
  }
}
