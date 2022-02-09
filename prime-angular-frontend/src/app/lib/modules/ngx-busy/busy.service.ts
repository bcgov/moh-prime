import { Injectable } from '@angular/core';
import { EMPTY, Observable, pipe, MonoTypeOperatorFunction } from 'rxjs';
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

  public showMessagePipe<T>(message: string, request: Observable<any>): MonoTypeOperatorFunction<T> {
    return pipe(
      tap((_) => { this._message = message }),
      exhaustMap((result: T) => (result) ? request : EMPTY),
      tap((_) => { this._message = '' })
    );
  }
}
