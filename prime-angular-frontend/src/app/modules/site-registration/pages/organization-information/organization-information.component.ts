import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { MatAutocompleteSelectedEvent } from '@angular/material/autocomplete';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable } from 'rxjs';
import { debounceTime, switchMap, map } from 'rxjs/operators';

import { FormUtilsService } from '@core/services/form-utils.service';
import { UtilsService, SortWeight } from '@core/services/utils.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { IPage } from '@registration/shared/interfaces/page.interface';
import { IForm } from '@registration/shared/interfaces/form.interface';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';
import {
  OrgBookResource, OrgBookAutocompleteResult, OrgBookFacetHttpResponse, OrgBookDetailHttpResponse, OrgBookRelatedHttpResponse
} from '@registration/shared/services/org-book-resource.service';

@Component({
  selector: 'app-organization-information',
  templateUrl: './organization-information.component.html',
  styleUrls: ['./organization-information.component.scss']
})
export class OrganizationInformationComponent implements OnInit, IPage, IForm {
  public busy: Subscription;
  public form: FormGroup;
  public routeUtils: RouteUtils;
  public organizations: string[];
  public doingBusinessAsNames: string[];
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteRegistrationResource: SiteRegistrationResource,
    private siteRegistrationService: SiteRegistrationService,
    private siteRegistrationStateService: SiteRegistrationStateService,
    private orgBookResource: OrgBookResource,
    private formUtilsService: FormUtilsService,
    private utilsService: UtilsService,
    private dialog: MatDialog
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public get name(): FormControl {
    return this.form.get('name') as FormControl;
  }

  public get orgId(): FormControl {
    return this.form.get('orgId') as FormControl;
  }

  public get doingBusinessAs(): FormControl {
    return this.form.get('doingBusinessAs') as FormControl;
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const payload = this.siteRegistrationStateService.site;
      this.siteRegistrationResource
        .updateSite(payload)
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    }
  }

  public onSelect({ option }: MatAutocompleteSelectedEvent) {
    const orgName = option.value;
    this.orgBookResource.getOrganizationFacet(orgName)
      .pipe(
        map((response: OrgBookFacetHttpResponse) => {
          // Assumed that only a single source ID will exist based on a
          // specific selection being made in autocomplete
          const sourceId = response.objects.results[0].topic.source_id;
          this.form.get('registrationId').patchValue(sourceId);
          return sourceId;
        }),
        switchMap((sourceId: string) => this.orgBookResource.getOrganizationDetail(sourceId)),
        map((response: OrgBookDetailHttpResponse) => response.id),
        switchMap((topicId: number) => this.orgBookResource.getOrganizationRelatedTo(topicId))
      )
      .subscribe((response: OrgBookRelatedHttpResponse[]) => {
        const doingBusinessAs = response
          .map((relation: OrgBookRelatedHttpResponse) => {
            // Assumed only a single name per organization is relavent
            const businessName = relation.related_topic.names[0].text;
            const isDoingBusinessAs = relation.attributes.some(a => a.value === 'Does Business As');
            return (isDoingBusinessAs) ? businessName : null;
          });
        // Remove duplicates since only names are persisted
        this.doingBusinessAsNames = [...new Set(doingBusinessAs)]
          .sort(this.sortDoingBusinessAsNames());
      });
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.MULTIPLE_SITES);
  }

  public nextRoute() {
    if (this.isCompleted) {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
    } else {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_ADDRESS);
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
    this.form = this.siteRegistrationStateService.organizationInformationForm;
  }

  private initForm() {
    const site = this.siteRegistrationService.site;
    this.isCompleted = site?.completed;
    this.siteRegistrationStateService.setSite(site, true);

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
