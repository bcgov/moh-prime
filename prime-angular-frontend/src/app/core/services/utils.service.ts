import { Injectable, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { SortDirection } from '@angular/material/sort';

import { WindowRefService } from './window-ref.service';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { EmailUtils } from '@lib/utils/email-utils.class';

export type SortWeight = -1 | 0 | 1;

@Injectable({
  providedIn: 'root'
})
export class UtilsService {
  private window: Window;

  constructor(
    @Inject(DOCUMENT) private document: Document,
    @Inject(APP_CONFIG) protected config: AppConfig,
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
   * Scroll to a material form field that is invalid, and if contained
   * within a <section> scroll to the section instead.
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
   * Checks if the browser is Internet Explorer, or pre-Chromium
   * Edge.
   *
   * The reversed attribute of ordered lists is not supported in IE or
   * pre-Chromium Edge, but has been supported in all other browsers
   * forevers!!!
   * @see https://caniuse.com/?search=reversed
   */
  public isIEOrPreChromiumEdge(): boolean {
    return !('reversed' in document.createElement('ol'));
  }

  /**
   * @description
   * Generic sorting of a JSON object by key.
   */
  public sortByKey<T extends { [key: string]: any }>(key: keyof T): (a: T, b: T) => SortWeight {
    return (a: T, b: T) => this.sort<T>(a[key], b[key]);
  }

  /**
   * @description
   * Generic sorting of a JSON object by direction.
   */
  public sortByDirection<T>(a: T, b: T, direction: SortDirection = 'asc', withTrailingNull: boolean = true): SortWeight {
    let result: SortWeight;

    if (a === null && withTrailingNull) {
      result = -1;
    } else if (b === null && withTrailingNull) {
      result = 1;
    } else {
      result = this.sort(a, b);
    }

    if (direction === 'desc') {
      result *= -1;
    }

    return result;
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
   * Download a PDF.
   */
  public downloadPdf(file: string | Blob, filename: string): void {
    if (typeof file === 'string') {
      file = this.base64ToBlob(file);
    }

    this.downloadDocument(file, filename);
  }

  /**
   * @description
   * Download a document using a document manager download token.
   */
  public downloadToken(token: string): void {
    this.window.location
      .assign(`${this.config.documentManagerUrl}/documents/downloads/${token}`);
  }
}
