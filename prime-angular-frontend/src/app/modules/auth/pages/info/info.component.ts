import { Injectable, Inject } from '@angular/core';
import { Component, OnInit } from '@angular/core';
import { KeycloakService } from 'keycloak-angular';
import { Router } from '@angular/router';
import { APP_CONFIG, AppConfig } from 'app/app-config.module';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.scss']
})
export class InfoComponent implements OnInit {

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private keycloakService: KeycloakService,
    router: Router) {
    if (keycloakService.getUserRoles().includes('prime_user')) {
      router.navigate(['/enrolment/profile']);
    }
  }

  ngOnInit() {
  }

  public bcscLogin() {
    this.keycloakService.login({
      idpHint: 'bcsc',
      redirectUri: this.config.loginRedirectUrl
    });
  }
}
