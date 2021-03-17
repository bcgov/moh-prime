import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeBannerPageComponent } from './enrollee-banner-page.component';

describe('EnrolleeBannerPageComponent', () => {
  let component: EnrolleeBannerPageComponent;
  let fixture: ComponentFixture<EnrolleeBannerPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrolleeBannerPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeBannerPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
