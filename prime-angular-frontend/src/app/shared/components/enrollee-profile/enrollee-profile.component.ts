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
  @Input() public showPreferredName: boolean;

  constructor() {
    this.showPreferredName = true;
  }

  public get fullname(): string {
    return (this.enrollee)
      ? `${this.enrollee.firstName} ${this.enrollee.middleName} ${this.enrollee.lastName}`
      : '';
  }

  public get hasPreferredName(): boolean {
    return (
      this.enrollee &&
      (
        !!this.enrollee.preferredFirstName ||
        !!this.enrollee.preferredMiddleName ||
        !!this.enrollee.preferredLastName
      )
    );
  }
}
