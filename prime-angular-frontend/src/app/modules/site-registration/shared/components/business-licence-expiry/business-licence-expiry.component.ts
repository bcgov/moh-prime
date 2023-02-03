import { Component, Input, OnInit, AfterContentInit, ViewChild } from '@angular/core';
import { Validators, FormGroup } from '@angular/forms';

import { MatSlideToggle, MatSlideToggleChange } from '@angular/material/slide-toggle';

import moment from 'moment';

@Component({
  selector: 'app-business-licence-expiry',
  templateUrl: './business-licence-expiry.component.html',
  styleUrls: ['./business-licence-expiry.component.scss']
})
export class BusinessLicenceExpiryComponent implements OnInit {
  @Input() public form: FormGroup;
  @Input() public showExpiryDate: boolean;

  public minExpiryDate: moment.Moment;

  @ViewChild('expiryDateSlide') public expiryDateSlideToggle: MatSlideToggle;

  constructor() {
    this.minExpiryDate = moment();
  }

  public ngOnInit(): void {
  }

  public onChange(event: MatSlideToggleChange): void {
    this.showExpiryDate = !event.checked
    let expiryDateControl = this.form.get('expiryDate');
    if (event.checked) {
      expiryDateControl.reset();
      expiryDateControl.clearValidators();
      expiryDateControl.updateValueAndValidity();
    } else {
      expiryDateControl.setValidators([Validators.required]);
    }
    this.form.markAsUntouched();
  }
}
