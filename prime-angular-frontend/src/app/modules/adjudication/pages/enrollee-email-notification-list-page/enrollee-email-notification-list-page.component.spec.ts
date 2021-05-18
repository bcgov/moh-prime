import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeEmailNotificationListPageComponent } from './enrollee-email-notification-list-page.component';

describe('EnrolleeEmailNotificationListPageComponent', () => {
  let component: EnrolleeEmailNotificationListPageComponent;
  let fixture: ComponentFixture<EnrolleeEmailNotificationListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrolleeEmailNotificationListPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeEmailNotificationListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
