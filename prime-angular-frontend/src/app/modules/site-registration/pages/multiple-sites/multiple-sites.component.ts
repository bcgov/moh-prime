import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@enrolment/shared/services/form-utils.service';
import { FormControlValidators } from '@shared/validators/form-control.validators';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { SiteRoutes } from '../../site-registration.routes';

@Component({
  selector: 'app-multiple-sites',
  templateUrl: './multiple-sites.component.html',
  styleUrls: ['./multiple-sites.component.scss']
})
export class MultipleSitesComponent implements OnInit {
  form: FormGroup;
  public decisions: { code: boolean, name: string }[];

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
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

  ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  protected createFormInstance() {
    this.form = this.formBuilder.group({
      hasMultipleSites: [null, [FormControlValidators.requiredBoolean]],
      organizationNumber: [null, []],
    });
  }

  protected initForm() {
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

  onSubmit() {
    this.toastService.openSuccessToast('Enrolment information has been saved');
    this.form.markAsPristine();
    this.router.navigate([SiteRoutes.SITE_INFORMATION], { relativeTo: this.route.parent });
  }
}
