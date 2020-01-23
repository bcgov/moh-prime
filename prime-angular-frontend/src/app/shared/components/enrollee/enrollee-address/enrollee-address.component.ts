import { Component, Input, ChangeDetectionStrategy } from '@angular/core';

import { Address } from '@enrolment/shared/models/address.model';

@Component({
  selector: 'app-enrollee-address',
  templateUrl: './enrollee-address.component.html',
  styleUrls: ['./enrollee-address.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EnrolleeAddressComponent {
  @Input() public address: Address;
  @Input() public showHeader: boolean;

  constructor() {
    this.showHeader = true;
  }
}
