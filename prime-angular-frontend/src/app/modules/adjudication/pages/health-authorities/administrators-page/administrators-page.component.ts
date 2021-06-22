import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { pipe } from 'rxjs';
import { map } from 'rxjs/operators';

import { Contact } from '@lib/models/contact.model';
import { AbstractContactsPage } from '@lib/classes/abstract-contacts-page.class';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { HealthAuthority } from '@shared/models/health-authority.model';

import { AdjudicationRoutes } from '@adjudication/adjudication.routes';

@Component({
  selector: 'app-administrators-page',
  templateUrl: './administrators-page.component.html',
  styleUrls: ['./administrators-page.component.scss']
})
export class AdministratorsPageComponent extends AbstractContactsPage implements OnInit {
  constructor(
    protected route: ActivatedRoute,
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    protected fb: FormBuilder,
    protected healthAuthResource: HealthAuthorityResource,
    router: Router
  ) {
    super(route, dialog, formUtilsService, fb, healthAuthResource, router);

    this.backRoute = AdjudicationRoutes.HEALTH_AUTH_TECHNICAL_SUPPORTS;
  }

  public ngOnInit(): void {
    this.init();
  }

  protected performSubmissionRequest(payload: Contact[]): NoContent {
    return this.healthAuthResource.updatePharmanetAdministrators(this.route.snapshot.params.haid, payload);
  }

  protected getContactsPipe() {
    return pipe(map(({ pharmanetAdministrators }: HealthAuthority) => pharmanetAdministrators));
  }
}
