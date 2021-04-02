import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaccinationsDashboardComponent } from './vaccinations-dashboard.component';

describe('VaccinationsDashboardComponent', () => {
  let component: VaccinationsDashboardComponent;
  let fixture: ComponentFixture<VaccinationsDashboardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VaccinationsDashboardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VaccinationsDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
