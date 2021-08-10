import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DemographicOverviewComponent } from './demographic-overview.component';
import { RouterTestingModule } from '@angular/router/testing';

describe('DemographicOverviewComponent', () => {
  let component: DemographicOverviewComponent;
  let fixture: ComponentFixture<DemographicOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [
        RouterTestingModule
      ],
      declarations: [
        DemographicOverviewComponent
      ]
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
