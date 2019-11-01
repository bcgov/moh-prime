import { Pipe, PipeTransform } from '@angular/core';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';

@Pipe({
  name: 'configCode'
})
export class ConfigCodePipe implements PipeTransform {
  constructor(
    private config: ConfigService
  ) { }

  public transform<T>(code: T, configKey: string, key: string = 'name'): string {
    return this.config[configKey].find((c: Config<T>) => c.code === code)[key];
  }
}
