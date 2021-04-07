import { Component, Input, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { Subscription } from 'rxjs';
import { map, tap } from 'rxjs/operators';

import { OrganizationResource } from '@core/resources/organization-resource.service';
import { OrgBookResource } from '@registration/shared/services/org-book-resource.service';

import { Organization } from '@registration/shared/models/organization.model';

@Component({
  selector: 'app-site-information-form',
  templateUrl: './site-information-form.component.html',
  styleUrls: ['./site-information-form.component.scss']
})
export class SiteInformationFormComponent implements OnInit {
  @Input() public form: FormGroup;
  @Input() public organizationId?: number;

  public busy: Subscription;
  public doingBusinessAsNames: string[];
  public isNewSite: FormControl;

  constructor(
    private organizationResource: OrganizationResource,
    private orgBookResource: OrgBookResource,
  ) {
    this.doingBusinessAsNames = [];
    this.isNewSite = new FormControl(false);
  }

  public get doingBusinessAs(): FormControl {
    return this.form.get('doingBusinessAs') as FormControl;
  }

  public get pec(): FormControl {
    return this.form.get('pec') as FormControl;
  }

  public ngOnInit(): void {
    this.initForm();
  }

  protected initForm(): void {
    if (this.organizationId) {
      this.getDoingBusinessAs(this.organizationId);
    }

    this.isNewSite.valueChanges
      .subscribe(value => {
        if (value) {
          this.pec.patchValue(null);
          this.pec.disable();
        }
        else {
          this.pec.enable();
        }
      });

    this.isNewSite.setValue(this.pec.disabled);
  }

  private getDoingBusinessAs(organizationId: number) {
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
