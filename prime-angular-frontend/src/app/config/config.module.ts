import { NgModule } from '@angular/core';

import { ConfigCodePipe } from './config-code.pipe';

@NgModule({
  declarations: [
    ConfigCodePipe
  ],
  providers: [
    ConfigCodePipe
  ],
  exports: [
    ConfigCodePipe
  ]
})
export class ConfigModule { }
