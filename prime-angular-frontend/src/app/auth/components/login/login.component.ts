import { Component, OnInit } from '@angular/core';
import { AuthResource } from 'src/app/core/resources/auth-resource.service';
declare const gapi: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  constructor(private authResource: AuthResource) {
  }

  ngOnInit() {
    gapi.signin2.render('google-login-button', {
      'scope': 'profile email',
      'onsuccess': this.onSuccess,
      'onfailure': this.onFailure
    });
  }

  onSuccess(googleUser) {
    var id_token = googleUser.getAuthResponse().id_token;
    this.authResource.login({ token: id_token }).subscribe(() => {
      // TODO: redirect to success page
    });
  }
  onFailure(error) {
    alert(JSON.stringify(error, undefined, 2));
  }
}