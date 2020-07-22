import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable } from 'rxjs';

import { OrganizationResource } from '@core/resources/organization-resource.service';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { OrganizationTypeEnum } from '@shared/enums/organization-type.enum';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { IForm } from '@registration/shared/interfaces/form.interface';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { OrganizationService } from '@registration/shared/services/organization.service';

@Component({
  selector: 'app-care-setting',
  templateUrl: './care-setting.component.html',
  styleUrls: ['./care-setting.component.scss']
})
export class CareSettingComponent implements OnInit, IPage, IForm {
  public busy: Subscription;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
  public organization: string;
  public doingBusinessAsNames: string[];
  public organizationTypes: Config<number>[];
  public filteredOrganizationTypes: Config<number>[];
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private organizationService: OrganizationService,
    private organizationResource: OrganizationResource,
    private organizationFormStateService: OrganizationFormStateService,
    private formUtilsService: FormUtilsService,
    private dialog: MatDialog,
    private configService: ConfigService,
  ) {
    this.title = 'Care Setting';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.organizationTypes = this.configService.organizationTypes;
  }

  public get organizationTypeCode(): FormControl {
    return this.form.get('organizationTypeCode') as FormControl;
  }

  public disableOrganization(organizationTypeCode: number): boolean {
    // Omit organizations types that are not "Community Practices" for ComPap
    return (organizationTypeCode !== OrganizationTypeEnum.COMMUNITY_PRACTICE);
  }

  public onSubmit() {
    // TODO structured to match in all organization views
    if (this.formUtilsService.checkValidity(this.form)) {
      // TODO when spoking don't update
      const payload = this.organizationFormStateService.organization;
      this.organizationResource
        .updateOrganization(payload, true)
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    }
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATION_INFORMATION);
  }

  public nextRoute() {
    this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATION_REVIEW);
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.organizationFormStateService.organizationTypeForm;
  }

  private initForm() {
    // TODO structured to match in all site views
    const organization = this.organizationService.organization;
    this.isCompleted = organization?.completed;
    this.organizationFormStateService.setForm(organization);
  }
}
