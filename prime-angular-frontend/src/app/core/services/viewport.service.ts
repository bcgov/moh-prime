import { Injectable, NgZone } from '@angular/core';
import { Observable } from 'rxjs';
import { distinctUntilChanged } from 'rxjs/operators';

import { WindowRefService } from '@core/services/window-ref.service';
import { DeviceResolution } from '@shared/enums/device-resolution.enum';

@Injectable({
  providedIn: 'root'
})
export class ViewportService {
  private viewport: Observable<DeviceResolution>;
  private window: Window;

  constructor(
    private windowRef: WindowRefService,
    private ngZone: NgZone
  ) {
    this.viewport = null;
    this.window = windowRef.nativeWindow;
  }

  /**
   * Check viewport size is equivalent to mobile.
   *
   * @returns {boolean}
   * @memberof ViewportService
   */
  public get isMobile(): boolean {
    return this.device === DeviceResolution.MOBILE;
  }

  /**
   * Check viewport size is equivalent to mobile.
   *
   * @returns {boolean}
   * @memberof ViewportService
   */
  public get isTablet(): boolean {
    return this.device === DeviceResolution.TABLET;
  }

  /**
   * Check viewport size is equivalent to desktop.
   *
   * @returns {boolean}
   * @memberof ViewportService
   */
  public get isDesktop(): boolean {
    return this.device === DeviceResolution.DESKTOP;
  }

  /**
   * Check viewport size is equivalent to wide desktop.
   *
   * @returns {boolean}
   * @memberof ViewportService
   */
  public get isWideDesktop(): boolean {
    return this.device === DeviceResolution.WIDE;
  }

  /**
   * Gets the device based on the viewport dimensions.
   *
   * @readonly
   * @type {DeviceResolution}
   * @memberof ViewportService
   */
  public get device(): DeviceResolution {
    return this.getDevice();
  }

  /**
   * Gets the observable for subscribing to the viewport onresize event.
   *
   * @returns
   * @memberof ViewportService
   */
  public onResize() {
    if (this.viewport === null) {
      this.setupViewportListener();
    }
    // Only emit when the current value is different than the last
    return this.viewport
      .pipe(
        distinctUntilChanged()
      );
  }

  /**
   * Setups the observable and viewport onresize event listener.
   *
   * @private
   * @memberof ViewportService
   */
  private setupViewportListener() {
    this.viewport = new Observable(observer => {
      this.window.addEventListener('resize', (event) => {
        this.ngZone.run(() => observer.next(this.getDevice()));
      });
    });
  }

  /**
   * Gets the type of "device" based on the viewport width.
   * ---
   * Devices are based on generally accepted media queries for viewport
   * widths, and does not detect the actual device.
   *
   * Mobile: Small < 576px
   * Tablet: Medium > 576px AND < 768px
   * Desktop: Large > 768px
   * Wide: Extra Large > 992px
   *
   * @see https://getbootstrap.com/docs/4.0/layout/grid/#variables
   *
   * @private
   * @returns {DeviceResolution}
   * @memberof ViewportService
   */
  private getDevice(): DeviceResolution {
    const width = this.window.innerWidth;

    if (width >= 1200) {
      return DeviceResolution.WIDE;
    } else if (width >= 992) {
      return DeviceResolution.DESKTOP;
    } else if (width >= 768) {
      return DeviceResolution.TABLET;
    } else {
      return DeviceResolution.MOBILE;
    }
  }
}
