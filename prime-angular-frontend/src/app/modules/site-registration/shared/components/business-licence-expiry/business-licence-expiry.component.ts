import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

import moment from 'moment';

@Component({
  selector: 'app-business-licence-expiry',
  templateUrl: './business-licence-expiry.component.html',
  styleUrls: ['./business-licence-expiry.component.scss']
})
export class BusinessLicenceExpiryComponent implements OnInit {
  @Input() public form: FormGroup;

  public minExpiryDate: moment.Moment;

  constructor() {
    this.minExpiryDate = moment();
  }

  ngOnInit(): void { }
}
