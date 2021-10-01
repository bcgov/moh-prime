import { AsyncValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

/**
 * @description
 * Check that a value is unique.
 */
export function uniqueAsync(request: (value: string) => Observable<boolean>, errorKey = 'unique'): AsyncValidatorFn {
  return (control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
    return request(control.value)
      .pipe(
        map((result: boolean) => (result) ? { [errorKey]: result } : null)
      );
  };
}
