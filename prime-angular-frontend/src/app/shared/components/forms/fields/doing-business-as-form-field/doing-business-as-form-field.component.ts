import { Component, Input, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';

import { Subscription } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { OrganizationResource } from '@core/resources/organization-resource.service';

import { Organization } from '@registration/shared/models/organization.model';
import { OrgBookResource } from '@registration/shared/services/org-book-resource.service';

@Component({
  selector: 'app-doing-business-as-form-field',
  templateUrl: './doing-business-as-form-field.component.html',
  styleUrls: ['./doing-business-as-form-field.component.scss']
})
export class DoingBusinessAsFormFieldComponent implements OnInit {
  @Input() public organizationId?: number;
  @Input() public label: string;
  @Input() public doingBusinessAs: FormControl;

  public busy: Subscription;
  public doingBusinessAsNames: string[];

  constructor(
    private organizationResource: OrganizationResource,
    private orgBookResource: OrgBookResource
  ) {
    this.label = 'Site Name';
    this.doingBusinessAsNames = [];
  }

  public ngOnInit(): void {
    this.getDoingBusinessAs(this.organizationId);
  }

  /**
   * @description
   * Get "Doing Business As" from OrgBook based on
   * the organization ID.
   */
  private getDoingBusinessAs(organizationId: number) {
    if (!organizationId) {
      return;
    }

    this.busy = this.organizationResource.getOrganizationById(organizationId)
      .pipe(
        map((organization: Organization) => organization.registrationId),
        this.orgBookResource.doingBusinessAsMap(),
        tap((doingBusinessAsNames: string[]) =>
          this.doingBusinessAsNames = doingBusinessAsNames
        )
      )
      .subscribe();
  }
}
