import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { ContactFormState } from './contact-form-state.class';

describe('ContactFormState', () => {
  let contactFormState: ContactFormState;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    });

    contactFormState = TestBed.inject(ContactFormState);
  });

  it('should create an instance', () => {
    expect(contactFormState).toBeTruthy();
  });
});
