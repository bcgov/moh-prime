import { Component, Inject, OnInit, Output, EventEmitter } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { DialogOptions } from '../../dialog-options.model';
import { SiteResource } from '@core/resources/site-resource.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteListViewModel } from '@registration/shared/models/site.model';
import { Organization } from '@registration/shared/models/organization.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

@Component({
  selector: 'app-link-site',
  templateUrl: './link-site.component.html',
  styleUrls: ['./link-site.component.scss']
})
export class LinkSiteComponent implements OnInit {
  @Output() public linkSite: EventEmitter<boolean>;

  public form: UntypedFormGroup;
  public sites: SiteListViewModel[];
  public organizations: Organization[];
  public saveClick: boolean;
  public removeClick: boolean;
  public siteId: number;
  public orgId: number;
  public orgName: string;
  public preSiteId: number;
  public showOrganization: boolean;
  public organizationSearchClicked: boolean;
  public showSiteList: boolean;
  public showSaveButton: boolean;

  constructor(
    private siteResource: SiteResource,
    private organizationResource: OrganizationResource,
    private fb: UntypedFormBuilder,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
  ) {

    this.linkSite = new EventEmitter<boolean>();
    this.siteId = data.data.siteId;

    if (data.data.organizationId) {
      this.orgId = data.data.organizationId;
    }
    if (data.data.predecessorSiteId) {
      this.preSiteId = data.data.predecessorSiteId
    }
  }

  public get predecessorSiteId(): UntypedFormControl {
    return this.form.get('predecessorSiteId') as UntypedFormControl;
  }

  public get organizationId(): UntypedFormControl {
    return this.form.get('organizationId') as UntypedFormControl;
  }

  public get organizationIdOrName(): UntypedFormControl {
    return this.form.get('organizationIdOrName') as UntypedFormControl;
  }

  public onCancel() {
    this.dialogRef.close();
    this.linkSite.emit(false);
  }

  public onSave() {
    if (this.form.valid) {
      this.saveClick = true;

      this.siteResource.saveSiteLink(this.predecessorSiteId.value, this.siteId)
        .subscribe(() => {
          this.dialogRef.close({ reload: true });
          this.linkSite.emit(true);
        });
    }
  }

  public onRemove() {
    if (this.form.valid) {
      this.removeClick = true;

      this.siteResource.removeSiteLink(this.predecessorSiteId.value, this.siteId)
        .subscribe(() => {
          this.dialogRef.close({ reload: true });
          this.linkSite.emit(true);
        });
    }
  }

  public onRefresh() {
    this.refreshSiteList(this.organizationId.value - 1000);
  }

  public searchOrganization() {
    this.organizationSearchClicked = true;
    this.showSiteList = false;
    this.showSaveButton = false;
    this.organizations = [];
    this.sites = [];

    var value = this.organizationIdOrName.value;

    if (!isNaN(Number(value))) {
      this.organizationResource.getOrganizationById(Number(value) - 1000)
        .subscribe({
          next: (organization) => {
            this.organizations = [organization];
          }
        });
    } else if (value !== '') {
      this.organizationResource.getOrganizationByName(this.organizationIdOrName.value.trim())
        .subscribe(organizations => {
          this.organizations = organizations;
        });
    }
    this.showOrganization = true;
  }

  public changeOrganization() {
    this.showOrganization = false;
    this.organizations = [];
    this.sites = [];
  }

  public ngOnInit(): void {
    this.createFormInstance();

    if (this.orgId) {
      this.organizationIdOrName.setValue(this.orgId + 1000);
      this.showOrganization = true;
      this.organizationResource.getOrganizationById(this.orgId)
        .subscribe(organization => {
          this.organizations = [organization];
        });
      this.organizationId.setValue(this.orgId);
      this.refreshSiteList(this.orgId);
    }
    if (this.preSiteId) {
      this.predecessorSiteId.setValue(this.preSiteId);
      this.showSiteList = true;
    }

    this.organizationId.valueChanges.subscribe(orgId => {
      if (orgId && orgId > 0) {
        this.siteResource.getSites(orgId)
          .subscribe(sites => {
            this.sites = sites.filter(s => s.id !== this.siteId && s.completed &&
              s.careSettingCode == CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE);
            this.showSiteList = true;
          });
      }
    });
    this.predecessorSiteId.valueChanges.subscribe(siteId => {
      if (siteId && siteId > 0) {
        this.showSaveButton = true;
      }
    });
    // To accommodate lengthy instruction text
    this.dialogRef.updateSize('750px', '450px');
    this.saveClick = false;
    this.removeClick = false;
  }

  protected createFormInstance() {
    this.form = this.fb.group({
      organizationIdOrName: [null,],
      organizationId: [null,],
      predecessorSiteId: [null, [Validators.required]],
    });
  }

  private refreshSiteList(organizationId: number): void {
    this.showOrganization = false;
    this.siteResource.getSites(organizationId)
      .subscribe(sites => {
        this.sites = sites.filter(s => s.id !== this.siteId)
        this.showSaveButton = true;
      });
    this.organizationResource.getOrganizationById(organizationId)
      .subscribe(org => {
        this.orgName = org.name;
        this.showOrganization = true;
      });
  }
}
