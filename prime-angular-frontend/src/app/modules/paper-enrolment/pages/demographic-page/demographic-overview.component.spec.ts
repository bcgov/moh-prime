import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DemographicOverviewComponent } from './demographic-overview.component';

describe('DemographicOverviewComponent', () => {
  let component: DemographicOverviewComponent;
  let fixture: ComponentFixture<DemographicOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DemographicOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DemographicOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
