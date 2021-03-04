import { TestBed } from '@angular/core/testing';

import { UtilsService } from './utils.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { Config, IWeightedConfig } from '@config/config.model';

describe('UtilsService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    providers: [
      {
        provide: APP_CONFIG,
        useValue: APP_DI_CONFIG
      }
    ]
  }));

  it('should create', () => {
    const service: UtilsService = TestBed.inject(UtilsService);
    expect(service).toBeTruthy();
  });

  it('should sortByKey', () => {
    const service: UtilsService = TestBed.inject(UtilsService);
    const configs: Config<number>[] = [
      { name: 'b', code: 2 },
      { name: 'a', code: 1 },
      { name: 'c', code: 3 },
    ];
    const result = configs.sort(service.sortByKey<Config<number>>('name'));
    expect(result[0].name).toBe('a');
    expect(result[2].code).toBe(3);

    const weightedConfigs: IWeightedConfig[] = [
      { weight: 2 },
      { weight: 1 },
      { weight: 3 },
    ];
    expect(weightedConfigs.sort(service.sortByKey<IWeightedConfig>('weight'))[0].weight).toBe(1);
  });
});
