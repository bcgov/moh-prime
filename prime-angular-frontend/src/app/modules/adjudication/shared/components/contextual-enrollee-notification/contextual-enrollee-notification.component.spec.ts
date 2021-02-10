import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ContextualEnrolleeNotificationComponent } from './contextual-enrollee-notification.component';

describe('ContextualEnrolleeNotificationComponent', () => {
  let component: ContextualEnrolleeNotificationComponent;
  let fixture: ComponentFixture<ContextualEnrolleeNotificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ContextualEnrolleeNotificationComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ContextualEnrolleeNotificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
