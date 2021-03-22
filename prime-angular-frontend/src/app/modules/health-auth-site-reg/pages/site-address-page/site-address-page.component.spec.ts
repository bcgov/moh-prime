import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteAddressPageComponent } from './site-address-page.component';

describe('SiteAddressPageComponent', () => {
  let component: SiteAddressPageComponent;
  let fixture: ComponentFixture<SiteAddressPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteAddressPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteAddressPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
