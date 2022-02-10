import { Injectable } from '@angular/core';
import { Observable, pipe, UnaryFunction } from 'rxjs';
import { tap, exhaustMap } from 'rxjs/operators';

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
   * @param message the message to be displayed when a busy has a busy thing (subscription, promise, array of either... etc)
   * @param request the function or observable that will run while the busy message and spinner are shown
   * @returns OperatorFunction
   */
  public showMessagePipe<T, R>(message: string, request: R): UnaryFunction<Observable<T>, Observable<any>> {
    return pipe(
      tap((_) => { this._message = message }),
      exhaustMap((value: T) => (typeof request === 'function')
        ? request(value)
        : request
      ),
      tap((_) => { this._message = '' })
    );
  }
}
