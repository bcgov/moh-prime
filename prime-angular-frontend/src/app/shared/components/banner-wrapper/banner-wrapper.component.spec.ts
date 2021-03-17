import { ComponentFixture, TestBed } from '@angular/core/testing';

import { BannerWrapperComponent } from './banner-wrapper.component';

describe('BannerWrapperComponent', () => {
  let component: BannerWrapperComponent;
  let fixture: ComponentFixture<BannerWrapperComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ BannerWrapperComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(BannerWrapperComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
