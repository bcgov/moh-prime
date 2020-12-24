import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { PhsaLabtechDashboardComponent } from './phsa-labtech-dashboard.component';

describe('PhsaLabtechDashboardComponent', () => {
  let component: PhsaLabtechDashboardComponent;
  let fixture: ComponentFixture<PhsaLabtechDashboardComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [PhsaLabtechDashboardComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        },
      ],
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PhsaLabtechDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
