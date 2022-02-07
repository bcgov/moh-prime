import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})

export class NgBusyService {
  private _message: string;
  private _showMessage: boolean;
  private _showSpinner: boolean;

  constructor() { };

  public set message(message: string) {
    this._message = message;
  }

  public set showMessage(showMessage: boolean) {
    this._showMessage = showMessage;
  }

  public set showSpinner(showSpinner: boolean) {
    this._showSpinner = showSpinner;
  }

  public get message(): string {
    return this._message
  }

  public get showMessage(): boolean {
    return this._showMessage;
  }

  public get showSpinner(): boolean {
    return this._showSpinner;
  }
}
