import { AbstractControl } from '@angular/forms';

import { Observable } from 'rxjs';

import { IForm } from '@lib/classes/form.interface';

export abstract class IFormPage implements IForm {
  public form: AbstractControl;
  public abstract onSubmit(): void;
  public canDeactivate(): Observable<boolean> | boolean {
    return true;
  }
}
