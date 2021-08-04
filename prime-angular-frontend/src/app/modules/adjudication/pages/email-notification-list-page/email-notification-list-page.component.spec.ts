import { ComponentFixture, TestBed } from '@angular/core/testing';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { EmailNotificationListPageComponent } from './email-notification-list-page.component';

describe('EmailNotificationListPageComponent', () => {
  let component: EmailNotificationListPageComponent;
  let fixture: ComponentFixture<EmailNotificationListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [EmailNotificationListPageComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
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
