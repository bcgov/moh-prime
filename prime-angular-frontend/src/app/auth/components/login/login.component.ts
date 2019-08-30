import { Component, OnInit, OnDestroy, NgZone } from '@angular/core';
import { Router } from '@angular/router';

import { BehaviorSubject } from 'rxjs';

import { AuthTokenService } from 'src/app/core/services/auth-token.service';
import { LoggerService } from 'src/app/core/services/logger.service';
import { WindowRefService } from 'src/app/core/services/window-ref.service';
import { ThrowStmt } from '@angular/compiler';

declare const gapi: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit, OnDestroy {

  private isAuthenticated: BehaviorSubject<boolean>;

  constructor(
    private tokenService: AuthTokenService,
    private windowRef: WindowRefService,
    private logger: LoggerService,
    private router: Router,
    private zone: NgZone
  ) {
    this.isAuthenticated = new BehaviorSubject<boolean>(false);
  }

  public ngOnInit() {
    console.log('INIT', this.isAuthenticated.getValue());

    this.isAuthenticated.next(false);

    gapi.signin2.render('google-login-button', {
      scope: 'profile email',
      onsuccess: (googleUser) => this.onSuccess(googleUser),
      onfailure: (googleUser) => this.onFailure(googleUser)
    });

    this.isAuthenticated.subscribe((isAuthenticated) => {
      console.log(isAuthenticated);
      if (isAuthenticated) {
        this.routeTo();
      }
    });
  }

  public ngOnDestroy() {
    this.isAuthenticated.unsubscribe();
  }

  private onSuccess(googleUser) {
    this.logger.info('LOGGED IN!');

    const token = googleUser.getAuthResponse().id_token;
    this.tokenService.setToken(token);

    this.isAuthenticated.next(true);
  }

  private onFailure(error) {
    this.logger.error(error);

    this.tokenService.removeToken();
  }

  private routeTo() {
    this.zone.run(() => {
      this.router.navigate(['/dashboard/applicant/enrollment']);
    });
  }
}
