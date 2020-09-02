import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';

import { SiteRegistrationListViewModel } from '@registration/shared/models/site-registration.model';
import { AuthService } from '@auth/shared/services/auth.service';

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
    private authService: AuthService
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
    const email = this.siteRegistration?.signingAuthority?.email;
    if (email) {
      // TODO: Do we want to attach a cookie cutter subject?
      window.location.href = `mailto:${email}`;
    }
  }

  public ngOnInit(): void { }
}
