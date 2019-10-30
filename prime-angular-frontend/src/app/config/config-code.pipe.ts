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

  transform(code: number, configKey: string, key: string = 'name'): string {
    return this.config[configKey].find((c: Config) => c.code === code)[key];
  }
}
