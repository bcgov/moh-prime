import { Component, OnInit, Inject } from '@angular/core';
import { MatDialog } from '@angular/material';

import { Subscription, from, Observable } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';

import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';

import { AuthProvider } from '@auth/shared/enum/auth-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { GpidResource } from '@core/resources/gpid-resource.service';
import { exhaustMap } from 'rxjs/operators';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public busy: Subscription;
  public gpid: Observable<string>;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private dialog: MatDialog,
    private authService: AuthService,
    private gpidResource: GpidResource
  ) { }

  public onAction() {
    this.requestAccess();
  }

  public async ngOnInit() {
    this.gpid = from(this.authService.isLoggedIn())
      .pipe(
        exhaustMap(() => this.gpidResource.getGpid())
      );
  }

  private requestAccess() {
    const data: DialogOptions = {
      title: 'PRIME Access',
      imageSrc: '/assets/prime_logo_icon.svg',
      actionText: 'Login',
      message: 'In order to access PRIME you must have a BC Services Card to authenticate.',
      cancelHide: true
    };
    this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .subscribe((response: boolean) => {
        if (response) {
          this.login();
        }
      });
  }

  private login() {
    this.authService.login({
      idpHint: AuthProvider.BCSC,
      redirectUri: this.config.loginRedirectUrl
    });
  }
}
