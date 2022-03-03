import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimeEmailComponent } from './prime-email.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { KeycloakService } from 'keycloak-angular';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('PrimeEmailComponent', () => {
  let component: PrimeEmailComponent;
  let fixture: ComponentFixture<PrimeEmailComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          PrimeEmailComponent
        ],
        providers: [
          {
            provide: APP_CONFIG,
            useValue: APP_DI_CONFIG
          },
          KeycloakService
        ],
        schemas: [CUSTOM_ELEMENTS_SCHEMA]
      }
    ).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrimeEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
