import { Injectable, Inject } from '@angular/core';
import { DOCUMENT } from '@angular/common';

import { WindowRefService } from './window-ref.service';

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

  public scrollTop() {
    const contentContainer = this.document.querySelector('.mat-sidenav-content') || this.window;
    contentContainer.scroll({ top: 0, left: 0, behavior: 'smooth' });
  }
}
