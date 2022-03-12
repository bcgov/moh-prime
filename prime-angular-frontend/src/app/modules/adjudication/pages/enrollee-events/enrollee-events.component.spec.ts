import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RouterTestingModule } from '@angular/router/testing';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { KeycloakService } from 'keycloak-angular';

import { EnrolleeEventsComponent } from './enrollee-events.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { AdjudicationModule } from '@adjudication/adjudication.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { BusinessEventTypeEnum } from '@adjudication/shared/models/business-event-type.model';

describe('EnrolleeEventsComponent', () => {
  let component: EnrolleeEventsComponent;
  let fixture: ComponentFixture<EnrolleeEventsComponent>;

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
    }).compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeEventsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('testing getBusinessEvents()', () => {
    describe('with businessEventTypes set to undefined', () => {
      it('component businessEventsType should not change', () => { });
    });


    describe('with businessEventType set properly', () => {
      it('component businessEventsType should change to the passed-in value', () => { });
    });

    describe('with getEnrolleeBusinessEvents completing', () => {
      it('should return an array of {date, content, name} objects')
    });

    describe('with getEnrolleeBusinessEvents not completing', () => { });
  });
});
