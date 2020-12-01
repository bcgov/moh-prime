import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';

import { FormUtilsService } from '@core/services/form-utils.service';

@Component({
  selector: 'app-contact-information-form',
  templateUrl: './contact-information-form.component.html',
  styleUrls: ['./contact-information-form.component.scss']
})
export class ContactInformationFormComponent implements OnInit {
  @Input() public form: FormGroup;
  @Input() public showSmsPhone: boolean;

  constructor() {
    // Defaults
    this.showSmsPhone = true;
  }

  public get phone(): FormControl {
    return this.form.get('phone') as FormControl;
  }

  public get phoneExtension(): FormControl {
    return this.form.get('phoneExtension') as FormControl;
  }

  public get email(): FormControl {
    return this.form.get('email') as FormControl;
  }

  public get smsPhone(): FormControl {
    return this.form.get('smsPhone') as FormControl;
  }

  public ngOnInit(): void { }
}
