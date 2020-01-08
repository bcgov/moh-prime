import { Component, OnInit, Input } from '@angular/core';
import { Organization } from '@enrolment/shared/models/organization.model';

@Component({
  selector: 'app-enrollee-organizations',
  templateUrl: './enrollee-organizations.component.html',
  styleUrls: ['./enrollee-organizations.component.scss']
})
export class EnrolleeOrganizationsComponent implements OnInit {
  @Input() public header: string;
  @Input() public organizations: Organization[];
  @Input() public organizationTypes: [];
  constructor() { }

  ngOnInit() {
  }

}
