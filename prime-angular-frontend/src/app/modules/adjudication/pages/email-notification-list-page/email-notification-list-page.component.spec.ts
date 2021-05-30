import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailNotificationListPageComponent } from './email-notification-list-page.component';

describe('EmailNotificationListPageComponent', () => {
  let component: EmailNotificationListPageComponent;
  let fixture: ComponentFixture<EmailNotificationListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmailNotificationListPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailNotificationListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
