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
   * Get a reference to the native window object.
   *
   * @readonly
   * @type {*}
   * @memberof WindowRefService
   */
  get nativeWindow(): Window {
    return getWindow();
  }
}
