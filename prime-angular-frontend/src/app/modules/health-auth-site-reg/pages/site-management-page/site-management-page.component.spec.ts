import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteManagementPageComponent } from './site-management-page.component';

describe('SiteManagementPageComponent', () => {
  let component: SiteManagementPageComponent;
  let fixture: ComponentFixture<SiteManagementPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SiteManagementPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteManagementPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
