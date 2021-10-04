import { AsyncValidatorFn, AbstractControl, ValidationErrors } from '@angular/forms';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

/**
 * @description
 * Customizable async validator.
 */
export function asyncValidator(request: (value: string) => Observable<boolean>, errorKey: string): AsyncValidatorFn {
  return (control: AbstractControl): Promise<ValidationErrors | null> | Observable<ValidationErrors | null> => {
    return request(control.value)
      .pipe(
        map((result: boolean) => (!result) ? { [errorKey]: result } : null)
      );
  };
}
