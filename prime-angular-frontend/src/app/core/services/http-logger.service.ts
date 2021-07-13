import { Injectable } from '@angular/core';
import { LoggerService } from './logger.service';

@Injectable({
  providedIn: 'root'
})
export class HttpLoggerService extends LoggerService {

  constructor() {
    super();
  }
}
