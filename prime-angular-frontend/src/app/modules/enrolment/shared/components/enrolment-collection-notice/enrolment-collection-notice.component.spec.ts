import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolmentCollectionNoticeComponent } from './enrolment-collection-notice.component';

describe('EnrolmentCollectionNoticeComponent', () => {
  let component: EnrolmentCollectionNoticeComponent;
  let fixture: ComponentFixture<EnrolmentCollectionNoticeComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EnrolmentCollectionNoticeComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolmentCollectionNoticeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
