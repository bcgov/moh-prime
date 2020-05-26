import { AbstractControl } from '@angular/forms';

import { Observable } from 'rxjs';

export abstract class IForm {
  public form: AbstractControl;
  public abstract onSubmit(): void;
  public canDeactivate(): Observable<boolean> | boolean {
    return true;
  }
}
