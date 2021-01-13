import { Directive, OnDestroy } from '@angular/core';

import { Subject } from 'rxjs';

// tslint:disable-next-line: no-empty-interface
export interface IAbstractComponent { }

@Directive()
export abstract class AbstractComponent implements IAbstractComponent, OnDestroy {
  /**
   * @description
   * Destroy component life cycle emitter.
   */
  protected componentDestroyed$: Subject<any>;

  constructor() {
    this.ngOnDestroyListener();
  }

  public ngOnDestroy() { }

  /**
   * @description
   * Clean up component subscriptions to prevent memory leaks.
   *
   * @example
   * anObservable
   *   .pipe(
   *     takeUntil(this.componentDestroyed$)
   *     ...
   *   )
   *   .subscribe(...);
   */
  private ngOnDestroyListener() {
    this.componentDestroyed$ = new Subject<void>();
    const destroyFunc = this.ngOnDestroy;
    this.ngOnDestroy = () => {
      destroyFunc.bind(this)();
      this.componentDestroyed$.next();
      this.componentDestroyed$.complete();
    };
  }
}
