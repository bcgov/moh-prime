import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { environment } from '@env/environment';
import { ConfigMap } from '@env/config-map.model';

import { AppModule } from './app/app.module';
import { APP_CONFIG, AppConfig, APP_DI_CONFIG } from './app/app-config.module';

// The deployment pipeline provides the config map based on environment
// without requiring a build by using the public assets folder, otherwise
// config map should not exist in local development which relies on the
// cascade of environment files.
fetch('/assets/config-map.json')
  .then((response) => response.json())
  .then((configMap: ConfigMap) => {
    let appConfig = APP_DI_CONFIG;

    if (configMap) {
      // TODO mohKeyCloakConfig will eventually have configuration applied throughout the environments
      const { keycloakConfig: { config }, ...root } = configMap;
      appConfig = { ...appConfig, ...root };
      appConfig.keycloakConfig.config = config;
    }

    return appConfig;
  })
  .catch(() => APP_DI_CONFIG)
  .then((appConfig: AppConfig) => {
    if (environment.production) {
      enableProdMode();
    }

    platformBrowserDynamic([
      { provide: APP_CONFIG, useValue: appConfig }
    ]).bootstrapModule(AppModule)
      .catch(err => console.error(err));
  });
