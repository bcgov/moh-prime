import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { FormUtilsService } from '@core/services/form-utils.service';

@Component({
  selector: 'app-form-errors',
  templateUrl: './form-errors.component.html',
  styleUrls: ['./form-errors.component.scss']
})
export class FormErrorsComponent implements OnInit {
  @Input() form: FormGroup;

  constructor(
    private formUtilsService: FormUtilsService
  ) { }

  public get errors(): { [key: string]: any } {
    return this.formUtilsService.getFormErrors(this.form);
  }

  public ngOnInit(): void { }
}
