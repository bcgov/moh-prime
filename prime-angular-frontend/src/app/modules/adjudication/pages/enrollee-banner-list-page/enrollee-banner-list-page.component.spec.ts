import { ComponentFixture, TestBed } from '@angular/core/testing';

import { EnrolleeBannerListPageComponent } from './enrollee-banner-list-page.component';

describe('EnrolleeBannerListPageComponent', () => {
  let component: EnrolleeBannerListPageComponent;
  let fixture: ComponentFixture<EnrolleeBannerListPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ EnrolleeBannerListPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EnrolleeBannerListPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
