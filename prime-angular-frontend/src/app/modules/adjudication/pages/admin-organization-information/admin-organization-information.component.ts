import { Component, OnInit } from '@angular/core';
import { Subscription, BehaviorSubject, Observable } from 'rxjs';
import { FormGroup } from '@angular/forms';
import { Organization } from '@registration/shared/models/organization.model';
import { ActivatedRoute, Router } from '@angular/router';
import { OrganizationResource } from '@core/resources/organization-resource.service';

@Component({
  selector: 'app-admin-organization-information',
  templateUrl: './admin-organization-information.component.html',
  styleUrls: ['./admin-organization-information.component.scss']
})
export class AdminOrganizationInformationComponent implements OnInit {
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

  ngOnInit(): void {
    this.busy = this.getOrganization()
      .subscribe((organization: Organization) => {
        console.log(organization);
        this.organization = organization;
      });
  }

  private getOrganization(): Observable<Organization> {
    const organizationId = this.route.snapshot.params.oid;
    return this.organizationResource.getOrganizationById(organizationId);
  }

}
