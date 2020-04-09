import { Component, OnInit, Input } from '@angular/core';

import { Config } from '@config/config.model';
import { ConfigCodePipe } from '@config/config-code.pipe';

import { Organization } from '@enrolment/shared/models/organization.model';

@Component({
  selector: 'app-enrollee-organizations',
  templateUrl: './enrollee-organizations.component.html',
  styleUrls: ['./enrollee-organizations.component.scss']
})
export class EnrolleeOrganizationsComponent implements OnInit {
  @Input() public header: string;
  @Input() public organizations: Organization[];
  // OrganizationTypes are sent instead of Organizations for a
  // certificate so the lookup table isn't required
  @Input() public organizationTypes: Config<number>[];

  constructor(
    private configPipe: ConfigCodePipe
  ) {
    this.organizations = [];
    this.organizationTypes = [];
  }

  public get hasOrganization(): boolean {
    return !![...this.organizations, ...this.organizationTypes].length;
  }

  public get normalizedOrganizationTypes(): string[] {
    let organizationTypes = [];

    if (this.organizations.length) {
      // Convert the lookup organization type codes
      organizationTypes = this.organizations
        .map(o => this.configPipe.transform(o.organizationTypeCode, 'organizationTypes'));
    } else if (this.organizationTypes.length) {
      // Directly use the organization types
      organizationTypes = this.organizationTypes
        .map(o => o.name);
    }

    return (organizationTypes.length)
      ? organizationTypes
      : [];
  }

  public ngOnInit() { }
}
