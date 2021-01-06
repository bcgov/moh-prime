import { waitForAsync, ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardRouteMenuItemComponent } from './dashboard-route-menu-item.component';
import { DashboardRouteMenuItem } from '../../models/dashboard-menu-item.model';

describe('DashboardRouteMenuItemComponent', () => {
  let component: DashboardRouteMenuItemComponent;
  let fixture: ComponentFixture<DashboardRouteMenuItemComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [DashboardRouteMenuItemComponent]
    }).compileComponents();
  }));

  beforeEach(() => {
    const routeMenuItem = new DashboardRouteMenuItem('Example', 'example');
    fixture = TestBed.createComponent(DashboardRouteMenuItemComponent);
    component = fixture.componentInstance;
    component.routeMenuItem = routeMenuItem;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
