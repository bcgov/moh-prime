import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { AdjudicationDashboardComponent } from './adjudication-dashboard.component';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

describe('AdjudicationDashboardComponent', () => {
  let component: AdjudicationDashboardComponent;
  let fixture: ComponentFixture<AdjudicationDashboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [AdjudicationDashboardComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(AdjudicationDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
