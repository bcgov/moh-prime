import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { PartyTypeEnum } from '@shared/enums/party-type.enum';

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

  private routeUtils: RouteUtils;


  constructor(
    protected fb: FormBuilder,
    protected route: ActivatedRoute,
    protected router: Router,
    protected phsaLabtechResource: PhsaLabtechResource,
    protected enrolmentFormStateService: PhsaFormStateService
  ) {
    this.routeUtils = new RouteUtils(route, router, PhsaLabtechRoutes.MODULE_PATH);
  }


  public onSubmit(): void {
    this.busy = this.phsaLabtechResource.createEnrollee(
      this.enrolmentFormStateService.json).subscribe(() =>
        this.routeUtils.routeRelativeTo(PhsaLabtechRoutes.SUBMISSION_CONFIRMATION)
      );
  }

  public routeBackTo() {
    this.routeUtils.routeRelativeTo(PhsaLabtechRoutes.DEMOGRAPHIC);
  }


  ngOnInit(): void {
    this.showProgress = true;

    this.busy = this.phsaLabtechResource.getPreApprovals(this.enrolmentFormStateService.json).subscribe(result => {
      this.availablePartyTypes = result;
      this.showProgress = false;
    });

    this.form = this.fb.group({
    });
  }

}
