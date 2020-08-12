import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';

import { NgxProgressModule } from '@lib/modules/ngx-progress/ngx-progress.module';

import { DashboardComponent } from './components/dashboard/dashboard.component';
import { DashboardHeaderComponent } from './components/dashboard-header/dashboard-header.component';
import { DashboardMenuComponent } from './components/dashboard-menu/dashboard-menu.component';
import { DashboardRouteMenuItemComponent } from './components/dashboard-route-menu-item/dashboard-route-menu-item.component';

@NgModule({
  declarations: [
    DashboardComponent,
    DashboardHeaderComponent,
    DashboardMenuComponent,
    DashboardRouteMenuItemComponent
  ],
  imports: [
    CommonModule,
    RouterModule,
    MatToolbarModule,
    MatSidenavModule,
    MatListModule,
    MatIconModule,
    NgxProgressModule
  ],
  exports: [
    DashboardComponent,
    DashboardHeaderComponent
  ]
})
export class DashboardModule { }
