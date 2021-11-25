import { Component, EventEmitter, Inject, Input, OnInit, Output, TemplateRef } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ActivatedRoute, Router } from '@angular/router';

import { BehaviorSubject, EMPTY, forkJoin, Observable, of, Subscription } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { Party } from '@lib/models/party.model';
import { asyncValidator } from '@lib/validators/form-async.validators';
import { OrganizationResource } from '@core/resources/organization-resource.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { ToastService } from '@core/services/toast.service';
import { DialogDefaultOptions } from '@shared/components/dialogs/dialog-default-options.model';
import { DIALOG_DEFAULT_OPTION } from '@shared/components/dialogs/dialogs-properties.provider';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { NoteComponent } from '@shared/components/dialogs/content/note/note.component';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

import { PermissionService } from '@auth/shared/services/permission.service';
import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { SiteRegistrationContainerComponent } from '@adjudication/shared/components/site-registration-container/site-registration-container.component';
import { AdjudicationResource } from '@adjudication/shared/services/adjudication-resource.service';
import { Organization } from '@registration/shared/models/organization.model';
import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';
import { Site } from '@registration/shared/models/site.model';
import { OrganizationClaim } from '@registration/shared/models/organization-claim.model';
import { BusinessLicence } from '@registration/shared/models/business-licence.model';

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
  public businessLicences: BusinessLicence[];
  public form: FormGroup;
  public refresh: BehaviorSubject<boolean>;

  public showSendNotification: boolean;
  public isNotificationSent: boolean;
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
    toastService: ToastService
  ) {
    super(
      defaultOptions,
      route,
      router,
      organizationResource,
      siteResource,
      adjudicationResource,
      dialog,
      toastService,
      permissionService
    );

    this.hasActions = true;
    this.refresh = new BehaviorSubject<boolean>(null);
    this.dataSource = new MatTableDataSource<SiteRegistrationListViewModel>([]);
  }

  public get pec(): FormControl {
    return this.form.get('pec') as FormControl;
  }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      const siteId = this.route.snapshot.params.sid;
      const pec = this.form.value.pec;
      this.busy = this.siteResource.updatePecCode(siteId, pec)
        .subscribe(() => {
          this.refresh.next(true);
          this.site.pec = pec;
        });
    }
  }

  public onApprove(siteId: number) {
    if (!this.formUtilsService.checkValidity(this.form)) {
      return;
    }

    super.onApprove(siteId);
  }

  public onApproveOrgClaim(): void {
    this.busy = this.organizationResource
      .approveOrganizationClaim(this.orgClaim.organizationId, this.orgClaim.id)
      .pipe(
        exhaustMap(() => this.organizationResource.getOrganizationById(this.organization.id))
      )
      .subscribe((organization: Organization) => {
        this.refresh.next(true);
        this.organization = organization;
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
    super.ngOnInit();

    this.createFormInstance();

    const { oid, sid } = this.route.snapshot.params;

    this.busy = forkJoin([
      this.organizationResource.getOrganizationById(oid),
      this.siteResource.getSiteById(sid),
      this.siteResource.getBusinessLicences(sid),
      this.organizationResource.getOrganizationClaimByOrgId(oid)
    ]).pipe(
      exhaustMap(([organization, site, businessLicences, orgClaim]: [Organization, Site, BusinessLicence[], OrganizationClaim]) => {
        this.organization = organization;
        this.site = site;
        this.businessLicences = businessLicences;
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
  }

  private createFormInstance() {
    this.form = this.fb.group({
      pec: [
        '',
        [Validators.required],
        asyncValidator(this.checkPecIsAssignable(), 'assignable')
      ]
    });
  }

  private initForm({ pec }: Site) {
    this.form.patchValue({ pec });
  }

  private checkPecIsAssignable(): (value: string) => Observable<boolean> {
    return (value: string) => this.siteResource.pecAssignable(this.site.id, value);
  }
}
