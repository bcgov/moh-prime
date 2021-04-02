import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VaccinationLoginComponent } from './vaccination-login.component';

describe('VaccinationLoginComponent', () => {
  let component: VaccinationLoginComponent;
  let fixture: ComponentFixture<VaccinationLoginComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VaccinationLoginComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VaccinationLoginComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
