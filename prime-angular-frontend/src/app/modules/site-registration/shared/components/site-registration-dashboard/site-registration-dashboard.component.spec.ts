import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteRegistrationDashboardComponent } from './site-registration-dashboard.component';

describe('SiteRegistrationDashboardComponent', () => {
  let component: SiteRegistrationDashboardComponent;
  let fixture: ComponentFixture<SiteRegistrationDashboardComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteRegistrationDashboardComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegistrationDashboardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
