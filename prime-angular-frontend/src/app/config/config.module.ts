import { NgModule, APP_INITIALIZER } from '@angular/core';

import { ConfigService } from './config.service';
import { ConfigCodePipe } from './config-code.pipe';

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
  ],
  declarations: [
    ConfigCodePipe
  ],
  exports: [
    ConfigCodePipe
  ]
})
export class ConfigModule { }
