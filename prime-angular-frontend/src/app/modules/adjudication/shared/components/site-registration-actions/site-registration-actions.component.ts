import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import { Subscription } from 'rxjs';

import { EmailUtils } from '@lib/utils/email-utils.class';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { SiteAdjudicationAction } from '@lib/enums/site-adjudication-action.enum';
import { Role } from '@auth/shared/enum/role.enum';
import { PermissionService } from '@auth/shared/services/permission.service';
import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';
import { HealthAuthoritySiteAdminList } from '@health-auth/shared/models/health-authority-admin-site-list.model';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { AuthorizedUser } from '@shared/models/authorized-user.model';

@Component({
  selector: 'app-site-registration-actions',
  templateUrl: './site-registration-actions.component.html',
  styleUrls: ['./site-registration-actions.component.scss']
})
export class SiteRegistrationActionsComponent implements OnInit {
  @Input() siteRegistration: SiteRegistrationListViewModel | HealthAuthoritySiteAdminList;
  @Output() public approve: EventEmitter<number>;
  @Output() public reject: EventEmitter<number>;
  @Output() public unreject: EventEmitter<number>;
  @Output() public escalate: EventEmitter<number>;
  @Output() public archive: EventEmitter<number>;
  @Output() public restore: EventEmitter<number>;
  @Output() public delete: EventEmitter<{ [key: string]: number }>;
  @Output() public enableEditing: EventEmitter<number>;
  @Output() public flag: EventEmitter<{ siteId: number, flagged: boolean }>;
  @Output() public isNew: EventEmitter<{ siteId: number, isNew: boolean }>;

  public Role = Role;
  public SiteStatusType = SiteStatusType;
  public SiteAdjudicationAction = SiteAdjudicationAction;
  public busy: Subscription;

  constructor(
    protected healthAuthorityResource: HealthAuthorityResource,
    private permissionService: PermissionService
  ) {
    this.delete = new EventEmitter<{ [key: string]: number }>();
    this.approve = new EventEmitter<number>();
    this.reject = new EventEmitter<number>();
    this.unreject = new EventEmitter<number>();
    this.escalate = new EventEmitter<number>();
    this.archive = new EventEmitter<number>();
    this.restore = new EventEmitter<number>();
    this.enableEditing = new EventEmitter<number>();
    this.flag = new EventEmitter<{ siteId: number, flagged: boolean }>();
    this.isNew = new EventEmitter<{ siteId: number, isNew: boolean }>();
  }

  public get isCommunitySite(): boolean {
    return !!((this.siteRegistration as SiteRegistrationListViewModel).organizationId)
  }

  public getSiteOrganizationId(): number {
    return (this.siteRegistration as SiteRegistrationListViewModel).organizationId;
  }

  public isHealthAuthoritySite(): boolean {
    return !((this.siteRegistration as SiteRegistrationListViewModel).organizationId)
  }

  public onApprove(): void {
    if (this.permissionService.hasRoles(Role.EDIT_SITE)) {
      this.approve.emit(this.siteRegistration.id);
    }
  }

  public onReject(): void {
    if (this.permissionService.hasRoles(Role.EDIT_SITE)) {
      this.reject.emit(this.siteRegistration.id);
    }
  }

  public onUnreject(): void {
    if (this.permissionService.hasRoles(Role.EDIT_SITE)) {
      this.unreject.emit(this.siteRegistration.id);
    }
  }

  public onEscalate(): void {
    if (this.permissionService.hasRoles(Role.EDIT_SITE)) {
      this.escalate.emit(this.siteRegistration.id);
    }
  }

  public onArchive() {
    this.archive.emit(this.siteRegistration.id);
  }

  public onRestore() {
    this.restore.emit(this.siteRegistration.id);
  }

  public onContactSigningAuthority() {
    const site = this.siteRegistration as SiteRegistrationListViewModel;
    const signingAuthority = site.signingAuthority;
    if (signingAuthority) {
      EmailUtils.openEmailClient(
        signingAuthority.email,
        `PRIME Site Registration - ${site.name}`,
        `Dear ${signingAuthority.firstName} ${signingAuthority.lastName},`
      );
    }
  }

  public onContactAuthorizedUser() {
    const healthAuthoritySite = this.siteRegistration as HealthAuthoritySiteAdminList;
    this.busy = this.healthAuthorityResource.getAuthorizedUsersByHealthAuthority(healthAuthoritySite.healthAuthorityOrganizationId)
      .subscribe((au_list: AuthorizedUser[]) => {

        if (au_list?.length > 0) {
          let auEmails = au_list.map(au => {
            return au.email;
          });
          let toEmails = "";
          auEmails.forEach(e => {
            if (toEmails.indexOf(e) < 0) {
              toEmails += e + ";";
            }
          });

          let authorizedUserName = "Dear Authorized Users";
          if (toEmails.split("@").length === 2) {
            let au = au_list.find(au => toEmails.indexOf(au.email) >= 0);
            authorizedUserName = `Dear ${au.firstName} ${au.lastName}`;
          }

          EmailUtils.openEmailClient(
            toEmails,
            `PRIME Health Authority Site Registration - ${healthAuthoritySite.healthAuthorityName}`,
            authorizedUserName
          );
        }
      });
  }

  public onToggleFlagSite() {
    if (this.permissionService.hasRoles(Role.VIEW_SITE)) {
      this.flag.emit({
        siteId: this.siteRegistration.id,
        flagged: !this.siteRegistration.flagged
      });
    }
  }

  public onToggleIsNewSite() {
    if (this.permissionService.hasRoles(Role.VIEW_SITE)) {
      this.isNew.emit({
        siteId: this.siteRegistration.id,
        isNew: !this.siteRegistration.isNew
      });
    }
  }

  public onDelete(record: { [key: string]: number }) {
    this.delete.emit(record);
  }

  public onRequestChanges(): void {
    if (this.permissionService.hasRoles(Role.EDIT_SITE)) {
      this.enableEditing.emit(this.siteRegistration.id);
    }
  }

  /**
   * @description
   * Check whether the given action is valid according to the status of the
   * site registration.
   */
  public isActionAllowed(action: SiteAdjudicationAction): boolean {
    switch (this.siteRegistration.status) {
      case SiteStatusType.EDITABLE:
        return (action === SiteAdjudicationAction.REJECT
          || action === SiteAdjudicationAction.ARCHIVE);
      case SiteStatusType.IN_REVIEW:
        return (action === SiteAdjudicationAction.REQUEST_CHANGES
          || action === SiteAdjudicationAction.APPROVE
          || action === SiteAdjudicationAction.REJECT
          || action === SiteAdjudicationAction.ARCHIVE);
      case SiteStatusType.LOCKED:
        return (action === SiteAdjudicationAction.UNREJECT);
      case SiteStatusType.ARCHIVED:
        return (action === SiteAdjudicationAction.RESTORE);
      default:
        return false;
    }
  }

  public ngOnInit(): void { }
}
