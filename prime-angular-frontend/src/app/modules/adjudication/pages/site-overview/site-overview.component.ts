import { Component, OnInit } from '@angular/core';
import { UntypedFormBuilder, UntypedFormControl, UntypedFormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { BehaviorSubject, EMPTY, forkJoin, Observable, of, Subscription } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { RoutePath, RouteUtils } from '@lib/utils/route-utils.class';
import { Party } from '@lib/models/party.model';
import { asyncValidator } from '@lib/validators/form-async.validators';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { HealthAuthoritySiteResource } from '@core/resources/health-authority-site-resource.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { NoteComponent } from '@shared/components/dialogs/content/note/note.component';
import { ChangeVendorNoteComponent } from '@shared/components/dialogs/content/change-vendor-note/change-vendor-note.component';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Organization } from '@registration/shared/models/organization.model';
import { CommunitySiteViewModel, Site } from '@registration/shared/models/site.model';
import { OrganizationClaim } from '@registration/shared/models/organization-claim.model';
import { ConfigService } from '@config/config.service';
import { VendorConfig } from '@config/config.model';
import { LinkSiteComponent } from '@shared/components/dialogs/content/link-site/link-site.component';

@Component({
  selector: 'app-site-overview',
  templateUrl: './site-overview.component.html',
  styleUrls: ['./site-overview.component.scss']
})
export class SiteOverviewComponent implements OnInit {
  public busy: Subscription;
  public hasActions: boolean;
  public organization: Organization;
  public site: Site;
  public predecessorSite: CommunitySiteViewModel;
  public siteVendors: VendorConfig[];
  public refresh: BehaviorSubject<boolean>;
  public orgClaim: OrganizationClaim;
  public newSigningAuthority: Party;
  public form: UntypedFormGroup;
  public showSendNotification: boolean;
  public showLinkSection: boolean;
  public isNotificationSent: boolean;
  public AdjudicationRoutes = AdjudicationRoutes;

  private routeUtils: RouteUtils;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected siteResource: SiteResource,
    protected adjudicationResource: AdjudicationResource,
    protected healthAuthSiteResource: HealthAuthoritySiteResource,
    private organizationResource: OrganizationResource,
    private fb: UntypedFormBuilder,
    private configService: ConfigService,
  ) {
    this.hasActions = true;
    this.refresh = new BehaviorSubject<boolean>(null);
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
  }

  public get pec(): UntypedFormControl {
    return this.form.get('pec') as UntypedFormControl;
  }

  public get vendors(): UntypedFormControl {
    return this.form.get('vendors') as UntypedFormControl;
  }

  public onRoute(routePath: RoutePath): void {
    this.routeUtils.routeWithin(routePath);
  }

  public saveSiteId(): void {
    if (this.pec.valid) {
      const siteId = +this.route.snapshot.params.sid;
      const pec = this.form.value.pec;
      this.busy = this.siteResource.updatePecCode(siteId, pec)
        .subscribe(() => {
          this.refresh.next(true);
          this.site.pec = pec;
        });
    }
  }

  public onEditVendor(): void {
    const siteId = +this.route.snapshot.params.sid;
    //const vendorChangeText = `from ${existingVendor} to ${vendor.name}`;

    const data: DialogOptions = {
      data: {
        siteId,
        vendorCode: this.site.siteVendors[0].vendorCode,
        siteVendors: this.siteVendors,
      }
    };
    this.dialog.open(ChangeVendorNoteComponent, { data }).afterClosed()
      .subscribe((data) => {
        if (data?.result) {
          this.refresh.next(true);
          this.site.siteVendors[0].vendorCode = data.vendorCode;
        }
      });
  }

  public onApproveOrgClaim(): void {
    this.busy = this.organizationResource
      .approveOrganizationClaim(this.orgClaim.organizationId, this.orgClaim.id)
      .subscribe(() => {
        this.refresh.next(true);
        this.organization.hasClaim = false;
      });
  }

  public onSendNotification(): void {
    const data: DialogOptions = {
      title: 'PharmaCare Provider Enrolment',
      message: 'Send notification to provider enrolment team',
      actionText: 'Send Notification',
      component: NoteComponent
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .pipe(
        exhaustMap((result: { output: string }) => {
          if (result) {
            return this.siteResource.sendSiteReviewedEmailUser(this.route.snapshot.params.sid, result.output);
          }
          return EMPTY;
        })
      )
      .subscribe(() => this.isNotificationSent = true);
  }

  public onOpenSiteLink(): void {
    const data: DialogOptions = {
      data: {
        siteId: this.site.id,
        organizationId: this.predecessorSite?.organization.id,
        predecessorSiteId: this.predecessorSite?.site.id
      },
    };
    this.busy = this.dialog.open(LinkSiteComponent, { data })
      .afterClosed()
      .subscribe((result: boolean) => {
        if (result) {
          this.siteResource.getSiteById(this.site.id).subscribe((site) => {
            this.predecessorSite = site.predecessorSite;
            this.site.predecessorSite = this.predecessorSite;
          })
        }
      });
  }

  public onDeleteSiteLink(): void {
    const data: DialogOptions = {
      title: 'Remove Predecessor Site',
      message: 'Are you sure to remove Predecessor Site?',
      actionText: 'Remove'
    };
    this.busy = this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .subscribe((result: boolean) => {
        if (result) {
          this.siteResource.removeSiteLink(this.site.predecessorSite.site.id, this.site.id)
            .subscribe(() => {
              this.site.predecessorSite = null;
              this.predecessorSite = null;
            });
        }
      });
  }

  public ngOnInit(): void {
    this.createFormInstance();

    const { oid, sid } = this.route.snapshot.params;

    this.busy = forkJoin([
      this.organizationResource.getOrganizationById(oid),
      this.siteResource.getSiteById(sid),
      this.organizationResource.getOrganizationClaimByOrgId(oid)
    ]).pipe(
      exhaustMap(([org, site, orgClaim]: [Organization, Site, OrganizationClaim]) => {
        // Full objects are needed to display overview components
        this.organization = org;
        this.site = site;
        this.predecessorSite = site.predecessorSite;
        this.siteVendors = this.configService.vendors
          .filter((vendor: VendorConfig) => vendor.careSettingCode === site.careSettingCode)
        this.orgClaim = orgClaim;
        this.initForm(site, this.siteVendors.find((vendor: VendorConfig) => vendor.code === this.site.siteVendors[0].vendorCode));
        this.showSendNotification = [
          CareSettingEnum.COMMUNITY_PHARMACY,
          CareSettingEnum.DEVICE_PROVIDER
        ].includes(site.careSettingCode);
        this.showLinkSection = [
          CareSettingEnum.PRIVATE_COMMUNITY_HEALTH_PRACTICE
        ].includes(site.careSettingCode);
        return of(orgClaim?.newSigningAuthorityId);
      }),
      exhaustMap((newSigningAuthorityId: number | null) =>
        (newSigningAuthorityId)
          ? this.organizationResource.getSigningAuthorityById(newSigningAuthorityId)
          : of(null)
      ),
    ).subscribe((signingAuthority: Party | null) => this.newSigningAuthority = signingAuthority);

    this.pec.markAsTouched();
  }

  private createFormInstance(): void {
    this.form = this.fb.group({
      pec: [
        '',
        [Validators.required],
        asyncValidator(this.checkPecIsAssignable(), 'assignable')
      ],
      vendors: ['', [Validators.required]]
    });
  }

  private initForm({ pec }: Site, vendor: VendorConfig): void {
    this.form.patchValue({ pec, 'vendors': vendor });
  }

  private checkPecIsAssignable(): (value: string) => Observable<boolean> {
    return (value: string) => this.siteResource.pecAssignable(this.route.snapshot.params.sid, value);
  }
}
