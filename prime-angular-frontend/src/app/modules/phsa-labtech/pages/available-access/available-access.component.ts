import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import { RouteUtils } from '@lib/utils/route-utils.class';

import { PhsaLabtechRoutes } from '@phsa/phsa-labtech.routes';

@Component({
  selector: 'app-available-access',
  templateUrl: './available-access.component.html',
  styleUrls: ['./available-access.component.scss']
})
export class AvailableAccessComponent implements OnInit {

  public form: FormGroup;
  public busy: Subscription;

  private routeUtils: RouteUtils;


  constructor(
    protected fb: FormBuilder,
    protected route: ActivatedRoute,
    protected router: Router,
  ) {
    this.routeUtils = new RouteUtils(route, router, PhsaLabtechRoutes.MODULE_PATH);
  }


  public onSubmit(): void {
    this.routeUtils.routeRelativeTo(PhsaLabtechRoutes.SUBMISSION_CONFIRMATION);
  }

  public routeBackTo() {
    this.routeUtils.routeRelativeTo(PhsaLabtechRoutes.DEMOGRAPHIC);
  }


  ngOnInit(): void {

    this.form = this.fb.group({
    });


  }

}
