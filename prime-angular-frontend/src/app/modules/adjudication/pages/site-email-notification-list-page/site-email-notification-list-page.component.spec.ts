import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteEmailNotificationListPageComponent } from './site-email-notification-list-page.component';

describe('SiteEmailNotificationListPageComponent', () => {
  let component: SiteEmailNotificationListPageComponent;
  let fixture: ComponentFixture<SiteEmailNotificationListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteEmailNotificationListPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteEmailNotificationListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
