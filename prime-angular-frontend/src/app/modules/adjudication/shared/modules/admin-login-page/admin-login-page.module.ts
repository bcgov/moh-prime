import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardModule } from '@lib/modules/dashboard/dashboard.module';
import { SharedModule } from '@shared/shared.module';

import { AdminLoginPageRoutingModule } from './admin-login-page-routing.module';
import { AdminLoginPageComponent } from './admin-login-page.component';

@NgModule({
  declarations: [AdminLoginPageComponent],
  imports: [
    SharedModule,
    DashboardModule,
    AdminLoginPageRoutingModule
  ]
})
export class AdminLoginPageModule { }
