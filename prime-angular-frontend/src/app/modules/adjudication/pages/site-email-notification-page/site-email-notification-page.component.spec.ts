import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteEmailNotificationPageComponent } from './site-email-notification-page.component';

describe('SiteEmailNotificationPageComponent', () => {
  let component: SiteEmailNotificationPageComponent;
  let fixture: ComponentFixture<SiteEmailNotificationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteEmailNotificationPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteEmailNotificationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
