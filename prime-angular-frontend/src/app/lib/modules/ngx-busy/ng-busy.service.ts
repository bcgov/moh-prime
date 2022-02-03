import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})

export class NgBusyService {
  public message: string;
  public isShowSpinner: boolean;

  constructor() { };

  public updateMessage(message: string): void {
    this.message = message;
  }

  public showSpinner(showSpinner: boolean): void {
    this.isShowSpinner = showSpinner;
  }
}
