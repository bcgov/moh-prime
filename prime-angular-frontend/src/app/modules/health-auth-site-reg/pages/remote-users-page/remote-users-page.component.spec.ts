import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoteUsersPageComponent } from './remote-users-page.component';

describe('RemoteUsersPageComponent', () => {
  let component: RemoteUsersPageComponent;
  let fixture: ComponentFixture<RemoteUsersPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RemoteUsersPageComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoteUsersPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
