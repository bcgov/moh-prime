import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';
import { MatDialogModule } from '@angular/material/dialog';

import { APP_CONFIG, AppConfig } from 'app/app-config.module';
import { SiteRegAccessComponent } from './site-reg-access.component';

describe('SiteRegAccessComponent', () => {
  let component: SiteRegAccessComponent;
  let fixture: ComponentFixture<SiteRegAccessComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      imports: [
        MatDialogModule
      ],
      declarations: [SiteRegAccessComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: AppConfig
        }
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
