import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { ConfigService } from '@config/config.service';
import { GisEnrolmentFormStateService } from '@gis/shared/services/gis-enrolment-form-state.service';

@Component({
  selector: 'app-ldap-information-page',
  templateUrl: './ldap-information-page.component.html',
  styleUrls: ['./ldap-information-page.component.scss']
})
export class LdapInformationPageComponent implements OnInit {
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

  public get ldapUsername(): FormControl {
    return this.form.get('ldapUsername') as FormControl;
  }

  public get ldapPassword(): FormControl {
    return this.form.get('ldapPassword') as FormControl;
  }

  public onSubmit() {

  }

  public ngOnInit(): void {
    this.form = this.formStateService.ldapInformationPageFormState.form;
    console.log('LDAP_INFO_BEFORE', this.formStateService.test);
    this.formStateService.test = false;
    console.log('LDAP_INFO_AFTER', this.formStateService.test);
  }
}
