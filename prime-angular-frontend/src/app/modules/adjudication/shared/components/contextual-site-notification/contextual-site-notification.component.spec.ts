import { ComponentFixture, TestBed } from '@angular/core/testing';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { ContextualSiteNotificationComponent } from './contextual-site-notification.component';

describe('ContextualSiteNotificationComponent', () => {
  let component: ContextualSiteNotificationComponent;
  let fixture: ComponentFixture<ContextualSiteNotificationComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ContextualSiteNotificationComponent],
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
    fixture = TestBed.createComponent(ContextualSiteNotificationComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
