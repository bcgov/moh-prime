import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

import { forkJoin, Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { SiteStatusType } from '@registration/shared/enum/site-status.enum';
import { HealthAuthoritySite } from '@health-auth/shared/models/health-authority-site.model';
import { HealthAuthorityEnum } from '@shared/enums/health-authority.enum';
import { Role } from '@auth/shared/enum/role.enum';
import { HealthAuthority } from '@shared/models/health-authority.model';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteAdjudicationAction } from '@registration/shared/enum/site-adjudication-action.enum';

@Component({
  selector: 'app-site-overview-page',
  templateUrl: './site-overview-page.component.html',
  styleUrls: ['./site-overview-page.component.scss']
})
export class SiteOverviewPageComponent implements OnInit {

  public busy: Subscription;
  public form: FormGroup;
  public site: HealthAuthoritySite;
  public healthAuthority: HealthAuthority;

  public SiteAdjudicationAction = SiteAdjudicationAction;
  public SiteStatusType = SiteStatusType;
  public AdjudicationRoutes = AdjudicationRoutes;
  public Role = Role;
  public HealthAuthorityEnum = HealthAuthorityEnum;

  private routeUtils: RouteUtils;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService,
    private healthAuthorityResource: HealthAuthorityResource
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.routePath(AdjudicationRoutes.SITE_REGISTRATIONS));
  }

  public get pec(): FormControl {
    return this.form.get('pec') as FormControl;
  }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      const params = this.route.snapshot.params;
      this.busy = this.healthAuthorityResource.updateHealthAuthoritySitePec(+params.haid, +params.sid, this.form.value)
        .subscribe(() => this.getData());
    }
  }

  public onRoute(routePath: string | (string | number)[]) {
    this.routeUtils.routeWithin(routePath);
  }

  public onAssign(siteId: number): void {
  }

  public onReassign(siteId: number): void {
  }

  public onNotify(siteId: number, healthAuthorityOrganizationId: HealthAuthorityEnum): void {
  }

  public onEscalate(siteId: number): void {

  }

  public onToggleFlagSite({ siteId, flagged }: { siteId: number, flagged: boolean }) {
  }

  public onDelete(record: { [key: string]: number }) {
  }

  public onDecline(siteId: number): void {
  }

  public onApprove(siteId: number): void {
  }

  public onRequestChanges(siteId: number): void {

  }

  public onEnableEditing(siteId: number): void {

  }

  public onContactSigningAuthority(): void {

  }

  public onReject(siteId: number): void {}

  public onUnreject(siteId: number): void {}

  public isActionAllowed(action: SiteAdjudicationAction): boolean {
    if (!this.site) {
      return false;
    }
    switch (this.site.status) {
      case SiteStatusType.EDITABLE:
        return (action === SiteAdjudicationAction.REJECT);
      case SiteStatusType.IN_REVIEW:
        return (action === SiteAdjudicationAction.REQUEST_CHANGES
          || action === SiteAdjudicationAction.APPROVE
          || action === SiteAdjudicationAction.REJECT);
      case SiteStatusType.LOCKED:
        return (action === SiteAdjudicationAction.UNREJECT);
      default:
        return false;
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.getData();
  }

  private getData(): void {
    const params = this.route.snapshot.params;
    this.busy = forkJoin({
      site: this.healthAuthorityResource.getHealthAuthoritySiteById(+params.haid, +params.sid),
      healthAuthority: this.healthAuthorityResource.getHealthAuthorityById(+params.haid)
    })
      .subscribe(({ site, healthAuthority }) => {
        this.site = site;
        this.healthAuthority = healthAuthority;
        this.form.get('pec').setValue(site.siteId);
      });
  }

  private createFormInstance() {
    this.form = this.fb.group({
      pec: [
        '',
        [Validators.required],
      ]
    });
  }
}
