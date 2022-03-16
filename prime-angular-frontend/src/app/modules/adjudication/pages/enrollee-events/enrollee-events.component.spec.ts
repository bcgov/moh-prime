import faker from 'faker';

import { of } from 'rxjs';
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
import { BusinessEvent } from '@adjudication/shared/models/business-event.model';
import { DateContent } from '@adjudication/shared/components/dated-content-table/dated-content-table.component';

describe('EnrolleeEventsComponent', () => {
  let component: EnrolleeEventsComponent;
  let fixture: ComponentFixture<EnrolleeEventsComponent>;

  let mockBusinessEvents: BusinessEvent[];
  let mockDateContent: DateContent[];

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
    mockBusinessEvents = [
      {
        enrolleeId: faker.random.number(),
        adminId: faker.random.number(),
        adminIDIR: faker.random.word(),
        businessEventTypeCode: BusinessEventTypeEnum.ADMIN_ACTION_CODE,
        eventDate: null,
        description: faker.random.words(),
        partyName: faker.random.word()
      },
      {
        enrolleeId: faker.random.number(),
        adminId: faker.random.number(),
        adminIDIR: faker.random.word(),
        businessEventTypeCode: BusinessEventTypeEnum.ADMIN_ACTION_CODE,
        eventDate: null,
        description: faker.random.words(),
        partyName: faker.random.word()
      }
    ];
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  describe('testing getBusinessEvents()', () => {
    describe('passing \'undefined\' businessEventsTypes', () => {
      it('should keep the same value as the original business events', () => {

        component.getBusinessEvents(undefined);
        expect((component as any).businessEventTypes).toEqual([]);
      });
    });

    describe('passing 1 item in the businessEventsTypes array', () => {
      it('should change the value of the original businessEventsTypes', () => {
        const mockBusinessEventType = [BusinessEventTypeEnum.STATUS_CHANGE_CODE];

        component.getBusinessEvents(mockBusinessEventType);
        expect((component as any).businessEventTypes).not.toEqual([]);
        expect((component as any).businessEventTypes).toEqual(mockBusinessEventType);
      });
    });

    describe('with getEnrolleeBusinessEvents() returning no BusinessEvents', () => {
      it('should return an empty array', () => {
        spyOn((component as any).adjudicationResource, 'getEnrolleeBusinessEvents')
          .and.returnValue(of([]));
        mockDateContent = [];

        component.getBusinessEvents([]);
        component.businessEvents$.subscribe((dateContent) => {
          expect(dateContent).toEqual(mockDateContent);
        });
      });
    });

    describe('with getEnrolleeBusinessEvents() returning 1 BusinessEvent', () => {
      it('should return an array of one BusinessEvent', () => {
        spyOn((component as any).adjudicationResource, 'getEnrolleeBusinessEvents')
          .and.returnValue(of(mockBusinessEvents.slice(0, 1)));
        mockDateContent = [
          {
            date: mockBusinessEvents[0].eventDate,
            content: mockBusinessEvents[0].description,
            name: mockBusinessEvents[0].adminIDIR
          }
        ];

        component.getBusinessEvents([]);
        component.businessEvents$.subscribe((dateContent) => {
          expect(dateContent).toEqual(mockDateContent);
        });
      });
    });

    describe('with getEnrolleeBusinessEvents() returning multiple BusinessEvents', () => {
      it('should return an array of equal size to the one returned from getEnrolleeBusinessEvents()', () => {
        spyOn((component as any).adjudicationResource, 'getEnrolleeBusinessEvents')
          .and.returnValue(of(mockBusinessEvents));
        mockDateContent = mockBusinessEvents.map((businessEvent: BusinessEvent) => {
          return {
            date: businessEvent.eventDate,
            content: businessEvent.description,
            name: businessEvent.adminIDIR
          }
        });

        component.getBusinessEvents([]);
        component.businessEvents$.subscribe((dateContent) => {
          expect(dateContent).toEqual(mockDateContent);
        });
      });
    });
  });
});
