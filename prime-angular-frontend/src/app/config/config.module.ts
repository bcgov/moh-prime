import { NgModule, APP_INITIALIZER } from '@angular/core';

import { ConfigService } from './config.service';

const initializer = (config: ConfigService) => {
  return () => config.load();
};

@NgModule({
  providers: [
    ConfigService,
    {
      provide: APP_INITIALIZER,
      useFactory: initializer,
      multi: true,
      deps: [ConfigService]
    }
  ]
})
export class ConfigModule { }
