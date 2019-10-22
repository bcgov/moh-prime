import { Component, OnInit } from '@angular/core';
import { KeycloakService } from 'keycloak-angular';

@Component({
  selector: 'app-info',
  templateUrl: './info.component.html',
  styleUrls: ['./info.component.scss']
})
export class InfoComponent implements OnInit {

  constructor(private keycloakService: KeycloakService) { }

  ngOnInit() {
  }
 
  public bcscLogin() {
    this.keycloakService.login({
      idpHint: 'bcsc',
      redirectUri: 'http://localhost:4200/enrolment/profile'
    })
  }
}
