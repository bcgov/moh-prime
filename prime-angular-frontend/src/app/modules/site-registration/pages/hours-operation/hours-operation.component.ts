import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@enrolment/shared/services/form-utils.service';
import { SiteRoutes } from '../../site-registration.routes';

@Component({
  selector: 'app-hours-operation',
  templateUrl: './hours-operation.component.html',
  styleUrls: ['./hours-operation.component.scss']
})
export class HoursOperationComponent implements OnInit {

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

  public get monday(): FormControl {
    return this.form.get('monday') as FormControl;
  }

  public get tuesday(): FormControl {
    return this.form.get('tuesday') as FormControl;
  }

  public get wednesday(): FormControl {
    return this.form.get('wednesday') as FormControl;
  }

  public get thursday(): FormControl {
    return this.form.get('thursday') as FormControl;
  }

  public get friday(): FormControl {
    return this.form.get('friday') as FormControl;
  }

  ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  protected createFormInstance() {
    this.form = this.formBuilder.group({
      monday: [null, []],
      tuesday: [null, []],
      wednesday: [null, []],
      thursday: [null, []],
      friday: [null, []]
    });
  }

  protected initForm() {

  }

  onSubmit() {
    this.toastService.openSuccessToast('Enrolment information has been saved');
    this.form.markAsPristine();
    this.router.navigate([SiteRoutes.VENDOR], { relativeTo: this.route.parent });
  }

  onBack() {
    this.router.navigate([SiteRoutes.SITE_INFORMATION], { relativeTo: this.route.parent });
  }

}
