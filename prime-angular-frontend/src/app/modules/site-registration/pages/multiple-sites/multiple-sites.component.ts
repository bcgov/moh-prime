import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';

import { ToastService } from '@core/services/toast.service';
import { FormControlValidators } from '@shared/validators/form-control.validators';
import { FormUtilsService } from '@common/services/form-utils.service';

import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-multiple-sites',
  templateUrl: './multiple-sites.component.html',
  styleUrls: ['./multiple-sites.component.scss']
})
export class MultipleSitesComponent implements OnInit {
  public form: FormGroup;
  public decisions: { code: boolean, name: string }[];
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastService: ToastService,
    private formUtilsService: FormUtilsService,
    private formBuilder: FormBuilder
  ) {
    this.decisions = [
      { code: false, name: 'No' },
      { code: true, name: 'Yes' }
    ];
  }

  public get hasMultipleSites(): FormControl {
    return this.form.get('hasMultipleSites') as FormControl;
  }

  public get organizationNumber(): FormControl {
    return this.form.get('organizationNumber') as FormControl;
  }

  public onSubmit() {
    this.toastService.openSuccessToast('Enrolment information has been saved');
    this.form.markAsPristine();
    this.router.navigate([SiteRoutes.ORGANIZATION_INFORMATION], { relativeTo: this.route.parent });
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.formBuilder.group({
      hasMultipleSites: [null, [FormControlValidators.requiredBoolean]],
      organizationNumber: [null, []],
    });
  }

  private initForm() {
    this.hasMultipleSites.valueChanges
      .subscribe((value: boolean) => {
        this.toggleOrganizationValidators(value, this.organizationNumber);
      });
  }

  private toggleOrganizationValidators(value: boolean, control: FormControl) {
    if (!value) {
      this.formUtilsService.resetAndClearValidators(control);
    } else {
      this.formUtilsService.setValidators(control, [Validators.required]);
    }
  }
}
