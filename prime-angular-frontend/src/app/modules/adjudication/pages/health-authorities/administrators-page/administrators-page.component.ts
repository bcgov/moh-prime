import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { UntypedFormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Observable, pipe, UnaryFunction } from 'rxjs';
import { map } from 'rxjs/operators';

import { Contact } from '@lib/models/contact.model';
import { AbstractContactsPage } from '@lib/classes/abstract-contacts-page.class';
import { NoContent } from '@core/resources/abstract-resource';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { UtilsService } from '@core/services/utils.service';
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
    protected fb: UntypedFormBuilder,
    protected healthAuthResource: HealthAuthorityResource,
    protected utilsService: UtilsService,
    router: Router
  ) {
    super(route, dialog, formUtilsService, fb, healthAuthResource, utilsService, router);

    this.backRoute = AdjudicationRoutes.HEALTH_AUTH_TECHNICAL_SUPPORTS;
  }

  public ngOnInit(): void {
    this.cardTitlePrefix = 'PharmaNet Admin: ';
    this.init();
  }

  protected getContactsPipe(): UnaryFunction<Observable<HealthAuthority>, Observable<Contact[]>> {
    return pipe(map(({ pharmanetAdministrators }: HealthAuthority) => pharmanetAdministrators));
  }

  protected performSubmissionRequest(contact: Contact[]): NoContent {
    return this.healthAuthResource.updateHealthAuthorityPharmanetAdministrators(this.route.snapshot.params.haid, contact);
  }
}
