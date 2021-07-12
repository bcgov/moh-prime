import { Component, EventEmitter, Inject, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';

import { BehaviorSubject, forkJoin, of, Subscription } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { UtilsService } from '@core/services/utils.service';

import { PermissionService } from '@auth/shared/services/permission.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { SiteRegistrationContainerComponent } from '@adjudication/shared/components/site-registration-container/site-registration-container.component';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Organization } from '@registration/shared/models/organization.model';
import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';
import { Site } from '@registration/shared/models/site.model';
import { OrganizationClaim } from '@registration/shared/models/organization-claim.model';
import { Party } from '@lib/models/party.model';

@Component({
  selector: 'app-site-overview',
  templateUrl: './site-overview.component.html',
  styleUrls: ['./site-overview.component.scss']
})
export class SiteOverviewComponent extends SiteRegistrationContainerComponent implements OnInit {
  @Input() public hasActions: boolean;
  @Input() public actions: TemplateRef<any>;
  @Input() public content: TemplateRef<any>;
  @Output() public action: EventEmitter<void>;

  public busy: Subscription;
  public columns: string[];
  public organization: Organization;
  public site: Site;
  public orgClaim: OrganizationClaim;
  public newSigningAuthority: Party;
  public form: FormGroup;
  public refresh: BehaviorSubject<boolean>;

  public showSearchFilter: boolean;
  public AdjudicationRoutes = AdjudicationRoutes;

  constructor(
    @Inject(DIALOG_DEFAULT_OPTION) defaultOptions: DialogDefaultOptions,
    protected route: ActivatedRoute,
    protected router: Router,
    protected adjudicationResource: AdjudicationResource,
    protected organizationResource: OrganizationResource,
    protected siteResource: SiteResource,
    private formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    permissionService: PermissionService,
    dialog: MatDialog,
    utilsService: UtilsService
  ) {
    super(defaultOptions,
      route,
      router,
      organizationResource,
      siteResource,
      adjudicationResource,
      permissionService,
      utilsService,
      dialog);

    this.hasActions = true;
    this.refresh = new BehaviorSubject<boolean>(null);
    this.dataSource = new MatTableDataSource<SiteRegistrationListViewModel>([]);
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const siteId = this.route.snapshot.params.sid;
      this.busy = this.siteResource
        .updatePecCode(siteId, this.form.value.pec)
        .subscribe(() => this.refresh.next(true));
    }
  }

  public onApproveOrgClaim() {
    this.busy = this.organizationResource
      .approveOrganizationClaim(this.organization.id, this.newSigningAuthority.id)
      .pipe(
        exhaustMap(() => this.organizationResource.getOrganizationById(this.organization.id))
      )
      .subscribe((organization: Organization) => {
        this.refresh.next(true);
        this.organization = organization;
      });
  }

  public ngOnInit(): void {
    super.ngOnInit();

    this.createFormInstance();

    const { oid, sid } = this.route.snapshot.params;

    this.busy = forkJoin({
      organization: this.organizationResource.getOrganizationById(oid),
      site: this.siteResource.getSiteById(sid),
      orgClaim: this.organizationResource.getOrganizationClaimByOrgId(oid)
    }).pipe(
      exhaustMap(({ organization, site, orgClaim }) => {
        this.organization = organization;
        this.site = site;
        this.orgClaim = orgClaim;
        this.form.get('pec').setValue(site.pec);
        return of(null);
      }),
      exhaustMap(() => this.organizationResource.getSigningAuthorityByUserId(`${this.orgClaim?.newSigningAuthorityId}`))
    ).subscribe((signingAuthority: Party) => {
      // Note if orgClaim is null, signingAuthority will be null too
      this.newSigningAuthority = signingAuthority;
    });
  }

  private createFormInstance() {
    this.form = this.fb.group({
      pec: [
        '',
        [Validators.required]
      ]
    });
  }
}
