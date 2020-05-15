import { NgModule } from '@angular/core';

import { ConfigService } from './config.service';
import { ConfigCodePipe } from './config-code.pipe';

@NgModule({
  providers: [
    ConfigService,
    ConfigCodePipe
  ],
  declarations: [
    ConfigCodePipe
  ],
  exports: [
    ConfigCodePipe
  ]
})
export class ConfigModule { }
