import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolmentLogHistoryComponent } from './enrolment-log-history.component';

describe('EnrolmentLogHistoryComponent', () => {
  let component: EnrolmentLogHistoryComponent;
  let fixture: ComponentFixture<EnrolmentLogHistoryComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrolmentLogHistoryComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolmentLogHistoryComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
