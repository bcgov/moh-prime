import { Component, OnInit, ChangeDetectionStrategy, Input } from '@angular/core';
import { Privilege } from '@enrolment/shared/models/privilege.model';

@Component({
  selector: 'app-enrollee-privileges',
  templateUrl: './enrollee-privileges.component.html',
  styleUrls: ['./enrollee-privileges.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EnrolleePrivilegesComponent implements OnInit {
  @Input() public header: string;
  @Input() public privileges: Privilege[];

  constructor() { }

  ngOnInit() { }

}



