import { Injectable, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';

import { WindowRefService } from './window-ref.service';

export type SortWeight = -1 | 0 | 1;

@Injectable({
  providedIn: 'root'
})
export class UtilsService {
  private window: Window;

  constructor(
    @Inject(DOCUMENT) private document: Document,
    private windowRef: WindowRefService
  ) {
    this.window = windowRef.nativeWindow;
  }

  /**
   * @description
   * Scroll to the top of the mat-sidenav container.
   */
  public scrollTop() {
    const contentContainer = this.document.querySelector('.mat-sidenav-content') || this.window;
    contentContainer.scroll({ top: 0, left: 0, behavior: 'smooth' });
  }

  /**
   * @description
   * Scroll to have the element in view.
   */
  public scrollTo(el: Element): void {
    if (el) {
      el.scrollIntoView({ behavior: 'smooth' });
    }
  }

  /**
   * @description
   * Scroll to a material form field that is invalid.
   * If it is in a section scroll to the section instead.
   */
  public scrollToErrorSection(): void {
    const firstElementWithError = document.querySelector('mat-form-field.ng-invalid');

    if (firstElementWithError) {
      const element = (firstElementWithError.closest('section') == null)
        ? firstElementWithError
        : firstElementWithError.closest('section');

      this.scrollTo(element);
    } else {
      this.scrollTop();
    }
  }

  /**
   * @description
   * Checks if the browser is Internet Explorer.
   */
  public isIE(): boolean {
    const isIE = /msie\s|trident\//i.test(window.navigator.userAgent);
    return isIE;
  }

  /**
   * @description
   * Generic sorting of a JSON object by key.
   */
  public sort<T>(a: T, b: T, key: string): SortWeight {
    return (a[key] > b[key])
      ? 1 : (a[key] < b[key])
        ? -1 : 0;
  }
}
