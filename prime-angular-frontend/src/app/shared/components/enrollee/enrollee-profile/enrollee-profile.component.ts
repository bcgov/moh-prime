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
  @Input() public title: string;

  constructor() {
    this.title = 'Personal Information from BC Services Card';
  }
}
