import { Observable } from 'rxjs';

/**
 * An abstract logger
 */
export abstract class AbstractLogger {
  /**
   *
   * @param type log entry type such as error, info, warn etc.
   * @param params log entry data
   */
  public abstract log(type: string, params: { msg?: string, data?: any[] }): Observable<boolean>
}
