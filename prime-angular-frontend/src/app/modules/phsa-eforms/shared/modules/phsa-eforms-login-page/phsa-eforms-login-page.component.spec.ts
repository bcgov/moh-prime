import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PhsaEformsLoginPageComponent } from './phsa-eforms-login-page.component';

describe('PhsaEformsLoginPageComponent', () => {
  let component: PhsaEformsLoginPageComponent;
  let fixture: ComponentFixture<PhsaEformsLoginPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ PhsaEformsLoginPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(PhsaEformsLoginPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
