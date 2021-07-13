import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RemoteUsersOverviewComponent } from './remote-users-overview.component';

describe('RemoteUsersOverviewComponent', () => {
  let component: RemoteUsersOverviewComponent;
  let fixture: ComponentFixture<RemoteUsersOverviewComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ RemoteUsersOverviewComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(RemoteUsersOverviewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
