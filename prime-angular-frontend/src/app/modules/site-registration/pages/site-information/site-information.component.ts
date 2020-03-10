import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@enrolment/shared/services/form-utils.service';
import { FormControlValidators } from '@shared/validators/form-control.validators';
import { SiteRoutes } from '../../site-registration.routes';

@Component({
  selector: 'app-site-information',
  templateUrl: './site-information.component.html',
  styleUrls: ['./site-information.component.scss']
})
export class SiteInformationComponent implements OnInit {
  form: FormGroup;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected toastService: ToastService,
    protected logger: LoggerService,
    protected utilService: UtilsService,
    private formUtilsService: FormUtilsService,
    private formBuilder: FormBuilder
  ) { }

  public get siteName(): FormControl {
    return this.form.get('siteName') as FormControl;
  }

  public get doingBusinessAs(): FormControl {
    return this.form.get('doingBusinessAs') as FormControl;
  }

  public get street(): FormControl {
    return this.form.get('street') as FormControl;
  }

  public get city(): FormControl {
    return this.form.get('city') as FormControl;
  }

  public get postal(): FormControl {
    return this.form.get('postal') as FormControl;
  }

  ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  protected createFormInstance() {
    this.form = this.formBuilder.group({
      siteName: [null, []],
      doingBusinessAs: [null, []],
      street: [{ value: null, disabled: false }, []],
      city: [{ value: null, disabled: false }, []],
      postal: [{ value: null, disabled: false }, []]
    });
  }

  protected initForm() {

  }

  onSubmit() {
    this.toastService.openSuccessToast('Enrolment information has been saved');
    this.form.markAsPristine();
    this.router.navigate([SiteRoutes.HOURS_OPERATION], { relativeTo: this.route.parent });
  }

  onBack() {
    this.router.navigate([SiteRoutes.MULTIPLE_SITES], { relativeTo: this.route.parent });
  }

}
