import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthAuthCareTypesPageComponent } from './health-auth-care-types-page.component';

describe('HealthAuthCareTypesPageComponent', () => {
  let component: HealthAuthCareTypesPageComponent;
  let fixture: ComponentFixture<HealthAuthCareTypesPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HealthAuthCareTypesPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthCareTypesPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
