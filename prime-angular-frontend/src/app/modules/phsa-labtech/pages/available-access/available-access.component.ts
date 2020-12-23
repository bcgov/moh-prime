import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, ValidatorFn } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';
import { PartyTypeEnum } from '@shared/enums/party-type.enum';

import { PhsaLabtechRoutes } from '@phsa/phsa-labtech.routes';
import { PhsaLabtechResource } from '@phsa/shared/services/phsa-labtech-resource.service';
import { PhsaFormStateService } from '@phsa/shared/services/phsa-labtech-form-state.service';
import { FormUtilsService } from '@core/services/form-utils.service';

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
    protected phsaFormStateService: PhsaFormStateService,
    private formUtilsService: FormUtilsService,
  ) {
    this.routeUtils = new RouteUtils(route, router, PhsaLabtechRoutes.MODULE_PATH);
  }


  public get partyTypes(): FormArray {
    return this.form.get('partyTypes') as FormArray;
  }

  public partyTypeText(partyType: PartyTypeEnum): string {
    return PartyTypeEnum.text(partyType);
  }

  public onSubmit(): void {
    if (this.formUtilsService.checkValidity(this.form)) {
      const phsaEnrollee = this.phsaFormStateService.json;

      // Turn bool array into type code array. Could make this a method for state service?
      phsaEnrollee.partyTypes = phsaEnrollee.partyTypes
        .map((bool, i) => (bool) ? this.availablePartyTypes[i] : 0)
        .filter(code => code != 0);

      this.busy = this.phsaLabtechResource.createEnrollee(phsaEnrollee)
        .subscribe(() =>
          this.routeUtils.routeRelativeTo(PhsaLabtechRoutes.SUBMISSION_CONFIRMATION)
        );
    }
  }

  public routeBackTo() {
    this.routeUtils.routeRelativeTo(PhsaLabtechRoutes.DEMOGRAPHIC);
  }

  ngOnInit(): void {
    this.form = this.phsaFormStateService.availableAccessForm;
    this.showProgress = true;
    this.busy = this.phsaLabtechResource.getPreApprovals(this.phsaFormStateService.json.email).subscribe(result => {
      this.availablePartyTypes = result;
      this.availablePartyTypes.forEach(partyType => this.partyTypes.push(this.fb.control(false)));
      this.showProgress = false;
    });
  }

}
