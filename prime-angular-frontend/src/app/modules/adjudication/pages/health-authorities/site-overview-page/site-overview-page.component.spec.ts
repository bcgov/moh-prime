import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteOverviewPageComponent } from './site-overview-page.component';

describe('SiteOverviewPageComponent', () => {
  let component: SiteOverviewPageComponent;
  let fixture: ComponentFixture<SiteOverviewPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteOverviewPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteOverviewPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
