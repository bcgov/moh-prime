import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { PartyTypeEnum } from '@phsa/shared/enums/party-type.enum';

import { PhsaLabtechRoutes } from '@phsa/phsa-labtech.routes';
import { PhsaLabtechResource } from '@phsa/shared/services/phsa-labtech-resource.service';
import { PhsaFormStateService } from '@phsa/shared/services/phsa-labtech-form-state.service';

@Component({
  selector: 'app-available-access',
  templateUrl: './available-access.component.html',
  styleUrls: ['./available-access.component.scss']
})
export class AvailableAccessComponent implements OnInit {
  public form: FormGroup;
  public busy: Subscription;
  public showProgress: boolean;
  public availablePartyTypes: PartyTypeEnum[];
  public hasNoRoleError: boolean;

  private routeUtils: RouteUtils;

  constructor(
    protected fb: FormBuilder,
    protected route: ActivatedRoute,
    protected router: Router,
    private phsaLabtechResource: PhsaLabtechResource,
    private phsaFormStateService: PhsaFormStateService,
    private formUtilsService: FormUtilsService
  ) {
    this.routeUtils = new RouteUtils(route, router, PhsaLabtechRoutes.MODULE_PATH);
  }

  public get partyTypes(): FormGroup {
    return this.form.get('partyTypes') as FormGroup;
  }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      const payload = this.phsaFormStateService.json;
      this.busy = this.phsaLabtechResource.createEnrollee(payload)
        .subscribe(() => this.nextRoute());
    } else {
      this.hasNoRoleError = true;
    }
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(PhsaLabtechRoutes.DEMOGRAPHIC);
  }

  public nextRoute(): void {
    this.routeUtils.routeRelativeTo(PhsaLabtechRoutes.SUBMISSION_CONFIRMATION);
  }

  public ngOnInit(): void {
    this.form = this.phsaFormStateService.availableAccessFormState.form;
    this.showProgress = true;

    this.busy = this.phsaLabtechResource
      .getPreApprovals(this.phsaFormStateService.json.email)
      .subscribe((availablePartyTypeCodes: PartyTypeEnum[]) => {
        this.availablePartyTypes = availablePartyTypeCodes;
        this.phsaFormStateService.availableAccessFormState.buildAvailableAccessFormControls(availablePartyTypeCodes);
        this.showProgress = false;
      });
  }
}
