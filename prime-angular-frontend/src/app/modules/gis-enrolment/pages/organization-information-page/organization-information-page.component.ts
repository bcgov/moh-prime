import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { ConfigService } from '@config/config.service';
import { GisEnrolmentFormStateService } from '@gis/shared/services/gis-enrolment-form-state.service';

@Component({
  selector: 'app-organization-information-page',
  templateUrl: './organization-information-page.component.html',
  styleUrls: ['./organization-information-page.component.scss']
})
export class OrganizationInformationPageComponent implements OnInit {
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

  public get organization(): FormControl {
    return this.form.get('organization') as FormControl;
  }

  public get role(): FormControl {
    return this.form.get('role') as FormControl;
  }

  public onSubmit() {

  }

  public ngOnInit(): void {
    this.form = this.formStateService.organizationInformationPageFormState.form;
  }
}
