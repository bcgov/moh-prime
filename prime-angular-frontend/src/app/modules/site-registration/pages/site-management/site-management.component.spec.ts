import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteManagementComponent } from './site-management.component';

describe('SiteManagementComponent', () => {
  let component: SiteManagementComponent;
  let fixture: ComponentFixture<SiteManagementComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [SiteManagementComponent]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteManagementComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
