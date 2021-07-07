import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteInformationOverviewComponent } from './site-information-overview.component';

describe('SiteInformationOverviewComponent', () => {
  let component: SiteInformationOverviewComponent;
  let fixture: ComponentFixture<SiteInformationOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteInformationOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteInformationOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
