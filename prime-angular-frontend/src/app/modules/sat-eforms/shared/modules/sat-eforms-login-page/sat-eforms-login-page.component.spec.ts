import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SatEformsLoginPageComponent } from './sat-eforms-login-page.component';

describe('SatEformsLoginPageComponent', () => {
  let component: SatEformsLoginPageComponent;
  let fixture: ComponentFixture<SatEformsLoginPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SatEformsLoginPageComponent ]
    }).compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SatEformsLoginPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
