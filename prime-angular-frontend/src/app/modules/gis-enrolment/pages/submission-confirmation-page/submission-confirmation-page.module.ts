import { NgModule } from '@angular/core';

import { SharedModule } from '@shared/shared.module';

import { SubmissionConfirmationPageRoutingModule } from './submission-confirmation-page-routing.module';
import { SubmissionConfirmationPageComponent } from './submission-confirmation-page.component';

@NgModule({
  declarations: [SubmissionConfirmationPageComponent],
  imports: [
    SharedModule,
    SubmissionConfirmationPageRoutingModule
  ]
})
export class SubmissionConfirmationPageModule { }
