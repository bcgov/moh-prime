import { Injectable } from '@angular/core';
import { ApiResource } from '@core/resources/api-resource.service';
import { ApiResourceUtilsService } from '@core/resources/api-resource-utils.service';
import { LoggerService } from '@core/services/logger.service';
import { Registrant } from '@shared/models/registrant';

@Injectable({
  providedIn: 'root'
})
export class RegistrantResource {

  constructor(
    private apiResource: ApiResource,
    private apiResourceUtilsService: ApiResourceUtilsService,
    private logger: LoggerService
  ) { }

  createRegistrant(registrant: Registrant): any {
    return registrant;
  }


}
