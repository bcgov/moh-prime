import { Component, Input, OnInit } from '@angular/core';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-business-licence-expiry',
  templateUrl: './business-licence-expiry.component.html',
  styleUrls: ['./business-licence-expiry.component.scss']
})
export class BusinessLicenceExpiryComponent implements OnInit {
  @Input() public form: FormGroup;

  ngOnInit(): void { }
}
