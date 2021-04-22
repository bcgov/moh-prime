import { AdjudicationRoutes } from '@adjudication/adjudication.routes';
import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { HealthAuthorityResource } from '@core/resources/health-authority-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HAAuthorizedUser } from '@shared/models/ha-authorized-user.model';
import { Role } from '@auth/shared/enum/role.enum';

import { HaAuthorizedUserEntryFormState } from './ha-authorized-user-entry-form-state.class';

@Component({
  selector: 'app-ha-authorized-user-entry',
  templateUrl: './ha-authorized-user-entry.component.html',
  styleUrls: ['./ha-authorized-user-entry.component.scss']
})
export class HaAuthorizedUserEntryComponent implements OnInit {
  public formState: HaAuthorizedUserEntryFormState;
  public routeUtils: RouteUtils;
  public busy: Subscription;
  public haid: number;
  public auid: number;

  public Role = Role;

  constructor(
    private fb: FormBuilder,
    private formUtilsService: FormUtilsService,
    private route: ActivatedRoute,
    private healthAuthorityResource: HealthAuthorityResource,
    private router: Router
  ) {
    this.routeUtils = new RouteUtils(route, router, AdjudicationRoutes.HEALTH_AUTHORITIES);
    this.haid = this.route.snapshot.params.haid;
    this.auid = this.route.snapshot.params.auid;
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.formState.form)) {
      const user = this.formState.json;
      if (this.auid) {
        this.busy = this.healthAuthorityResource.updateAuthorizedUser(this.haid, this.auid, user)
          .subscribe(() => this.routeUtils.routeRelativeTo(['../', AdjudicationRoutes.AUTHORIZED_USERS]));
      } else {
        this.busy = this.healthAuthorityResource.createAuthorizedUser(this.haid, user)
          .subscribe(() => this.routeUtils.routeRelativeTo(['../', AdjudicationRoutes.AUTHORIZED_USERS]));
      }
    }
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(['../', AdjudicationRoutes.AUTHORIZED_USERS]);
  }

  public ngOnInit(): void {
    this.createFormInstance();
    if (this.auid) {
      this.getAuthorizedUser();
    }
  }

  protected getAuthorizedUser() {
    this.busy = this.healthAuthorityResource.getAuthorizedUserById(this.haid, this.auid)
      .subscribe((user: HAAuthorizedUser) => this.formState.patchValue(user))
  }

  protected createFormInstance() {
    this.formState = new HaAuthorizedUserEntryFormState(this.fb);
  }
}
