import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeToaComparePageComponent } from './enrollee-toa-compare-page.component';

describe('EnrolleeToaComparePageComponent', () => {
  let component: EnrolleeToaComparePageComponent;
  let fixture: ComponentFixture<EnrolleeToaComparePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrolleeToaComparePageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeToaComparePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
