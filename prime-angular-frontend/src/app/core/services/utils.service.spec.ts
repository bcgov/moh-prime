import { TestBed } from '@angular/core/testing';

import { UtilsService } from './utils.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

interface SortableModel {
  code: number;
  name: string;
  weight: number;
}

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

  it('should sort by a key contained within an object literal', () => {
    const service: UtilsService = TestBed.inject(UtilsService);
    const sortableModels: SortableModel[] = [
      { name: 'b', code: 2, weight: 1 },
      { name: 'a', code: 1, weight: 3 },
      { name: 'c', code: 3, weight: 2 }
    ];

    let results = sortableModels.sort(service.sortByKey<SortableModel>('name'));
    expect(results.map(r => r.code)).toEqual([1, 2, 3]);

    results = results.sort(service.sortByKey<SortableModel>('weight'));
    expect(results.map(r => r.name)).toEqual(['b', 'c', 'a']);
  });
});
