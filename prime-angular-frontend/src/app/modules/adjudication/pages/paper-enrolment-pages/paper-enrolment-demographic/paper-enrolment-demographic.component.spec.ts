import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaperEnrolmentDemographicComponent } from './paper-enrolment-demographic.component';

describe('PaperEnrolmentDemographicComponent', () => {
  let component: PaperEnrolmentDemographicComponent;
  let fixture: ComponentFixture<PaperEnrolmentDemographicComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PaperEnrolmentDemographicComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PaperEnrolmentDemographicComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
