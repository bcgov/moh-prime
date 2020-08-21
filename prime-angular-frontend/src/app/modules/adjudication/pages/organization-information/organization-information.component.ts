import { Component, OnInit } from '@angular/core';
import { Subscription, BehaviorSubject, Observable } from 'rxjs';
import { Organization } from '@registration/shared/models/organization.model';
import { ActivatedRoute, Router } from '@angular/router';
import { OrganizationResource } from '@core/resources/organization-resource.service';

@Component({
  selector: 'app-organization-information',
  templateUrl: './organization-information.component.html',
  styleUrls: ['./organization-information.component.scss']
})
export class OrganizationInformationComponent implements OnInit {
  public busy: Subscription;
  public hasActions: boolean;
  public refresh: BehaviorSubject<boolean>;

  public organization: Organization;

  constructor(
    private route: ActivatedRoute,
    private organizationResource: OrganizationResource
  ) {
    this.hasActions = true;
  }

  public ngOnInit(): void {
    this.busy = this.getOrganization()
      .subscribe((organization: Organization) => {
        this.organization = organization;
      });
  }

  private getOrganization(): Observable<Organization> {
    const organizationId = this.route.snapshot.params.oid;
    return this.organizationResource.getOrganizationById(organizationId);
  }
}
