import { Injectable } from '@angular/core';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { ApiResource } from '@core/resources/api-resource.service';
import { LoggerService } from '@core/services/logger.service';
import { ToastService } from '@core/services/toast.service';
import { PhsaLabtech } from '../models/phsa-lab-tech.model';

@Injectable({
  providedIn: 'root'
})
export class PhsaLabtechResource {
  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public createEnrollee(payload: PhsaLabtech): void {

    this.logger.trace('TODO: call API', payload);
  }
}
