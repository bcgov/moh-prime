import { APP_INITIALIZER, Injector, NgModule } from '@angular/core';

import { ConfigService } from './config.service';
import { ConfigCodePipe } from './config-code.pipe';

function configFactory(injector: Injector): () => Promise<void> {
  return async (): Promise<void> => {
    // Ensure configuration is populated before the application
    // is fully initialized to prevent race conditions
    await injector.get(ConfigService).load().toPromise();
  };
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
      deps: [Injector]
    },
    ConfigCodePipe
  ],
  exports: [
    ConfigCodePipe
  ]
})
export class ConfigModule {}
