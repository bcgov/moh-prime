import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimeLogoComponent } from './prime-logo.component';

describe('PrimeLogoComponent', () => {
  let component: PrimeLogoComponent;
  let fixture: ComponentFixture<PrimeLogoComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrimeLogoComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrimeLogoComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
