import { Component, OnInit, NgZone } from '@angular/core';
import { Router } from '@angular/router';

import { BehaviorSubject } from 'rxjs';

import { AuthTokenService } from 'src/app/core/services/auth-token.service';
import { LoggerService } from 'src/app/core/services/logger.service';
import { WindowRefService } from 'src/app/core/services/window-ref.service';

declare const gapi: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  private isAuthenticated: BehaviorSubject<boolean>;

  constructor(
    private tokenService: AuthTokenService,
    private windowRef: WindowRefService,
    private logger: LoggerService,
    private router: Router,
    private zone: NgZone
  ) { }

  public ngOnInit() {
    gapi.signin2.render('google-login-button', {
      scope: 'profile email',
      onsuccess: googleUser => this.onSuccess(googleUser),
      onfailure: googleUser => this.onFailure(googleUser)
    });
  }

  private onSuccess(googleUser) {
    const token = googleUser.getAuthResponse().id_token;
    this.zone.run(() => {
      this.tokenService.setToken(token);
      this.routeTo();
    });
  }

  private onFailure(error) {
    this.logger.error(error);

    this.tokenService.removeToken();
  }

  private routeTo() {
    this.zone.run(() => {
      this.router.navigate(['/dashboard/applicant/enrolment']);
    });
  }
}
