import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';

import { map } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '../../shared/models/enrolment.model';
import { EnrolmentStateService } from '../../shared/services/enrolment-state.service';
import { EnrolmentResource } from '../../shared/services/enrolment-resource.service';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss']
})
export class ReviewComponent implements OnInit {
  // TODO: make a proper enrolment model
  public enrolment: Enrolment;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private enrolmentStateService: EnrolmentStateService,
    private enrolmentResource: EnrolmentResource,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public onSubmit() {
    if (this.enrolmentStateService.isEnrolmentValid()) {
      const payload = this.enrolmentStateService.enrolment;
      this.enrolmentResource.updateEnrolment(payload)
        .subscribe(
          () => {
            this.toastService.openSuccessToast('Enrolment has been submitted');
            this.router.navigate(['confirmation'], { relativeTo: this.route.parent });
          },
          (error: any) => {
            this.toastService.openErrorToast('Enrolment could not be submitted');
            this.logger.error('[Enrolment] Review::onSubmit error has occurred: ', error);
          });
    } else {
      // TODO: indicate where validation failed in the review to prompt user edits
      console.log('PROFILE', this.enrolmentStateService.isProfileInfoValid());
      console.log('CONTACT', this.enrolmentStateService.isContactInfoValid());
      console.log('PROFESSIONAL', this.enrolmentStateService.isProfessionalInfoValid());
      console.log('DECLARATION', this.enrolmentStateService.isSelfDeclarationValid());
      console.log('ACCESS', this.enrolmentStateService.isPharmaNetAccessValid());
    }
  }

  public showYesNo(declared: boolean) {
    return (declared === null) ? 'N/A'
      : (declared) ? 'Yes' : 'No';
  }

  public redirect(route: string) {
    this.router.navigate(['enrolment', route]);
  }

  public ngOnInit() {
    // TODO: detect enrolment already exists and don't reload
    // TODO: apply guard if not enrolment is found to redirect to profile
    this.enrolmentResource.enrolments()
      .pipe(
        map((enrolment: Enrolment) => this.enrolment = enrolment)
      )
      .subscribe((enrolment: Enrolment) => {
        if (enrolment) {
          this.enrolmentStateService.enrolment = enrolment;
        }
      });
  }
}
