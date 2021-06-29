import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteBannerListPageComponent } from './site-banner-list-page.component';

describe('SiteBannerListPageComponent', () => {
  let component: SiteBannerListPageComponent;
  let fixture: ComponentFixture<SiteBannerListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteBannerListPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteBannerListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
