import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';

import { Subscription } from 'rxjs';

import { ToastService } from '@core/services/toast.service';
import { Country } from '@shared/enums/country.enum';
import { Province } from '@shared/enums/province.enum';

import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-site-address',
  templateUrl: './site-address.component.html',
  styleUrls: ['./site-address.component.scss']
})
export class SiteAddressComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public formControlNames: string[];
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private fb: FormBuilder,
    private toastService: ToastService,
  ) {
    this.formControlNames = [
      'street',
      'city',
      'provinceCode',
      'postal'
    ];
  }

  public onSubmit() {
    // TODO proper submission when backend payload known
    // if (this.form.valid) { }
    this.toastService.openSuccessToast('Enrolment information has been saved');
    this.form.markAsPristine();
    this.router.navigate([SiteRoutes.HOURS_OPERATION], { relativeTo: this.route.parent });
  }

  public onBack() {
    this.router.navigate([SiteRoutes.ORGANIZATION_INFORMATION], { relativeTo: this.route.parent });
  }

  public ngOnInit() {
    this.createFormInstance();
  }

  private createFormInstance() {
    this.form = this.fb.group({
      street: [
        { value: null, disabled: false },
        [Validators.required]
      ],
      city: [
        { value: null, disabled: false },
        [Validators.required]
      ],
      provinceCode: [
        { value: Province.BRITISH_COLUMBIA, disabled: true },
        [Validators.required]
      ],
      postal: [
        { value: null, disabled: false },
        [Validators.required]
      ],
      countryCode: [
        { value: Country.CANADA, disabled: true },
        [Validators.required]
      ],
    });
  }
}
