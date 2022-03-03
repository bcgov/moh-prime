import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { RouterTestingModule } from '@angular/router/testing';

import { SiteRegistrationDashboardComponent } from './site-registration-dashboard.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

describe('SiteRegistrationDashboardComponent', () => {
  let component: SiteRegistrationDashboardComponent;
  let fixture: ComponentFixture<SiteRegistrationDashboardComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [SiteRegistrationDashboardComponent],
      imports: [
        RouterTestingModule
      ],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegistrationDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
