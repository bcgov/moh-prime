import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimeEnrolmentAccessComponent } from './prime-enrolment-access.component';

describe('PrimeEnrolmentAccessComponent', () => {
  let component: PrimeEnrolmentAccessComponent;
  let fixture: ComponentFixture<PrimeEnrolmentAccessComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PrimeEnrolmentAccessComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PrimeEnrolmentAccessComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
