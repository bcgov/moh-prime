import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EmailNotificationViewPageComponent } from './email-notification-view-page.component';

describe('EmailNotificationViewPageComponent', () => {
  let component: EmailNotificationViewPageComponent;
  let fixture: ComponentFixture<EmailNotificationViewPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EmailNotificationViewPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmailNotificationViewPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
