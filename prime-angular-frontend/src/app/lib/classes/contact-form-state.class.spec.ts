import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { ContactFormState } from './contact-form-state.class';

describe('ContactFormState', () => {
  let contactFormState: ContactFormState;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [
        ReactiveFormsModule,
      ],
    });

    contactFormState = TestBed.inject(ContactFormState);
  });

  it('should create an instance', () => {
    expect(contactFormState).toBeTruthy();
  });
});
