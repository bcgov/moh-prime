import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BannerListViewComponent } from './banner-list-view.component';

describe('BannerListViewComponent', () => {
  let component: BannerListViewComponent;
  let fixture: ComponentFixture<BannerListViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BannerListViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BannerListViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
