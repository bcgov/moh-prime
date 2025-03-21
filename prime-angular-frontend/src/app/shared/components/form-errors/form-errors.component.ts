import { Component, OnInit, Input } from '@angular/core';
import { UntypedFormGroup } from '@angular/forms';

import { FormUtilsService } from '@core/services/form-utils.service';

/**
 * @description
 * Debug form errors visually in the template.
 */
@Component({
  selector: 'app-form-errors',
  templateUrl: './form-errors.component.html',
  styleUrls: ['./form-errors.component.scss']
})
export class FormErrorsComponent implements OnInit {
  @Input() form: UntypedFormGroup;

  constructor(
    private formUtilsService: FormUtilsService
  ) { }

  public get errors(): { [key: string]: any } {
    return this.formUtilsService.getFormErrors(this.form);
  }

  public ngOnInit(): void { }
}
