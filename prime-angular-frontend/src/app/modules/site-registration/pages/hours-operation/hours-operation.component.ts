import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { ToastService } from '@core/services/toast.service';

import { SiteRoutes } from '@registration/site-registration.routes';

@Component({
  selector: 'app-hours-operation',
  templateUrl: './hours-operation.component.html',
  styleUrls: ['./hours-operation.component.scss']
})
export class HoursOperationComponent implements OnInit {
  public form: FormGroup;
  public amHours: string[];
  public pmHours: string[];
  public days: string[];
  public SiteRoutes = SiteRoutes;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastService: ToastService,
    private formBuilder: FormBuilder
  ) {
    this.amHours = [
      '1:00',
      '2:00',
      '3:00',
      '4:00',
      '5:00',
      '6:00',
      '7:00',
      '8:00',
      '9:00',
      '10:00',
      '11:00',
      '12:00'
    ];

    this.pmHours = [
      '13:00',
      '14:00',
      '15:00',
      '16:00',
      '17:00',
      '18:00',
      '19:00',
      '20:00',
      '21:00',
      '22:00',
      '23:00',
      '24:00'
    ];

    this.days = [
      'monday',
      'tuesday',
      'wednesday',
      'thursday',
      'friday',
      'saturday',
      'sunday'
    ];

  }

  public get mondayAM(): FormControl {
    return this.form.get('mondayAM') as FormControl;
  }

  public get mondayPM(): FormControl {
    return this.form.get('mondayPM') as FormControl;
  }

  public get tuesdayAM(): FormControl {
    return this.form.get('tuesdayAM') as FormControl;
  }

  public get tuesdayPM(): FormControl {
    return this.form.get('tuesdayPM') as FormControl;
  }

  public get wednesdayAM(): FormControl {
    return this.form.get('wednesdayAM') as FormControl;
  }

  public get wednesdayPM(): FormControl {
    return this.form.get('wednesdayPM') as FormControl;
  }

  public get thursdayAM(): FormControl {
    return this.form.get('thursdayAM') as FormControl;
  }

  public get thursdayPM(): FormControl {
    return this.form.get('thursdayPM') as FormControl;
  }

  public get fridayAM(): FormControl {
    return this.form.get('fridayAM') as FormControl;
  }

  public get fridayPM(): FormControl {
    return this.form.get('fridayPM') as FormControl;
  }

  public get saturdayAM(): FormControl {
    return this.form.get('saturdayAM') as FormControl;
  }

  public get saturdayPM(): FormControl {
    return this.form.get('saturdayPM') as FormControl;
  }

  public get sundayAM(): FormControl {
    return this.form.get('sundayAM') as FormControl;
  }

  public get sundayPM(): FormControl {
    return this.form.get('sundayPM') as FormControl;
  }

  public onSubmit() {
    this.toastService.openSuccessToast('Enrolment information has been saved');
    this.form.markAsPristine();
    this.router.navigate([SiteRoutes.VENDOR], { relativeTo: this.route.parent });
  }

  public onBack() {
    this.router.navigate([SiteRoutes.ORGANIZATION_INFORMATION], { relativeTo: this.route.parent });
  }

  public ngOnInit() {
    this.createFormInstance();
  }

  private createFormInstance() {
    this.form = this.formBuilder.group({
      mondayAM: [null, []],
      mondayPM: [null, []],
      tuesdayAM: [null, []],
      tuesdayPM: [null, []],
      wednesdayAM: [null, []],
      wednesdayPM: [null, []],
      thursdayAM: [null, []],
      thursdayPM: [null, []],
      fridayAM: [null, []],
      fridayPM: [null, []],
      saturdayAM: [null, []],
      saturdayPM: [null, []],
      sundayAM: [null, []],
      sundayPM: [null, []],
    });
  }
}
