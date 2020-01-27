import { DialogOptions } from './dialog-options.model';

export interface DialogDefaultOptions {
  [key: string]: (...args: any) => DialogOptions;
}
