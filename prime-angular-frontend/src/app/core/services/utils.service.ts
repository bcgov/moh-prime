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
  public sortByKey<T>(a: { [key: string]: any }, b: { [key: string]: any }, key: string): SortWeight {
    return this.sort<T>(a[key], b[key]);
  }

  /**
   * @description
   * Generic sorting of a JSON object by key.
   */
  public sort<T>(a: T, b: T): SortWeight {
    return (a > b)
      ? 1 : (a < b)
        ? -1 : 0;
  }

  /**
   * @description
   * Download a document from an ArrayBuffer
   */
  public downloadDocumentFromArrayBuffer(arrayBuffer: ArrayBuffer, type: string, filename: string): boolean {
    const blob: any = new Blob([arrayBuffer], { type });

    // IE doesn't allow using a blob object directly as link href
    // instead it is necessary to use msSaveOrOpenBlob
    if (navigator && navigator.msSaveOrOpenBlob) {
      navigator.msSaveOrOpenBlob(blob, filename);
      return;
    }

    // For other browsers:
    // Create a link pointing to the ObjectURL containing the blob.
    const data = URL.createObjectURL(blob);
    const link = document.createElement('a');
    link.href = data;
    link.download = filename;
    link.click();
    setTimeout(() => {
      // For Firefox it is necessary to delay revoking the ObjectURL
      URL.revokeObjectURL(data);
    }, 100);
  }
}
