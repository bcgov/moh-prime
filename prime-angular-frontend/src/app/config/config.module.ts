import { NgModule, APP_INITIALIZER } from '@angular/core';

import { ConfigService } from './config.service';
import { ConfigCodePipe } from './config-code.pipe';

const initializer = (config: ConfigService) => {
  return () => config.load().toPromise();
};

@NgModule({
  providers: [
    ConfigService,
    // TODO: keep initializer until testing completed using resolver
    // {
    //   provide: APP_INITIALIZER,
    //   useFactory: initializer,
    //   multi: true,
    //   deps: [ConfigService]
    // }
  ],
  declarations: [
    ConfigCodePipe
  ],
  exports: [
    ConfigCodePipe
  ]
})
export class ConfigModule { }
