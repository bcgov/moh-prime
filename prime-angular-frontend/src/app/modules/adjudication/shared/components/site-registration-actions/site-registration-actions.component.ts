import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';
import { AuthService } from '@auth/shared/services/auth.service';
import { UtilsService } from '@core/services/utils.service';

import { map } from 'rxjs/operators';
import { OrganizationResource } from '@core/resources/organization-resource.service';

@Component({
  selector: 'app-site-registration-actions',
  templateUrl: './site-registration-actions.component.html',
  styleUrls: ['./site-registration-actions.component.scss']
})
export class SiteRegistrationActionsComponent implements OnInit {
  @Input() siteRegistration: SiteRegistrationListViewModel;
  @Output() public approve: EventEmitter<number>;
  @Output() public decline: EventEmitter<number>;
  @Output() public delete: EventEmitter<{ [key: string]: number }>;

  constructor(
    private authService: AuthService,
    private utilsService: UtilsService
  ) {
    this.delete = new EventEmitter<{ [key: string]: number }>();
    this.approve = new EventEmitter<number>();
    this.decline = new EventEmitter<number>();
  }

  public get canEdit(): boolean {
    return this.authService.isAdmin();
  }

  public get canDelete(): boolean {
    return this.authService.isSuperAdmin();
  }

  public onApprove(): void {
    if (this.canEdit) {
      this.approve.emit(this.siteRegistration.siteId);
    }
  }

  public onDecline(): void {
    if (this.canEdit) {
      this.decline.emit(this.siteRegistration.siteId);
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

  public ngOnInit(): void { }
}
