import { ComponentFixture, TestBed } from '@angular/core/testing';

import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';
import { SatEformsDashboardComponent } from './sat-eforms-dashboard.component';

describe('SatEformsDashboardComponent', () => {
  let component: SatEformsDashboardComponent;
  let fixture: ComponentFixture<SatEformsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [SatEformsDashboardComponent],
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
    fixture = TestBed.createComponent(SatEformsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
