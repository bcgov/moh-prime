import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthAuthorityAuthorizedUserPageComponent } from './health-authority-authorized-user-page.component';

describe('HealthAuthorityAuthorizedUserPageComponent', () => {
  let component: HealthAuthorityAuthorizedUserPageComponent;
  let fixture: ComponentFixture<HealthAuthorityAuthorizedUserPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HealthAuthorityAuthorizedUserPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthorityAuthorizedUserPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
