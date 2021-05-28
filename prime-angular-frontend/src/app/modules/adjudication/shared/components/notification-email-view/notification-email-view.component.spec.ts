import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotificationEmailViewComponent } from './notification-email-view.component';

describe('NotificationEmailViewComponent', () => {
  let component: NotificationEmailViewComponent;
  let fixture: ComponentFixture<NotificationEmailViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NotificationEmailViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NotificationEmailViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
