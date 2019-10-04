import { NgModule } from '@angular/core';

import { EnrolmentRoutingModule } from './enrolment-routing.module';
import { SharedModule } from '@shared/shared.module';

import { ProfileComponent } from './pages/profile/profile.component';
import { ContactComponent } from './pages/contact/contact.component';
import { SelfDeclarationComponent } from './pages/self-declaration/self-declaration.component';
import { ProfessionalInfoComponent } from './pages/professional-info/professional-info.component';
import { PharmanetAccessComponent } from './pages/pharmanet-access/pharmanet-access.component';
import { ReviewComponent } from './pages/review/review.component';

import { SubHeaderComponent } from './shared/components/sub-header/sub-header.component';
import { DashboardComponent } from './shared/components/dashboard/dashboard.component';
import { PageTitleComponent } from './shared/components/page-title/page-title.component';

@NgModule({
  declarations: [
    ProfileComponent,
    ContactComponent,
    ProfessionalInfoComponent,
    SelfDeclarationComponent,
    PharmanetAccessComponent,
    ReviewComponent,
    SubHeaderComponent,
    DashboardComponent,
    PageTitleComponent
  ],
  imports: [
    SharedModule,
    EnrolmentRoutingModule
  ]
})
export class EnrolmentModule { }
