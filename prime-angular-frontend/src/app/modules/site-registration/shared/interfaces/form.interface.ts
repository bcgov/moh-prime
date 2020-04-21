import { FormGroup } from '@angular/forms';

import { Observable } from 'rxjs';

export interface IForm {
  form: FormGroup;
  onSubmit(data?: any): void;
  canDeactivate(): Observable<boolean> | boolean;
}
