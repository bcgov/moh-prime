import { ComponentFixture, TestBed } from '@angular/core/testing';
import { APP_CONFIG, APP_DI_CONFIG } from 'app/app-config.module';

import { GisDashboardComponent } from './gis-dashboard.component';

xdescribe('GisDashboardComponent', () => {
  let component: GisDashboardComponent;
  let fixture: ComponentFixture<GisDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [GisDashboardComponent],
      providers: [
        {
          provide: APP_CONFIG,
          useValue: APP_DI_CONFIG
        }
      ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(GisDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
