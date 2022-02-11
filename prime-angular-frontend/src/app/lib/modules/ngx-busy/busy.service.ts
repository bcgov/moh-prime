import { Injectable } from '@angular/core';
import { Observable, isObservable, pipe, UnaryFunction } from 'rxjs';
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
   * @param observableOrFunction the observable or function that will run while the busy message and spinner are shown
   * @returns OperatorFunction
   */
  public showMessagePipe<T, R>(
    message: string,
    observableOrFunction: Observable<T> | ((params: R) => Observable<T>)
  ): UnaryFunction<Observable<unknown>, Observable<T>> {
    const operator = isObservable(observableOrFunction)
      ? exhaustMap(() => observableOrFunction)
      : exhaustMap(observableOrFunction);

    return pipe(
      tap((_) => (this._message = message)),
      operator,
      tap((_) => (this._message = ''))
    );
  }
}
