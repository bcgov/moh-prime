import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})

export class BusyService {
  private _message: string;
  private _showSpinner: boolean;

  constructor() { };

  public get message(): string {
    return this._message
  }

  public showMessage(message: string): void {
    this._message = message;
  }
}
