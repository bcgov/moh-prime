import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatDialog } from '@angular/material/dialog';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { Observable, of, pipe } from 'rxjs';
import { debounceTime, switchMap, tap, exhaustMap, take, map } from 'rxjs/operators';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { Party } from '@lib/models/party.model';
import { FormUtilsService } from '@core/services/form-utils.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { asyncValidator } from '@lib/validators/form-async.validators';

import { AuthService } from '@auth/shared/services/auth.service';
import { BcscUser } from '@auth/shared/models/bcsc-user.model';

import { SiteRoutes } from '@registration/site-registration.routes';
import { Site } from '@registration/shared/models/site.model';
import { OrgBookAutocompleteHttpResponse } from '@registration/shared/models/orgbook.model';
import { OrganizationService } from '@registration/shared/services/organization.service';
import { OrganizationFormStateService } from '@registration/shared/services/organization-form-state.service';
import { OrgBookResource } from '@registration/shared/services/org-book-resource.service';
import { Organization } from '@registration/shared/models/organization.model';
import { OrganizationNamePageFormState } from './organization-name-page-form-state.class';
import { WebApiLoggerService } from '@core/services/web-api-logger.service';

@UntilDestroy()
@Component({
  selector: 'app-organization-name-page',
  templateUrl: './organization-name-page.component.html',
  styleUrls: ['./organization-name-page.component.scss']
})
export class OrganizationNamePageComponent extends AbstractEnrolmentPage implements OnInit {
  public formState: OrganizationNamePageFormState;
  public title: string;
  public routeUtils: RouteUtils;
  public organizationId: number;
  public isCompleted: boolean;
  public usedOrgBook: boolean;
  public orgBookOrganizations: string[];
  public orgBookTotalResults: number;
  public orgBookDoingBusinessAsNames: string[];
  public SiteRoutes = SiteRoutes;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private organizationService: OrganizationService,
    private organizationResource: OrganizationResource,
    private organizationFormStateService: OrganizationFormStateService,
    private orgBookResource: OrgBookResource,
    private siteResource: SiteResource,
    private authService: AuthService,
    private route: ActivatedRoute,
    private webApiLogger: WebApiLoggerService,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);

    this.organizationId = +this.route.snapshot.params.oid;
  }

  public getOrgBookLink(orgId: string, display: boolean = false) {
    const url = 'https://www.orgbook.gov.bc.ca/en/organization';
    return (display)
      ? `${url}/${orgId}`
      : `${url}/registration.registries.ca/${orgId}`;
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
    this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATION_REVIEW);
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
    if (this.organizationId === 0) {
      return;
    }
    const organization = this.organizationService.organizations?.find((org) => org.id === this.organizationId);
    if (!organization) {
      return;
    }
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
        untilDestroyed(this),
        debounceTime(400),
        switchMap((value: string) => this.orgBookResource.autocomplete(value))
      )
      .subscribe((response: OrgBookAutocompleteHttpResponse) => {
        // Assumed only a single name per organization is relevant
        this.orgBookOrganizations = response.results.map(o => o.names[0]?.text).filter(o => o);
        this.orgBookTotalResults = response.total;
      });

    this.formState.name.addAsyncValidators(asyncValidator(this.checkOrganizationName$(), 'duplicate'));
  }

  private checkOrganizationName$(): (name: string) => Observable<boolean> {
    return (name: string) => this.authService.getUser$()
      .pipe(
        exhaustMap((bcscUser: BcscUser) => {
          return this.organizationResource.getSigningAuthorityOrganizationByUsername(bcscUser.username)
        }),
        map((orgs: Organization[]) => {
          return !orgs || !orgs.some(org => org.name === name && org.id !== this.organizationId);
        }),
      );
  }

  protected performSubmission(): Observable<number | null> {
    const payload = this.organizationFormStateService.json;
    const request$ = (this.organizationId)
      ? this.organizationResource.updateOrganization(payload)
      : this.authService.getUser$()
        .pipe(
          exhaustMap((bcscUser: BcscUser) => this.organizationResource.getSigningAuthorityByUsername(bcscUser.username)),
          exhaustMap((party: Party) => this.organizationResource.createOrganization(party.id)),
          tap((organization: Organization) => {
            this.organizationService.organization = organization;

            // Add newly created Organization
            if (!this.organizationService.organizations) {
              this.organizationService.organizations = [organization];
            } else {
              this.organizationService.organizations.push(organization);
            }

            // Copy over only new information from `organization`
            payload.id = organization.id;
            payload.signingAuthorityId = organization.signingAuthorityId;
            payload.signingAuthority = organization.signingAuthority;

            payload.name = this.organizationFormStateService.json.name;
            payload.doingBusinessAs = this.organizationFormStateService.json.doingBusinessAs;
          }),
          exhaustMap(() => this.webApiLogger.debug(`Registration ID updating to ${payload.registrationId}`, { orgName: payload.name })),
          exhaustMap(() => this.organizationResource.updateOrganization(payload)),
        );

    return request$
      .pipe(
        exhaustMap(() => this.organizationResource.updateCompleted(this.organizationService.organization.id)),
        exhaustMap(
          // Check the organization wasn't completed before the update, and
          // if not then this is the initial registration wizard
          () => (!this.isCompleted)
            ? this.siteResource.createSite(this.organizationService.organization.id)
            : of(null)
        ),
        map((site: Site) => site?.id),
        tap(() => this.organizationService.organization.completed = true),
      );
  }

  protected afterSubmitIsSuccessful(siteId?: number): void {
    const redirectPath = this.route.snapshot.queryParams.redirect;
    let routePath: string | string[];

    if (redirectPath) {
      routePath = [redirectPath, SiteRoutes.SITE_REVIEW];
    } else {
      routePath = (!this.isCompleted)
        ? [SiteRoutes.SITES, `${siteId}`, SiteRoutes.CARE_SETTING]
        : SiteRoutes.ORGANIZATION_REVIEW;
    }

    this.routeUtils.routeRelativeTo(routePath);
  }

  private getDoingBusinessAs() {
    return pipe(
      // Expects an organization registrationId
      this.orgBookResource.doingBusinessAsMap(),
      tap((doingBusinessAsNames: string[]) => this.orgBookDoingBusinessAsNames = doingBusinessAsNames)
    );
  }
}
