import { Component, OnInit } from '@angular/core';
import { KeycloakService } from 'keycloak-angular';
import { Router } from '@angular/router';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.scss']
})
export class InfoComponent implements OnInit {

  constructor(private keycloakService: KeycloakService, router: Router) {
    if (keycloakService.getUserRoles().includes('prime_user')) {
      router.navigate(['/enrolment/profile'])
    }
  }

  ngOnInit() {
  }

  public bcscLogin() {
    this.keycloakService.login({
      idpHint: 'bcsc',
      redirectUri: 'http://localhost:4200'
    })
  }
}
