import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PaperEnrolleeReturneesPageComponent } from './paper-enrollee-returnees-page.component';

describe('PaperEnrolleeReturneesComponent', () => {
  let component: PaperEnrolleeReturneesPageComponent;
  let fixture: ComponentFixture<PaperEnrolleeReturneesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [PaperEnrolleeReturneesPageComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PaperEnrolleeReturneesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
