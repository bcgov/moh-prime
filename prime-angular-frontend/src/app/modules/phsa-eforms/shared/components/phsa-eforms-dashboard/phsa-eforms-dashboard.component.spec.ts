import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { PhsaEformsDashboardComponent } from './phsa-eforms-dashboard.component';

describe('PhsaEformsDashboardComponent', () => {
  let component: PhsaEformsDashboardComponent;
  let fixture: ComponentFixture<PhsaEformsDashboardComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [PhsaEformsDashboardComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
      ],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhsaEformsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
