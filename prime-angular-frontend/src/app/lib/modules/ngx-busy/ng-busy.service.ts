import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})

export class NgBusyService {
  private message: string;
  private isShowSpinner: boolean;

  constructor() { };

  public updateMessage(message: string): void {
    this.message = message;
  }

  public showSpinner(showSpinner: boolean): void {
    this.isShowSpinner = showSpinner;
  }

  public getMessage(): string {
    return this.message
  }

  public getIsShowSpinner(): boolean {
    return this.isShowSpinner;
  }
}
