import { Injectable } from '@angular/core';
import { Observable, pipe, OperatorFunction } from 'rxjs';
import { tap } from 'rxjs/operators';
@Injectable({
  providedIn: 'root',
})

export class BusyService {
  private _message: string;

  constructor() { };

  public get message(): string {
    return this._message
  }

  /**
   * @description
   * @param message the message to be displayed when a busy has a subscription or observable
   * @param operator the operator to run while the busy message and spinner are shown
   * @returns OperatorFunction
   */
  public showMessagePipe<T>(message: string, operator: OperatorFunction<T, any>): OperatorFunction<T, Observable<any>> {
    return pipe(
      tap((_) => { this._message = message }),
      operator,
      tap((_) => { this._message = '' })
    );
  }
}
