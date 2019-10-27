import { DialogDefaultOptions } from './dialog-default-options.model';

export interface DialogDefaultContent {
  [key: string]: (...args: any) => DialogDefaultOptions;
}
