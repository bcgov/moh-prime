import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';

import { Subscription } from 'rxjs';

import { ToastService } from '@core/services/toast.service';

import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-organization-information',
  templateUrl: './organization-information.component.html',
  styleUrls: ['./organization-information.component.scss']
})
export class OrganizationInformationComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private toastService: ToastService
  ) { }

  public get siteName(): FormControl {
    return this.form.get('siteName') as FormControl;
  }

  public get doingBusinessAs(): FormControl {
    return this.form.get('doingBusinessAs') as FormControl;
  }

  public onSubmit() {
    // TODO proper submission when backend payload known
    // if (this.form.valid) { }
    this.toastService.openSuccessToast('Enrolment information has been saved');
    this.form.markAsPristine();
    this.router.navigate([SiteRoutes.SITE_ADDRESS], { relativeTo: this.route.parent });
  }

  public onBack() {
    this.router.navigate([SiteRoutes.MULTIPLE_SITES], { relativeTo: this.route.parent });
  }

  public ngOnInit() {
    this.createFormInstance();
  }

  private createFormInstance() {
    // TODO rename form fields when backend payload known
    this.form = this.fb.group({
      siteName: [
        null,
        [Validators.required]
      ],
      doingBusinessAs: [
        null,
        [Validators.required]
      ]
    });
  }
}
