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
  // Organization Types instead of Organizations are sent from adjudicator link certificate so that a lookup table isnt needed
  @Input() public organizationTypes: number[];

  constructor() { }

  public get hasOrganization(): boolean {
    return !!(this.organizations.length || this.organizationTypes.length);
  }

  public ngOnInit() { }
}
