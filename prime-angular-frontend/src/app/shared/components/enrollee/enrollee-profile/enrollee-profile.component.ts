import { Component, Input, ChangeDetectionStrategy } from '@angular/core';

import { Enrollee } from '@shared/models/enrollee.model';

@Component({
  selector: 'app-enrollee-profile',
  templateUrl: './enrollee-profile.component.html',
  styleUrls: ['./enrollee-profile.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EnrolleeProfileComponent {
  @Input() public enrollee: Enrollee;
  @Input() public showPersonal: boolean;
  @Input() public showAddress: boolean;
  @Input() public showAddressTitle: boolean;

  constructor() {
    this.showPersonal = true;
    this.showAddress = true;
    this.showAddressTitle = true;
  }

  public get physicalAddress() {
    return (this.enrollee && this.enrollee.physicalAddress)
      ? this.enrollee.physicalAddress
      : null;
  }
}
