import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PrimeContactComponent } from './prime-contact.component';

describe('PrimeContactComponent', () => {
  let component: PrimeContactComponent;
  let fixture: ComponentFixture<PrimeContactComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ PrimeContactComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PrimeContactComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
