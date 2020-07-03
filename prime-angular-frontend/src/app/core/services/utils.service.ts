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
   * Conversion of Base64 encoded document to a Blob.
   */
  public base64ToBlob(base64: string, type: string = 'application/pdf'): Blob {
    const decoded = window.atob(base64.replace(/\s/g, ''));
    const len = decoded.length;
    const buffer = new ArrayBuffer(len);
    const data = new Uint8Array(buffer);

    for (let i = 0; i < len; i++) {
      data[i] = decoded.charCodeAt(i);
    }

    return new Blob([data], { type });
  }

  /**
   * @description
   * Download a document.
   */
  public downloadDocument(file: Blob, filename: string): void {
    // Allow downloads in IE and Edge browsers prior to
    // Chromium-based Edge where it is deprecated
    if (navigator && navigator.msSaveOrOpenBlob) {
      navigator.msSaveOrOpenBlob(file, filename);
      return;
    }

    const data = URL.createObjectURL(file);
    const link = document.createElement('a');
    link.href = data;
    link.download = filename;
    link.target = '_blank';
    link.click();
    setTimeout(() => {
      URL.revokeObjectURL(data);
      link.remove();
    }, 100);
  }

  /**
   * @description
   * Download a document using a document manager download token.
   */
  public downloadToken(token: string): void {
    // TODO: Replace with injected env variable
    window.location.replace(`http://localhost:6001/documents/downloads/${token}`);
  }
}
