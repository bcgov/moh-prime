import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthAuthSiteRegLoginPageComponent } from './health-auth-site-reg-login-page.component';

describe('HealthAuthSiteRegLoginPageComponent', () => {
  let component: HealthAuthSiteRegLoginPageComponent;
  let fixture: ComponentFixture<HealthAuthSiteRegLoginPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HealthAuthSiteRegLoginPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthSiteRegLoginPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
