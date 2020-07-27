import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray, FormControl } from '@angular/forms';

import { Subscription } from 'rxjs';
import { exhaustMap, map } from 'rxjs/operators';

import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { OrganizationResource } from '@core/resources/organization-resource.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteService } from '@registration/shared/services/site.service';
import { RemoteUser } from '@registration/shared/models/remote-user.model';
import { Organization } from '@registration/shared/models/organization.model';

@Component({
  selector: 'app-remote-users',
  templateUrl: './remote-users.component.html',
  styleUrls: ['./remote-users.component.scss']
})
export class RemoteUsersComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;
  public hasNoRemoteUserError: boolean;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
    private organizationResource: OrganizationResource,
    private formUtilsService: FormUtilsService
  ) {
    this.title = 'Practitioners Requiring Remote PharmaNet Access';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public get remoteUsers(): FormArray {
    return this.form.get('remoteUsers') as FormArray;
  }

  public get hasRemoteUsers(): FormControl {
    return this.form.get('hasRemoteUsers') as FormControl;
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      this.hasNoRemoteUserError = false;
      const payload = this.siteFormStateService.json;
      const organizationId = this.route.snapshot.params.oid;

      this.organizationResource
        .getOrganizationById(organizationId)
        .pipe(
          map((organization: Organization) => !!organization.acceptedAgreementDate),
          // When the organization agreement has already been signed mark the site as completed
          exhaustMap((hasSignedOrgAgreement: boolean) =>
            this.siteResource.updateSite(payload, hasSignedOrgAgreement)
              .pipe(map(() => hasSignedOrgAgreement))
          )
        )
        .subscribe((hasSignedOrgAgreement: boolean) => {
          this.form.markAsPristine();
          this.nextRoute(organizationId, hasSignedOrgAgreement);
        });
    } else {
      this.hasNoRemoteUserError = true;
    }
  }

  public onRemove(index: number) {
    this.remoteUsers.removeAt(index);
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(['../', SiteRoutes.TECHNICAL_SUPPORT]);
  }

  public nextRoute(organizationId: number, hasSignedOrgAgreement: boolean) {
    if (!hasSignedOrgAgreement) {
      const siteId = this.route.snapshot.params.sid;
      // Provide site for redirection after accepting the organization agreement
      this.routeUtils.routeTo([SiteRoutes.routePath(SiteRoutes.SITE_MANAGEMENT), organizationId, SiteRoutes.ORGANIZATION_AGREEMENT], {
        queryParams: { redirect: `${SiteRoutes.SITES}/${siteId}`, siteId }
      });
    } else {
      this.routeUtils.routeRelativeTo(['../', SiteRoutes.SITE_REVIEW]);
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.siteFormStateService.remoteUsersForm;
  }

  private initForm() {
    this.remoteUsers.valueChanges
      .subscribe((remoteUsers: RemoteUser[]) => {
        (remoteUsers.length)
          ? this.hasRemoteUsers.disable({ emitEvent: false })
          : this.hasRemoteUsers.enable({ emitEvent: false });
      });

    this.hasRemoteUsers.valueChanges
      .subscribe((hasRemoteUsers: boolean) => {
        (hasRemoteUsers)
          ? this.remoteUsers.setValidators(FormArrayValidators.atLeast(1))
          : this.remoteUsers.clearValidators();

        this.hasNoRemoteUserError = false;
        this.remoteUsers.updateValueAndValidity({ emitEvent: false });
      });

    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    // Inform the parent not to patch the form as there are outstanding changes
    // to the remote users that need to be persisted
    const fromRemoteUser = this.route.snapshot.queryParams.fromRemoteUser === 'true';
    // Remove query param from URL without refreshing
    this.router.navigate([], { queryParams: { fromRemoteUser: null } });
    this.siteFormStateService.setForm(site, !fromRemoteUser);
  }
}
