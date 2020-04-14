import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteAddressComponent } from './site-address.component';

describe('SiteAddressComponent', () => {
  let component: SiteAddressComponent;
  let fixture: ComponentFixture<SiteAddressComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteAddressComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteAddressComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
