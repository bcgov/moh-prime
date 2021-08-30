import { Injectable } from '@angular/core';
import { AbstractControl, AsyncValidator, ValidationErrors } from '@angular/forms';

import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { SiteResource } from '@core/resources/site-resource.service';

@Injectable({ providedIn: 'root' })
export class UniquePecValidator implements AsyncValidator {
  constructor(private siteResource: SiteResource) { }

  public validate(control: AbstractControl): Promise<ValidationErrors> | Observable<ValidationErrors> {
    return this.siteResource.pecExists(control.value)
      .pipe(
        map(pecExists => pecExists ? { pecExists } : null)
      );
  }
}
