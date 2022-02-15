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
   * Pipe for setting/clearing the busy message that wraps the execution of a customizeable operator function.
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
