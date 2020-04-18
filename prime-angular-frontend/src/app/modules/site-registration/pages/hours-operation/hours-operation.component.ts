import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { ToastService } from '@core/services/toast.service';

import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-hours-operation',
  templateUrl: './hours-operation.component.html',
  styleUrls: ['./hours-operation.component.scss']
})
export class HoursOperationComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastService: ToastService,
    private formBuilder: FormBuilder
  ) { }

  public get weekends(): FormControl {
    return this.form.get('weekends') as FormControl;
  }

  public get allDay(): FormControl {
    return this.form.get('allDay') as FormControl;
  }

  public get specialHours(): FormControl {
    return this.form.get('specialHours') as FormControl;
  }

  public onSubmit() {
    // TODO proper submission when backend payload known
    // if (this.form.valid) { }
    this.toastService.openSuccessToast('Enrolment information has been saved');
    this.form.markAsPristine();
    this.router.navigate([SiteRoutes.VENDOR], { relativeTo: this.route.parent });
  }

  public onBack() {
    this.router.navigate([SiteRoutes.ORGANIZATION_AGREEMENT], { relativeTo: this.route.parent });
  }

  public ngOnInit() {
    this.createFormInstance();
  }

  private createFormInstance() {
    // TODO proper naming when backend payload known
    this.form = this.formBuilder.group({
      weekends: [null, []],
      allDay: [null, []],
      specialHours: [null, []]
    });
  }
}
