import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteInformationPageComponent } from './site-information-page.component';

describe('SiteInformationPageComponent', () => {
  let component: SiteInformationPageComponent;
  let fixture: ComponentFixture<SiteInformationPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteInformationPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteInformationPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
