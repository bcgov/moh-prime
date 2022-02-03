import { Injectable } from '@angular/core';

import { Observable, Observer } from 'rxjs';
import { share } from 'rxjs/operators';


@Injectable({
  providedIn: 'root',
})

export class NgBusyService {
  public message$: Observable<string>;
  public isShowSpinner$: Observable<boolean>;
  private message: string;
  private isShowSpinner: boolean;
  private messageObserver: Observer<string>;
  private spinnerObserver: Observer<boolean>;

  constructor() {
    this.isShowSpinner = false;
    this.message = '';
    this.message$ = new Observable<string>(m => this.messageObserver = m).pipe(share());
    this.isShowSpinner$ = new Observable<boolean>(m => this.spinnerObserver = m).pipe(share());
  };

  public updateMessage(message: string): void {
    this.message = message;
    this.messageObserver.next(this.message);
  }

  public showSpinner(showSpinner: boolean): void {
    this.isShowSpinner = showSpinner;
    this.spinnerObserver.next(this.isShowSpinner);
  }
}
