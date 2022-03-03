import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { KeycloakService } from 'keycloak-angular';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { AdjudicatorEventsComponent } from './adjudicator-events.component';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';


describe('AdjudicatorEventsComponent', () => {
  let component: AdjudicatorEventsComponent;
  let fixture: ComponentFixture<AdjudicatorEventsComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        HttpClientTestingModule,
        RouterTestingModule,
        AdjudicationModule,
        BrowserAnimationsModule,
      ],
      declarations: [],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
        KeycloakService
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdjudicatorEventsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
