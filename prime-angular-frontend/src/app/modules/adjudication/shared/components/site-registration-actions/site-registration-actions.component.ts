import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';
import { Role } from '@auth/shared/enum/role.enum';
import { PermissionService } from '@auth/shared/services/permission.service';
import { UtilsService } from '@core/services/utils.service';
import { SiteStatusType } from '@registration/shared/enum/site-status.enum';
import { SiteAdjudicationAction } from '@registration/shared/enum/site-adjudication-action.enum';

@Component({
  selector: 'app-site-registration-actions',
  templateUrl: './site-registration-actions.component.html',
  styleUrls: ['./site-registration-actions.component.scss']
})
export class SiteRegistrationActionsComponent implements OnInit {
  @Input() siteRegistration: SiteRegistrationListViewModel;
  @Output() public approve: EventEmitter<number>;
  @Output() public decline: EventEmitter<number>;
  @Output() public unreject: EventEmitter<number>;
  @Output() public escalate: EventEmitter<number>;
  @Output() public delete: EventEmitter<{ [key: string]: number }>;
  @Output() public enableEditing: EventEmitter<number>;
  @Output() public flag: EventEmitter<{ siteId: number, flagged: boolean }>;

  public Role = Role;
  public SiteStatusType = SiteStatusType;
  public SiteAdjudicationAction = SiteAdjudicationAction;

  constructor(
    private permissionService: PermissionService,
    private utilsService: UtilsService
  ) {
    this.delete = new EventEmitter<{ [key: string]: number }>();
    this.approve = new EventEmitter<number>();
    this.decline = new EventEmitter<number>();
    this.unreject = new EventEmitter<number>();
    this.escalate = new EventEmitter<number>();
    this.enableEditing = new EventEmitter<number>();
    this.flag = new EventEmitter<{ siteId: number, flagged: boolean }>();
  }

  public onApprove(): void {
    if (this.permissionService.hasRoles(Role.EDIT_SITE)) {
      this.approve.emit(this.siteRegistration.siteId);
    }
  }

  public onReject(): void {
    if (this.permissionService.hasRoles(Role.EDIT_SITE)) {
      this.decline.emit(this.siteRegistration.siteId);
    }
  }

  public onUnreject(): void {
    if (this.permissionService.hasRoles(Role.EDIT_SITE)) {
      this.unreject.emit(this.siteRegistration.siteId);
    }
  }

  public onEscalate(): void {
    if (this.permissionService.hasRoles(Role.EDIT_SITE)) {
      this.escalate.emit(this.siteRegistration.siteId);
    }
  }

  public onContactSigningAuthority() {
    const signingAuthority = this.siteRegistration?.signingAuthority;
    if (signingAuthority) {
      this.utilsService.mailTo(
        signingAuthority.email,
        `PRIME Site Registration - ${this.siteRegistration.name}`,
        `Dear ${signingAuthority.firstName} ${signingAuthority.lastName},`
      );
    }
  }

  public onToggleFlagSite() {
    if (this.permissionService.hasRoles(Role.VIEW_SITE)) {
      this.flag.emit({
        siteId: this.siteRegistration.siteId,
        flagged: !this.siteRegistration.flagged
      })
    }
  }

  public onDelete(record: { [key: string]: number }) {
    this.delete.emit(record);
  }

  public onRequestChanges(): void {
    if (this.permissionService.hasRoles(Role.EDIT_SITE)) {
      this.enableEditing.emit(this.siteRegistration.siteId);
    }
  }

  /**
   * @param action
   * @returns Whether the given action is valid according to the status of the site registration
   */
  public isActionAllowed(action: SiteAdjudicationAction): boolean {
    switch (this.siteRegistration.status) {
      case SiteStatusType.EDITABLE:
        return (action === SiteAdjudicationAction.REJECT);
      case SiteStatusType.IN_REVIEW:
        return (action === SiteAdjudicationAction.REQUEST_CHANGES || action === SiteAdjudicationAction.APPROVE || action === SiteAdjudicationAction.REJECT);
      case SiteStatusType.LOCKED:
        return (action === SiteAdjudicationAction.UNREJECT);
      default:
        return false;
    }
  }

  public ngOnInit(): void { }
}
