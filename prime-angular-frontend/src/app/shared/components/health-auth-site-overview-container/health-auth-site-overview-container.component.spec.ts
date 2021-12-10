import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthAuthSiteOverviewContainerComponent } from './health-auth-site-overview-container.component';

describe('HealthAuthOverviewContainerComponent', () => {
  let component: HealthAuthSiteOverviewContainerComponent;
  let fixture: ComponentFixture<HealthAuthSiteOverviewContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HealthAuthSiteOverviewContainerComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthSiteOverviewContainerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
