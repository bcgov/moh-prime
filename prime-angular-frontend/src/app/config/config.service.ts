import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {
  private configuration: any;

  constructor() { }

  public get config() {
    return this.configuration;
  }

  /**
   * Load runtime configuration.
   *
   * @returns {Promise<any>}
   * @memberof ConfigService
   */
  public async load(): Promise<any> {
    // TODO: pipe multiple observables for additional config
    return this.getConfiguration()
      .toPromise()
      .then((config) => this.configuration = config);
  }

  /**
   * Get the configuration for bootstrapping the application.
   *
   * @private
   * @returns {Observable<any>}
   * @memberof ConfigService
   */
  // TODO: rename to reflect the type of configuration
  private getConfiguration(): Observable<any> {
    // TODO: add configuration endpoint
    return new Observable((subscriber) => {
      subscriber.complete();
    });
  }
}
