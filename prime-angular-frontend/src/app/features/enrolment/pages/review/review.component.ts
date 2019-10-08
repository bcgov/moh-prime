import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

import { LoggerService } from '@core/services/logger.service';
import { Enrolment } from '../../shared/models/enrolment.model';
import { EnrolmentStateService } from '../../shared/services/enrolment-state.service';
import { EnrolmentResourceService } from '../../shared/services/enrolment-resource.service';
import { ToastService } from '@core/services/toast.service';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrls: ['./review.component.scss']
})
export class ReviewComponent implements OnInit {
  // TODO: make a proper enrolment model
  public enrolment: Enrolment;

  constructor(
    private router: Router,
    private enrolmentStateService: EnrolmentStateService,
    private enrolmentResource: EnrolmentResourceService,
    private toastService: ToastService,
    private logger: LoggerService
  ) { }

  public onSubmit() {
    // TODO: perform validation on the entire application
    // TODO: indicate where validation failed in the review to prompt user
    // if (this.form.valid) {
    //   const payload = this.enrolmentStateService.getEnrolment();
    //   this.enrolmentResource.submitEnrolment(payload)
    //     .subscribe(
    //       (enrolment: Enrolment) => {
    //         this.toastService.openSuccessToast('Enrolment has been submitted');
    //         this.form.markAsPristine();
    //         this.router.navigate(['professional'], { relativeTo: this.route.parent });
    //       },
    //       (error: any) => {
    //         this.toastService.openSuccessToast('Enrolment could not be submitted');
    //         this.logger.error('[Enrolment] Review::onSubmit error has occurred: ', error);
    //       });
    // } else {
    //   this.form.markAllAsTouched();
    // }
  }

  public showYesNo(declared: boolean) {
    return (declared) ? 'Yes' : 'No';
  }

  public route(route: string) {
    this.router.navigate(['enrolment', route]);
  }

  public ngOnInit() {
    this.enrolment = this.enrolmentStateService.getEnrolment();
  }
}
