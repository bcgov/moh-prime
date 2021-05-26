import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteRegistrationTabsComponent } from './site-registration-tabs.component';

describe('SiteRegistrationTabsComponent', () => {
  let component: SiteRegistrationTabsComponent;
  let fixture: ComponentFixture<SiteRegistrationTabsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteRegistrationTabsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRegistrationTabsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
