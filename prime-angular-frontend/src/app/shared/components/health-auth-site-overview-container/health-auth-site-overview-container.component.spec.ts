import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthAuthSiteOverviewContainerComponent } from './health-auth-site-overview-container.component';

describe('HealthAuthOverviewContainerComponent', () => {
  let component: HealthAuthSiteOverviewContainerComponent;
  let fixture: ComponentFixture<HealthAuthSiteOverviewContainerComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HealthAuthSiteOverviewContainerComponent],
      schemas: [CUSTOM_ELEMENTS_SCHEMA]
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
