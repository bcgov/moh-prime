import { EventEmitter } from '@angular/core';

export interface IDialogContent {
  data: any;
  output?: EventEmitter<any>;
}
