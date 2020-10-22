import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { KeycloakService } from 'keycloak-angular';

import { BceidComponent } from './bceid.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('BceidComponent', () => {
  let component: BceidComponent;
  let fixture: ComponentFixture<BceidComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [
        BceidComponent
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        KeycloakService
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BceidComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
