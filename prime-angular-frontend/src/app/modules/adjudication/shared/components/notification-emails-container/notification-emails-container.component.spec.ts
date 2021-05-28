import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotificationEmailsContainerComponent } from './notification-emails-container.component';

describe('NotificationEmailsContainerComponent', () => {
  let component: NotificationEmailsContainerComponent;
  let fixture: ComponentFixture<NotificationEmailsContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NotificationEmailsContainerComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(NotificationEmailsContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
