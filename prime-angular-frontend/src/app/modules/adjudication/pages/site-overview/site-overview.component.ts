import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { BehaviorSubject, EMPTY, forkJoin, Observable, of, Subscription } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { RoutePath, RouteUtils } from '@lib/utils/route-utils.class';
import { Party } from '@lib/models/party.model';
import { asyncValidator } from '@lib/validators/form-async.validators';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthoritySiteResource } from '@core/resources/health-authority-site-resource.service';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { NoteComponent } from '@shared/components/dialogs/content/note/note.component';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Organization } from '@registration/shared/models/organization.model';
import { Site } from '@registration/shared/models/site.model';
import { OrganizationClaim } from '@registration/shared/models/organization-claim.model';

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
  public refresh: BehaviorSubject<boolean>;
  public orgClaim: OrganizationClaim;
  public newSigningAuthority: Party;
  public form: FormGroup;
  public showSendNotification: boolean;
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
    private formUtilsService: FormUtilsService,
    private fb: FormBuilder,

  ) {
    this.hasActions = true;
    this.refresh = new BehaviorSubject<boolean>(null);
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
  }

  public get pec(): FormControl {
    return this.form.get('pec') as FormControl;
  }

  public onRoute(routePath: RoutePath): void {
    this.routeUtils.routeWithin(routePath);
  }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      const siteId = +this.route.snapshot.params.sid;
      const pec = this.form.value.pec;
      this.busy = this.siteResource.updatePecCode(siteId, pec)
        .subscribe(() => {
          this.refresh.next(true);
          this.site.pec = pec;
        });
    }
  }

  public onApproveOrgClaim(): void {
    this.busy = this.organizationResource
      .approveOrganizationClaim(this.orgClaim.organizationId, this.orgClaim.id)
      .subscribe(() => {
        this.refresh.next(true);
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
        this.orgClaim = orgClaim;
        this.initForm(site);
        this.showSendNotification = [
          CareSettingEnum.COMMUNITY_PHARMACIST,
          CareSettingEnum.DEVICE_PROVIDER
        ].includes(site.careSettingCode);
        return of(orgClaim?.newSigningAuthorityId);
      }),
      exhaustMap((newSigningAuthorityId: number | null) =>
        (newSigningAuthorityId)
          ? this.organizationResource.getSigningAuthorityById(newSigningAuthorityId)
          : of(null)
      )
    ).subscribe((signingAuthority: Party | null) => this.newSigningAuthority = signingAuthority);

    this.pec.markAsTouched();
  }

  private createFormInstance(): void {
    this.form = this.fb.group({
      pec: [
        '',
        [Validators.required],
        asyncValidator(this.checkPecIsAssignable(), 'assignable')
      ]
    });
  }

  private initForm({ pec }: Site): void {
    this.form.patchValue({ pec });
  }

  private checkPecIsAssignable(): (value: string) => Observable<boolean> {
    return (value: string) => this.siteResource.pecAssignable(this.route.snapshot.params.sid, value);
  }
}
