import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaperEnrolleeReturneesComponent } from './paper-enrollee-returnees.component';

describe('PaperEnrolleeReturneesComponent', () => {
  let component: PaperEnrolleeReturneesComponent;
  let fixture: ComponentFixture<PaperEnrolleeReturneesComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PaperEnrolleeReturneesComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PaperEnrolleeReturneesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
