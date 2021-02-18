import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { ConfigService } from '@config/config.service';
import { GisEnrolmentFormStateService } from '@gis/shared/services/gis-enrolment-form-state.service';

@Component({
  selector: 'app-enrollee-information-page',
  templateUrl: './enrollee-information-page.component.html',
  styleUrls: ['./enrollee-information-page.component.scss']
})
export class EnrolleeInformationPageComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public form: FormGroup;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private formStateService: GisEnrolmentFormStateService,
    private configService: ConfigService
  ) {
    this.title = route.snapshot.data.title;
  }

  public get phone(): FormControl {
    return this.form.get('phone') as FormControl;
  }

  public get email(): FormControl {
    return this.form.get('email') as FormControl;
  }

  public onSubmit() {

  }

  public ngOnInit(): void {
    this.form = this.formStateService.enrolleeInformationPageFormState.form;
  }
}
