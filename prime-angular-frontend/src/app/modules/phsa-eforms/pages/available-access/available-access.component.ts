import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription, of, noop } from 'rxjs';

import { BUSY_SUBMISSION_MESSAGE } from '@lib/constants';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { FormUtilsService } from '@core/services/form-utils.service';
import { BusyService } from '@lib/modules/ngx-busy/busy.service';

import { PartyTypeEnum } from '@phsa/shared/enums/party-type.enum';
import { PhsaEformsRoutes } from '@phsa/phsa-eforms.routes';
import { PhsaEformsResource } from '@phsa/shared/resources/phsa-eforms-resource.service';
import { PhsaEformsFormStateService } from '@phsa/shared/services/phsa-eforms-form-state.service';

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
    private phsaEformsResource: PhsaEformsResource,
    private phsaEformsFormStateService: PhsaEformsFormStateService,
    private formUtilsService: FormUtilsService,
    private busyService: BusyService
  ) {
    this.routeUtils = new RouteUtils(route, router, PhsaEformsRoutes.MODULE_PATH);
  }

  public get partyTypes(): FormGroup {
    return this.form.get('partyTypes') as FormGroup;
  }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      const payload = this.phsaEformsFormStateService.json;
      this.busy = of(noop).pipe(
        this.busyService.showMessagePipe(BUSY_SUBMISSION_MESSAGE, this.phsaEformsResource.createEnrollee(payload))
      )
        .subscribe(() => this.nextRoute());
    } else {
      this.hasNoRoleError = true;
    }
  }

  public onBack(): void {
    this.routeUtils.routeRelativeTo(PhsaEformsRoutes.DEMOGRAPHIC);
  }

  public nextRoute(): void {
    this.routeUtils.routeRelativeTo(PhsaEformsRoutes.SUBMISSION_CONFIRMATION);
  }

  public ngOnInit(): void {
    this.form = this.phsaEformsFormStateService.availableAccessFormState.form;
    this.showProgress = true;

    this.busy = this.phsaEformsResource
      .getPreApprovals(this.phsaEformsFormStateService.json.email)
      .subscribe((availablePartyTypeCodes: PartyTypeEnum[]) => {
        this.availablePartyTypes = availablePartyTypeCodes;
        this.phsaEformsFormStateService.availableAccessFormState.buildAvailableAccessFormControls(availablePartyTypeCodes);
        this.showProgress = false;
      });
  }
}
