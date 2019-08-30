import { NgModule } from "@angular/core";

import { SharedModule } from "src/app/shared/shared.module";

import { ApplicantsRoutingModule } from "./applicants-routing.module";
import { EnrolmentComponent } from "./pages/enrolment/enrolment.component";
import { AuthenticateCompleteComponent } from "./pages/authenticate-complete/authenticate-complete.component";
import { AuthenticateInProgressComponent } from "./pages/authenticate-in-progress/authenticate-in-progress.component";
import { AuthenticateDeniedComponent } from "./pages/authenticate-denied/authenticate-denied.component";

@NgModule({
  declarations: [
    EnrolmentComponent,
    AuthenticateCompleteComponent,
    AuthenticateInProgressComponent,
    AuthenticateDeniedComponent
  ],
  imports: [SharedModule, ApplicantsRoutingModule]
})
export class ApplicantsModule {}
