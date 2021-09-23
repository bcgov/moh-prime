import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SatEformsDashboardComponent } from './sat-eforms-dashboard.component';

describe('SatEformsDashboardComponent', () => {
  let component: SatEformsDashboardComponent;
  let fixture: ComponentFixture<SatEformsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SatEformsDashboardComponent ]
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
