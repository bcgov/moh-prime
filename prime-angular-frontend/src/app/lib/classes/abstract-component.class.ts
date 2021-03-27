import { Directive, OnDestroy } from '@angular/core';

import { Subject } from 'rxjs';

// tslint:disable-next-line: no-empty-interface
export interface IAbstractComponent { }

/**
 * @deprecated
 * Don't use this component for inheritance it is slowly being
 * refactored out and replaced with UntilDestroy decorator.
 */
@Directive()
// tslint:disable-next-line:directive-class-suffix
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
