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
    if (this.isEnrolmentValid()) {
      const payload = this.enrolmentStateService.getEnrolment();
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
    } else {
      // TODO: indicate where validation failed in the review to prompt user
    }
  }

  public isEnrolmentValid(): boolean {
    return (
      this.isProfileInfoValid() &&
      this.isContactInfoValid() &&
      this.isProfessionalInfoValid() &&
      this.isSelfDeclarationValid() &&
      this.isPharmaNetAccessValid()
    );
  }

  public isProfileInfoValid(): boolean {
    return this.enrolmentStateService.profileForm.valid;
  }

  public isContactInfoValid(): boolean {
    return this.enrolmentStateService.contactForm.valid;
  }

  public isProfessionalInfoValid(): boolean {
    return this.enrolmentStateService.professionalInfoForm.valid;
  }

  public isSelfDeclarationValid(): boolean {
    return this.enrolmentStateService.selfDeclarationForm.valid;
  }

  public isPharmaNetAccessValid(): boolean {
    return this.enrolmentStateService.pharmaNetAccessForm.valid;
  }

  public showYesNo(declared: boolean) {
    return (declared) ? 'Yes' : 'No';
  }

  public route(route: string) {
    this.router.navigate(['enrolment', route]);
  }

  public ngOnInit() {
    this.enrolment = this.enrolmentStateService.getEnrolment();

    // TODO: indicate where validation failed in the review to prompt user
    console.log('PROFILE_VALID', this.enrolmentStateService.profileForm.valid);
    console.log('CONTACT_VALID', this.enrolmentStateService.contactForm.valid);
    console.log('PROFESSIONAL_VALID', this.enrolmentStateService.professionalInfoForm.valid);
    console.log('DECLARATION_VALID', this.enrolmentStateService.selfDeclarationForm.valid);
    console.log('ACCESS_VALID', this.enrolmentStateService.pharmaNetAccessForm.valid);
  }
}
