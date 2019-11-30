import { Injectable } from '@angular/core';

function getWindow(): any {
  return window;
}

@Injectable({
  providedIn: 'root'
})
export class WindowRefService {
  constructor() { }

  /**
   * @description
   * Get a reference to the native window object.
   */
  get nativeWindow(): Window {
    return getWindow();
  }
}
