import { TestBed } from '@angular/core/testing';

import { AddressAutocompleteResource } from './address-autocomplete-resource.service';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { MatSnackBarModule } from '@angular/material/snack-bar';

describe('AddressAutocompleteResource', () => {
  let service: AddressAutocompleteResource;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        MatSnackBarModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
      ]
    });
    service = TestBed.inject(AddressAutocompleteResource);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
