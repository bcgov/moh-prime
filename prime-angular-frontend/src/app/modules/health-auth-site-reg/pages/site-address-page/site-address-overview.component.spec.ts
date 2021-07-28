import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteAddressOverviewComponent } from './site-address-overview.component';

describe('SiteAddressOverviewComponent', () => {
  let component: SiteAddressOverviewComponent;
  let fixture: ComponentFixture<SiteAddressOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteAddressOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteAddressOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
