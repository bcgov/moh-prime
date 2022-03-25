import { TestBed } from '@angular/core/testing';
import { ReactiveFormsModule } from '@angular/forms';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { TechnicalSupportsFormState } from './technical-supports-form-state';

/**
 * Based on src\app\lib\classes\contact-form-state.class.spec.ts
 */
describe('TechnicalSupportsFormState', () => {
  let technicalSupportsFormState: TechnicalSupportsFormState;

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

    technicalSupportsFormState = TestBed.inject(TechnicalSupportsFormState);
  });

  it('should create an instance', () => {
    expect(technicalSupportsFormState).toBeTruthy();
  });
});
