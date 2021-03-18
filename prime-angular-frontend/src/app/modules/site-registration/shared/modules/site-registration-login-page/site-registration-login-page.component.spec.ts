import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteRegistrationLoginPageComponent } from './site-registration-login-page.component';

describe('SiteRegistrationLoginPageComponent', () => {
  let component: SiteRegistrationLoginPageComponent;
  let fixture: ComponentFixture<SiteRegistrationLoginPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteRegistrationLoginPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegistrationLoginPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
