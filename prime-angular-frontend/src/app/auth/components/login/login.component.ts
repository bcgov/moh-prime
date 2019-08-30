import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { AuthResource } from 'src/app/core/resources/auth-resource.service';

declare const gapi: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  constructor(
    private authResource: AuthResource,
    private router: Router
  ) { }

  ngOnInit() {
    gapi.signin2.render('google-login-button', {
      scope: 'profile email',
      onsuccess: this.onSuccess,
      onfailure: this.onFailure
    });
  }

  onSuccess(googleUser) {
    const token = googleUser.getAuthResponse().id_token;
    this.authResource.login({ token })
      .subscribe(() => {
        // NOTE: intentionally set role based on route for the purpose
        // of the MVP requirements not requiring admin authentication
        // TODO: use configuration for routes
        this.router.navigate(['/dashboard/applicants/enrollment'], {
          state: { isApplicant: true }
        });
      });
  }
  onFailure(error) {
    alert(JSON.stringify(error, undefined, 2));
  }
}
