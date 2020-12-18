import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeOverviewComponent } from './enrollee-overview.component';

describe('EnrolleeOverviewComponent', () => {
  let component: EnrolleeOverviewComponent;
  let fixture: ComponentFixture<EnrolleeOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrolleeOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
