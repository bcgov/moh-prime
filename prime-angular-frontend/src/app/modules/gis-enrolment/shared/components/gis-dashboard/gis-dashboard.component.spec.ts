import { ComponentFixture, TestBed } from '@angular/core/testing';

import { GisDashboardComponent } from './gis-dashboard.component';

describe('GisDashboardComponent', () => {
  let component: GisDashboardComponent;
  let fixture: ComponentFixture<GisDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ GisDashboardComponent ]
    })
    .compileComponents();
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
