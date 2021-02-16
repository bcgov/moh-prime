import { ChangeDetectionStrategy, Component, Input } from '@angular/core';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';

@Component({
  selector: 'app-bcsc-profile',
  templateUrl: './bcsc-profile.component.html',
  styleUrls: ['./bcsc-profile.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class BcscProfileComponent {
  @Input() public user: BcscUser;
  @Input() public showPersonal: boolean;
  @Input() public showAddress: boolean;
  @Input() public showAddressTitle: boolean;

  constructor() {
    this.showPersonal = true;
    this.showAddress = true;
    this.showAddressTitle = true;
  }

  public get verifiedAddress() {
    return (this.user && this.user.verifiedAddress)
      ? this.user.verifiedAddress
      : null;
  }
}
