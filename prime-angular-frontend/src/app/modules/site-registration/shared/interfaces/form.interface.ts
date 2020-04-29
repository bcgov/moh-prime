import { FormGroup } from '@angular/forms';

import { Observable } from 'rxjs';

export interface IForm {
  form: FormGroup;
  onSubmit(): void;
  canDeactivate(): Observable<boolean> | boolean;
}
