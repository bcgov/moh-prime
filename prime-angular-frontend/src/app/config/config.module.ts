import { APP_INITIALIZER, NgModule } from '@angular/core';

import { Configuration } from './config.model';
import { ConfigService } from './config.service';
import { ConfigCodePipe } from './config-code.pipe';

function configFactory(configService: ConfigService): () => Promise<Configuration> {
  // Ensure configuration is populated before the application
  // is fully initialized to prevent race conditions
  return async () => await configService.load().toPromise();
}

@NgModule({
  declarations: [
    ConfigCodePipe
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: configFactory,
      multi: true,
      deps: [ConfigService]
    },
    ConfigCodePipe
  ],
  exports: [
    ConfigCodePipe
  ]
})
export class ConfigModule {}
