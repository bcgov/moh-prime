import { Component, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

import { FormUtilsService } from '@core/services/form-utils.service';

@Component({
  selector: 'app-contact-information-form',
  templateUrl: './contact-information-form.component.html',
  styleUrls: ['./contact-information-form.component.scss']
})
export class ContactInformationFormComponent implements OnInit {
  @Input() public form: FormGroup;

  constructor(
    private formUtilsService: FormUtilsService
  ) { }

  public get voicePhone(): FormControl {
    return this.form.get('voicePhone') as FormControl;
  }

  public get voiceExtension(): FormControl {
    return this.form.get('voiceExtension') as FormControl;
  }

  public get contactEmail(): FormControl {
    return this.form.get('contactEmail') as FormControl;
  }

  public get contactPhone(): FormControl {
    return this.form.get('contactPhone') as FormControl;
  }

  public ngOnInit(): void { }
}
