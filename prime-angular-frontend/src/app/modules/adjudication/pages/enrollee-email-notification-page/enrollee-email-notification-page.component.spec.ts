import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeEmailNotificationPageComponent } from './enrollee-email-notification-page.component';

describe('EnrolleeEmailNotificationPageComponent', () => {
  let component: EnrolleeEmailNotificationPageComponent;
  let fixture: ComponentFixture<EnrolleeEmailNotificationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrolleeEmailNotificationPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeEmailNotificationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
