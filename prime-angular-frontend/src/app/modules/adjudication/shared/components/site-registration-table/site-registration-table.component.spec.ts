import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KeycloakService } from 'keycloak-angular';

import { SiteRegistrationTableComponent } from './site-registration-table.component';
import { AuthenticationService } from '@auth/shared/services/authentication.service';

describe('SiteRegistrationTableComponent', () => {
  let component: SiteRegistrationTableComponent;
  let fixture: ComponentFixture<SiteRegistrationTableComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      imports: [

      ],
      providers: [
        AuthenticationService,
        KeycloakService
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegistrationTableComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
