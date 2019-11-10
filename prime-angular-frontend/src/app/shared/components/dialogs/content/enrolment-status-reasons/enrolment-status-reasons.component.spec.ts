import { async, ComponentFixture, TestBed, inject } from '@angular/core/testing';

import { MockEnrolmentService } from 'test/mocks/mock-enrolment.service';

import { EnrolmentStatusReasonsComponent } from './enrolment-status-reasons.component';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';

describe('EnrolmentStatusReasonsComponent', () => {
  let component: EnrolmentStatusReasonsComponent;
  let fixture: ComponentFixture<EnrolmentStatusReasonsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule(
      {
        declarations: [
          EnrolmentStatusReasonsComponent
        ],
        providers: [
          {
            provide: EnrolmentService,
            useClass: MockEnrolmentService
          }
        ]
      }
    ).compileComponents();
  }));

  beforeEach(inject([EnrolmentService], (enrolmentService: EnrolmentService) => {
    fixture = TestBed.createComponent(EnrolmentStatusReasonsComponent);
    component = fixture.componentInstance;
    component.data = { enrolment: enrolmentService.enrolment };
    fixture.detectChanges();
  }));

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
