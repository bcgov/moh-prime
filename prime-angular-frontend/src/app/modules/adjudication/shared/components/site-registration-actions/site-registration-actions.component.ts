import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';
import { Role } from '@auth/shared/enum/role.enum';
import { PermissionService } from '@auth/shared/services/permission.service';
import { UtilsService } from '@core/services/utils.service';
import { SiteStatusType } from '@registration/shared/enum/site-status.enum';

@Component({
  selector: 'app-site-registration-actions',
  templateUrl: './site-registration-actions.component.html',
  styleUrls: ['./site-registration-actions.component.scss']
})
export class SiteRegistrationActionsComponent implements OnInit {
  @Input() siteRegistration: SiteRegistrationListViewModel;
  @Output() public approve: EventEmitter<number>;
  @Output() public decline: EventEmitter<number>;
  @Output() public escalate: EventEmitter<number>;
  @Output() public delete: EventEmitter<{ [key: string]: number }>;
  @Output() public enableEditing: EventEmitter<number>;

  public Role = Role;
  public SiteStatusType = SiteStatusType;

  constructor(
    private permissionService: PermissionService,
    private utilsService: UtilsService
  ) {
    this.delete = new EventEmitter<{ [key: string]: number }>();
    this.approve = new EventEmitter<number>();
    this.decline = new EventEmitter<number>();
    this.escalate = new EventEmitter<number>();
    this.enableEditing = new EventEmitter<number>();
  }

  public onApprove(): void {
    if (this.permissionService.hasRoles(Role.EDIT_SITE)) {
      this.approve.emit(this.siteRegistration.siteId);
    }
  }

  public onDecline(): void {
    if (this.permissionService.hasRoles(Role.EDIT_SITE)) {
      this.decline.emit(this.siteRegistration.siteId);
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

  public onDelete(record: { [key: string]: number }) {
    this.delete.emit(record);
  }

  public onEnableEditing(): void {
    if (this.permissionService.hasRoles(Role.EDIT_SITE)) {
      this.enableEditing.emit(this.siteRegistration.siteId);
    }
  }

  public ngOnInit(): void { }
}
