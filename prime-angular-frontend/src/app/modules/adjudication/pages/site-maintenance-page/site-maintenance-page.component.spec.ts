import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteMaintenancePageComponent } from './site-maintenance-page.component';

describe('SiteMaintenancePageComponent', () => {
  let component: SiteMaintenancePageComponent;
  let fixture: ComponentFixture<SiteMaintenancePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteMaintenancePageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteMaintenancePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
