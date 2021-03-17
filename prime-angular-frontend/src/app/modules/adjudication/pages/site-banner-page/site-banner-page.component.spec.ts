import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteBannerPageComponent } from './site-banner-page.component';

describe('SiteBannerPageComponent', () => {
  let component: SiteBannerPageComponent;
  let fixture: ComponentFixture<SiteBannerPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteBannerPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteBannerPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
