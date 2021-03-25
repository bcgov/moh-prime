import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatDialog } from '@angular/material/dialog';

import { Observable, of, pipe } from 'rxjs';
import { debounceTime, switchMap, tap, exhaustMap, take, map } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { OrgBookAutocompleteHttpResponse } from '@registration/shared/models/orgbook.model';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { OrgBookResource } from '@registration/shared/services/org-book-resource.service';
import { OrganizationNamePageFormState } from './organization-name-page-form-state.class';

@Component({
  selector: 'app-organization-name-page',
  templateUrl: './organization-name-page.component.html',
  styleUrls: ['./organization-name-page.component.scss']
})
export class OrganizationNamePageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: OrganizationNamePageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public organizations: string[];
  public totalResults: number;
  public doingBusinessAsNames: string[];
  public isCompleted: boolean;
  public usedOrgBook: boolean;
  public SiteRoutes = SiteRoutes;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private organizationService: OrganizationService,
    private organizationResource: OrganizationResource,
    private organizationFormStateService: OrganizationFormStateService,
    private orgBookResource: OrgBookResource,
    private siteResource: SiteResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public getOrgBookLink(orgId: string, display: boolean = false) {
    const url = 'https://www.orgbook.gov.bc.ca/en/organization';
    return (display)
      ? `${ url }/${ orgId }`
      : `${ url }/registration.registries.ca/${ orgId }`;
  }

  public onSelect({ option }: MatAutocompleteSelectedEvent) {
    const orgName = option.value;
    this.orgBookResource.getOrganizationFacet(orgName)
      .pipe(
        this.orgBookResource.sourceIdMap(),
        tap((sourceId: string) => this.usedOrgBook = true),
        tap((sourceId: string) => this.formState.form.get('registrationId').patchValue(sourceId)),
        this.getDoingBusinessAs()
      )
      .subscribe();
  }

  public onInput() {
    this.usedOrgBook = false;
    this.formState.registrationId.reset();
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATION_SIGNING_AUTHORITY);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm();
    this.initForm();
  }

  protected createFormInstance() {
    this.formState = this.organizationFormStateService.organizationNamePageFormState;
  }

  protected patchForm(): void {
    const organization = this.organizationService.organization;
    this.isCompleted = organization?.completed;
    this.organizationFormStateService.setForm(organization, true);
    this.formState.form.markAsPristine();

    this.usedOrgBook = !!organization?.registrationId;

    if (organization.registrationId) {
      this.busy = of(organization.registrationId)
        .pipe(take(1), this.getDoingBusinessAs())
        .subscribe();
    }
  }

  protected initForm() {
    this.formState.name.valueChanges
      .pipe(
        debounceTime(400),
        switchMap((value: string) => this.orgBookResource.autocomplete(value))
      )
      .subscribe((response: OrgBookAutocompleteHttpResponse) => {
        // Assumed only a single name per organization is relavent
        this.organizations = response.results.map(o => o.names[0]?.text).filter(o => o);
        this.totalResults = response.total;
      });
  }

  protected performSubmission(): Observable<number | null> {
    const organizationId = this.route.snapshot.params.oid;
    const payload = this.organizationFormStateService.json;
    return this.organizationResource
      .updateOrganization(payload)
      .pipe(
        exhaustMap(() =>
          this.organizationResource.updateCompleted(organizationId)
        ),
        exhaustMap(
          // Check the organization wasn't completed before the update, and
          // if not then this is the initial registration wizard
          () => (!this.isCompleted)
            ? this.siteResource.createSite(organizationId)
            : of(null)
        ),
        map((site: Site) => site?.id)
      );
  }

  protected afterSubmitIsSuccessful(siteId?: number): void {
    this.formState.form.markAsPristine();

    const redirectPath = this.route.snapshot.queryParams.redirect;
    let routePath: string | string[];

    if (redirectPath) {
      routePath = [redirectPath, SiteRoutes.SITE_REVIEW];
    } else {
      routePath = (!this.isCompleted)
        ? [SiteRoutes.SITES, `${ siteId }`, SiteRoutes.CARE_SETTING]
        : SiteRoutes.ORGANIZATION_REVIEW;
    }

    this.routeUtils.routeRelativeTo(routePath);
  }

  private getDoingBusinessAs() {
    return pipe(
      // Expects an organization registrationId
      this.orgBookResource.doingBusinessAsMap(),
      tap((doingBusinessAsNames: string[]) => this.doingBusinessAsNames = doingBusinessAsNames)
    );
  }
}
