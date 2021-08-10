import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { NO_ERRORS_SCHEMA } from '@angular/core';

import { KeycloakService } from 'keycloak-angular';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { AdjudicatorActionsComponent } from './adjudicator-actions.component';

describe('AdjudicatorActionsComponent', () => {
  let component: AdjudicatorActionsComponent;
  let fixture: ComponentFixture<AdjudicatorActionsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        AdjudicationModule
      ],
      providers: [
        KeycloakService,
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
      schemas: [NO_ERRORS_SCHEMA]
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdjudicatorActionsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
