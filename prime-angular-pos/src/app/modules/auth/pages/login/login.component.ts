import { Component, OnInit, Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material';

import { Subscription } from 'rxjs';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';

import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';

import { AuthProvider } from '@auth/shared/enum/auth-provider.enum';
import { AuthService } from '@auth/shared/services/auth.service';
import { DataResource } from '@auth/shared/resources/data-resource.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public busy: Subscription;
  public data: any;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private route: ActivatedRoute,
    private dialog: MatDialog,
    private authService: AuthService,
    private dataResource: DataResource
  ) { }

  public onAction() {
    this.requestAccess();
  }

  public ngOnInit() {
    const hasLoggedIn = this.route.snapshot.params.login;

    if (hasLoggedIn) {
      this.busy = this.dataResource.getData()
        // TODO handle response to display GPID
        .subscribe((data: any) => this.data = data);
    }
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
