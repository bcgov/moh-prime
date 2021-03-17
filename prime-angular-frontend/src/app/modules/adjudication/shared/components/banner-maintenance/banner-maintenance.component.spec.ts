import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BannerMaintenanceComponent } from './banner-maintenance.component';

describe('BannerMaintenanceComponent', () => {
  let component: BannerMaintenanceComponent;
  let fixture: ComponentFixture<BannerMaintenanceComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BannerMaintenanceComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BannerMaintenanceComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
