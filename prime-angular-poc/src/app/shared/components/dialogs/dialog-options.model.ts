import { Component } from '@angular/compiler/src/core';

export interface DialogOptions {
  icon?: string;
  imageSrc?: string; // Alternative to an icon
  title?: string;
  message?: string;
  actionType?: 'primary' | 'accent' | 'warn';
  actionText?: string;
  cancelText?: string;
  cancelHide?: boolean;
  component?: any;
  data?: { [key: string]: any };
}
