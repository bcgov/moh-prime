import { Injectable, NgZone } from '@angular/core';

import { Observable } from 'rxjs';
import { distinctUntilChanged } from 'rxjs/operators';

import { WindowRefService } from './window-ref.service';
import { DeviceResolution } from '../../shared/enums/device-resolutions.enum';

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

  public get isMobile(): boolean {
    return this.device === DeviceResolution.MOBILE;
  }

  public get isTablet(): boolean {
    return this.device === DeviceResolution.TABLET;
  }

  public get isDesktop(): boolean {
    return this.device === DeviceResolution.DESKTOP;
  }

  public get isWideDesktop(): boolean {
    return this.device === DeviceResolution.WIDE;
  }

  public get device(): DeviceResolution {
    return this.getDevice();
  }

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

  private setupViewportListener() {
    this.viewport = new Observable(observer => {
      this.window.addEventListener('resize', (event) => {
        this.ngZone.run(() => observer.next(this.getDevice()));
      });
    });
  }

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
