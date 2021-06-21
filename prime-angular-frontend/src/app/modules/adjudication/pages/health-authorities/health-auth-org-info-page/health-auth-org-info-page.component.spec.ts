import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthAuthOrgInfoPageComponent } from './health-auth-org-info-page.component';

describe('HealthAuthOrgInfoPageComponent', () => {
  let component: HealthAuthOrgInfoPageComponent;
  let fixture: ComponentFixture<HealthAuthOrgInfoPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [HealthAuthOrgInfoPageComponent]
    })
      .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthOrgInfoPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
