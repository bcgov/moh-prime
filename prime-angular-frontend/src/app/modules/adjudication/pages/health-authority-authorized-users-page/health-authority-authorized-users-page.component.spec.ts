import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HealthAuthorityAuthorizedUsersPageComponent } from './health-authority-authorized-users-page.component';

describe('HealthAuthorityAuthorizedUsersPageComponent', () => {
  let component: HealthAuthorityAuthorizedUsersPageComponent;
  let fixture: ComponentFixture<HealthAuthorityAuthorizedUsersPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HealthAuthorityAuthorizedUsersPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HealthAuthorityAuthorizedUsersPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
