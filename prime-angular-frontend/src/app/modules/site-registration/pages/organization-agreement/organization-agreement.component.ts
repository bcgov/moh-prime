import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { ToastService } from '@core/services/toast.service';

import { SiteRoutes } from '@registration/site-registration.routes';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';

@Component({
  selector: 'app-organization-agreement',
  templateUrl: './organization-agreement.component.html',
  styleUrls: ['./organization-agreement.component.scss']
})
export class OrganizationAgreementComponent implements OnInit {
  public busy: Subscription;

  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteRegistrationResource: SiteRegistrationResource,
    private toastService: ToastService
  ) { }

  public onSubmit() {
    // TODO proper submission when backend payload known
    // if (this.form.valid) { }
    this.toastService.openSuccessToast('Enrolment information has been saved');
    this.router.navigate([SiteRoutes.VENDORS], { relativeTo: this.route.parent });
  }

  public onBack() {
    this.router.navigate([SiteRoutes.SITE_ADDRESS], { relativeTo: this.route.parent });
  }

  public ngOnInit(): void {
    // TODO change the footer if already signed
    this.siteRegistrationResource.getOrganizationAgreement()
  }
}
