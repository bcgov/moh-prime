import { ComponentFixture, TestBed } from '@angular/core/testing';

import { VendorOverviewComponent } from './vendor-overview.component';

describe('VendorOverviewComponent', () => {
  let component: VendorOverviewComponent;
  let fixture: ComponentFixture<VendorOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ VendorOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(VendorOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
