import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SiteRemoteUsersComponent } from './site-remote-users.component';

describe('SiteRemoteUsersComponent', () => {
  let component: SiteRemoteUsersComponent;
  let fixture: ComponentFixture<SiteRemoteUsersComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SiteRemoteUsersComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SiteRemoteUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
