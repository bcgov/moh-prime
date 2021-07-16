import { enableProdMode } from '@angular/core';
import { platformBrowserDynamic } from '@angular/platform-browser-dynamic';

import { environment } from '@env/environment';

import { AppModule } from './app/app.module';
import { APP_CONFIG, AppConfig, ConfigMap, defaultAppConfig } from './app/app-config.module';

fetch('/assets/config-map-test.json')
  .then((response) => response.json())
  .then((configMap: ConfigMap) => {
    let appConfig = defaultAppConfig;

    if (configMap) {
      const { keycloakConfig: { config }, ...root } = configMap;
      appConfig = { ...appConfig, ...root };
      appConfig.keycloakConfig.config = config;
    }

    return appConfig;
  })
  .catch(() => defaultAppConfig)
  .then((appConfig: AppConfig) => {
    if (environment.production) {
      enableProdMode();
    }

    platformBrowserDynamic([
      { provide: APP_CONFIG, useValue: appConfig }
    ]).bootstrapModule(AppModule)
      .catch(err => console.error(err));
  });
