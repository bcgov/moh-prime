import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContextualSiteNotificationComponent } from './contextual-site-notification.component';

describe('ContextualSiteNotificationComponent', () => {
  let component: ContextualSiteNotificationComponent;
  let fixture: ComponentFixture<ContextualSiteNotificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContextualSiteNotificationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContextualSiteNotificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
