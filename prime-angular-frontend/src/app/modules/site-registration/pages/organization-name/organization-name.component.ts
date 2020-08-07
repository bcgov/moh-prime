import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable, of } from 'rxjs';
import { debounceTime, switchMap, tap, exhaustMap } from 'rxjs/operators';

import { FormUtilsService } from '@core/services/form-utils.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { UtilsService, SortWeight } from '@core/services/utils.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { IForm } from '@registration/shared/interfaces/form.interface';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { OrgBookResource, OrgBookAutocompleteResult } from '@registration/shared/services/org-book-resource.service';
import { Site } from '@registration/shared/models/site.model';

@Component({
  selector: 'app-organization-name',
  templateUrl: './organization-name.component.html',
  styleUrls: ['./organization-name.component.scss']
})
export class OrganizationNameComponent implements OnInit, IPage, IForm {
  public busy: Subscription;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
  public organizations: string[];
  public doingBusinessAsNames: string[];
  public isCompleted: boolean;
  public usedOrgBook: boolean;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private organizationService: OrganizationService,
    private organizationResource: OrganizationResource,
    private organizationFormStateService: OrganizationFormStateService,
    private orgBookResource: OrgBookResource,
    private siteResource: SiteResource,
    private formUtilsService: FormUtilsService,
    private utilsService: UtilsService,
    private dialog: MatDialog
  ) {
    this.title = 'Organization Name';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public get name(): FormControl {
    return this.form.get('name') as FormControl;
  }

  public get registrationId(): FormControl {
    return this.form.get('registrationId') as FormControl;
  }

  public get doingBusinessAs(): FormControl {
    return this.form.get('doingBusinessAs') as FormControl;
  }

  public getOrgBookLink(orgId: string) {
    return `https://www.orgbook.gov.bc.ca/en/organization/${orgId}`;
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const organizationId = this.route.snapshot.params.oid;
      const payload = this.organizationFormStateService.json;
      this.organizationResource
        .updateOrganization(payload, true)
        .pipe(
          exhaustMap(
            // Check the organization wasn't completed before the update, and
            // if not then this is the initial registration wizard
            () => (!this.isCompleted)
              ? this.siteResource.createSite(organizationId)
              : of(null)
          )
        )
        .subscribe((site: Site) => {
          this.form.markAsPristine();
          this.nextRoute(site?.id);
        });
    }
  }

  public onSelect({ option }: MatAutocompleteSelectedEvent) {
    const orgName = option.value;
    this.orgBookResource.getOrganizationFacet(orgName)
      .pipe(
        this.orgBookResource.sourceIdMap(),
        tap((sourceId: string) => this.usedOrgBook = true),
        tap((sourceId: string) => this.form.get('registrationId').patchValue(sourceId)),
        this.orgBookResource.doingBusinessAsMap()
      )
      .subscribe((doingBusinessAsNames: string[]) =>
        this.doingBusinessAsNames = doingBusinessAsNames
      );
  }

  public onInput() {
    this.usedOrgBook = false;
    this.registrationId.reset();
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY);
  }

  public nextRoute(siteId: number) {
    const redirectPath = this.route.snapshot.queryParams.redirect;
    if (redirectPath) {
      this.routeUtils.routeRelativeTo([redirectPath, SiteRoutes.SITE_REVIEW]);
    } else {
      if (this.isCompleted) {
        this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATION_REVIEW);
      } else {
        this.routeUtils.routeRelativeTo([SiteRoutes.SITES, siteId, SiteRoutes.CARE_SETTING]);
      }
    }
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
    this.form = this.organizationFormStateService.organizationNameForm;
  }

  private initForm() {
    const organization = this.organizationService.organization;
    this.isCompleted = organization?.completed;
    this.organizationFormStateService.setForm(organization, true);

    this.name.valueChanges
      .pipe(
        debounceTime(400),
        switchMap((value: string) => this.orgBookResource.autocomplete(value))
      )
      .subscribe((organizations: OrgBookAutocompleteResult[]) => {
        // Assumed only a single name per organization is relavent
        this.organizations = organizations.map(o => o.names[0].text);
      });
  }

  /**
   * @description
   * Sort by day of the week.
   */
  private sortDoingBusinessAsNames(): (a: string, b: string) => SortWeight {
    return (a: string, b: string) =>
      this.utilsService.sort<string>(a, b);
  }
}
