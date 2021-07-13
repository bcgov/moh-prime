import { Injectable } from '@angular/core';

import { AbstractLoggerService } from './abstract-logger.service';

@Injectable({
  providedIn: 'root'
})
export class LoggerService extends AbstractLoggerService {
  constructor() {
    super();
  }

}
