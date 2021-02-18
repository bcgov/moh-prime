import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { SubmissionConfirmationPageComponent } from './submission-confirmation-page.component';

export const route: string = 'confirmation';

const routes: Routes = [
  {
    path: '',
    component: SubmissionConfirmationPageComponent,
    data: { title: 'GIS Enrolment' }
  }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SubmissionConfirmationPageRoutingModule { }
