import { Component, Input, OnInit } from '@angular/core';
import { HealthAuthoritySiteAdmin } from '@health-auth/shared/models/health-authority-admin-site.model';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { Contact } from '@lib/models/contact.model';

@Component({
  selector: 'app-health-auth-site-overview-container',
  templateUrl: './health-auth-site-overview-container.component.html',
  styleUrls: ['./health-auth-site-overview-container.component.scss']
})
export class HealthAuthSiteOverviewContainerComponent implements OnInit {
  @Input() public healthAuthoritySite: HealthAuthoritySite | HealthAuthoritySiteAdmin;
  @Input() public pharmanetAdministrators: Contact[];
  @Input() public technicalSupports: Contact[];

  public isIncomplete: boolean;
  public isApproved: boolean;

  public get pharmanetAdministratorName(): string {
    return (this.healthAuthoritySite as HealthAuthoritySiteAdmin)?.pharmanetAdministratorName;
  }

  public get technicalSupportName(): string {
    return (this.healthAuthoritySite as HealthAuthoritySiteAdmin)?.technicalSupportName;
  }

  constructor() {
    this.pharmanetAdministrators = [];
    this.technicalSupports = [];
  }

  ngOnInit(): void {
    this.isIncomplete = this.healthAuthoritySite?.isIncomplete();
    this.isApproved = this.healthAuthoritySite?.isApproved();
  }

}
