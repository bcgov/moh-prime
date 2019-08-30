import { Component, OnInit } from '@angular/core';
declare const gapi: any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  constructor() { }

  ngOnInit() {
    gapi.signin2.render('google-login-button', {
      'scope': 'profile email',
      'onsuccess': this.onSuccess,
      'onfailure': this.onFailure
    });
  }

  onSuccess(googleUser) {
    var id_token = googleUser.getAuthResponse().id_token;

    // send token to backend
    // get new token
    // save token

    // redirect to success page
  }
  onFailure(error) {
    alert(JSON.stringify(error, undefined, 2));
  }
}