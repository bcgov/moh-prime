import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup } from '@angular/forms';

import { Subject, Subscription } from 'rxjs';

import { Contact } from '@lib/models/contact.model';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { ContactFormState } from '@lib/classes/contact-form-state.class';

import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-technical-supports-page',
  templateUrl: './technical-supports-page.component.html',
  styleUrls: ['./technical-supports-page.component.scss']
})
export class TechnicalSupportsPageComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public form: FormGroup;
  public routeUtils: RouteUtils;
  public contacts: Contact[];
  public formSubmittingEvent: Subject<void>;

  constructor(
    private fb: FormBuilder,
    private haResource: HealthAuthorityResource,
    private formUtilsService: FormUtilsService,
    route: ActivatedRoute,
    router: Router
  ) {
    this.title = route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.MODULE_PATH);
    this.formSubmittingEvent = new Subject<void>();
  }

  public onBack(): void {
  }

  public onSubmit(): void {
  }

  public ngOnInit(): void {
    this.createFormInstance();
  }

  private createFormInstance() {
    this.form = new ContactFormState(this.fb, this.formUtilsService).form;
  }

}
