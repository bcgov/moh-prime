import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoteUsersComponent } from './remote-users.component';

describe('RemoteUsersComponent', () => {
  let component: RemoteUsersComponent;
  let fixture: ComponentFixture<RemoteUsersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ RemoteUsersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoteUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
