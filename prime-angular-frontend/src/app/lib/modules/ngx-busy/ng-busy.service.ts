import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})

export class NgBusyService {
  private _message: string;
  private _isShowSpinner: boolean;

  constructor() { };

  public set message(message: string) {
    this._message = message;
  }

  public set isShowSpinner(showSpinner: boolean) {
    this._isShowSpinner = showSpinner;
  }

  public get message(): string {
    return this._message
  }

  public get isShowSpinner(): boolean {
    return this._isShowSpinner;
  }
}
