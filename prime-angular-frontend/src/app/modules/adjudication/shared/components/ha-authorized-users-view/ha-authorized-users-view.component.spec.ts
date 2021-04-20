import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HaAuthorizedUsersViewComponent } from './ha-authorized-users-view.component';

describe('HaAuthorizedUsersViewComponent', () => {
  let component: HaAuthorizedUsersViewComponent;
  let fixture: ComponentFixture<HaAuthorizedUsersViewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HaAuthorizedUsersViewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HaAuthorizedUsersViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
