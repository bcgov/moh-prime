import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';
import { AuthService } from '@auth/shared/services/auth.service';
import { UtilsService } from '@core/services/utils.service';

@Component({
  selector: 'app-site-registration-actions',
  templateUrl: './site-registration-actions.component.html',
  styleUrls: ['./site-registration-actions.component.scss']
})
export class SiteRegistrationActionsComponent implements OnInit {
  @Input() siteRegistration: SiteRegistrationListViewModel;
  @Output() public approve: EventEmitter<number>;
  @Output() public decline: EventEmitter<number>;

  constructor(
    private authService: AuthService,
    private utilService: UtilsService
  ) {
    this.approve = new EventEmitter<number>();
    this.decline = new EventEmitter<number>();
  }

  public get canEdit(): boolean {
    return this.authService.isAdmin();
  }

  public approveSite(): void {
    if (this.canEdit) {
      this.approve.emit(this.siteRegistration.siteId);
    }
  }

  public declineSite(): void {
    if (this.canEdit) {
      this.approve.emit(this.siteRegistration.siteId);
    }
  }

  public contactSigningAuthorityForSite() {
    const signingAuthority = this.siteRegistration?.signingAuthority;
    if (signingAuthority) {
      this.utilService.mailTo(signingAuthority.email, `PRIME Site Registration - ${this.siteRegistration.name}`,
        `Dear ${signingAuthority.firstName} ${signingAuthority.lastName},`);
    }
  }

  public ngOnInit(): void { }
}
