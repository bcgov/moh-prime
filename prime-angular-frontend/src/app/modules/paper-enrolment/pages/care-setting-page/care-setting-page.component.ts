import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';

import { Observable, of } from 'rxjs';
import { tap, exhaustMap } from 'rxjs/operators';

import { AbstractEnrolmentPage } from '@lib/classes/abstract-enrolment-page.class';
import { RouteUtils } from '@lib/utils/route-utils.class';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { HttpEnrollee } from '@shared/models/enrolment.model';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';
import { OboSite } from '@enrolment/shared/models/obo-site.model';

import { PaperEnrolmentResource } from '@paper-enrolment/services/paper-enrolment-resource.service';
import { PaperEnrolmentRoutes } from '@paper-enrolment/paper-enrolment.routes';
import { CareSettingFormState } from './care-setting-form-state.class';
import { NoContent } from '@core/resources/abstract-resource';

@Component({
  selector: 'app-care-setting-page',
  templateUrl: './care-setting-page.component.html',
  styleUrls: ['./care-setting-page.component.scss']
})
export class CareSettingPageComponent extends AbstractEnrolmentPage implements OnInit, OnDestroy {
  public formState: CareSettingFormState;
  public enrollee: HttpEnrollee;
  public careSettingTypes: Config<number>[];
  public filteredCareSettingTypes: Config<number>[];
  public healthAuthorities: Config<number>[];
  public routeUtils: RouteUtils;

  constructor(
    protected dialog: MatDialog,
    protected formUtilsService: FormUtilsService,
    private fb: FormBuilder,
    private configService: ConfigService,
    private paperEnrolmentResource: PaperEnrolmentResource,
    private route: ActivatedRoute,
    router: Router
  ) {
    super(dialog, formUtilsService);

    this.careSettingTypes = this.configService.careSettings;
    this.healthAuthorities = this.configService.healthAuthorities;
    this.routeUtils = new RouteUtils(route, router, PaperEnrolmentRoutes.MODULE_PATH);
  }

  public onBack() {
    this.routeUtils.routeRelativeTo([PaperEnrolmentRoutes.DEMOGRAPHIC]);
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const canDeactivate = super.canDeactivate();
    return (canDeactivate instanceof Observable)
      ? canDeactivate.pipe(tap(() => this.formState.removeIncompleteCareSettings()))
      : canDeactivate;
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
    this.patchForm();
  }

  public ngOnDestroy(): void {
    // TODO is this required when there is no FormStateService
    this.formState.removeIncompleteCareSettings();
  }

  protected createFormInstance(): void {
    this.formState = new CareSettingFormState(this.fb, this.configService);
  }

  protected initForm(): void {
    // Always have at least one care setting ready for
    // the enrollee to fill out
    this.formState.addCareSetting();
  }

  protected patchForm(): void {
    const enrolleeId = +this.route.snapshot.params.eid;
    if (!enrolleeId) {
      return;
    }

    this.paperEnrolmentResource.getEnrolleeById(enrolleeId)
      .subscribe((enrollee: HttpEnrollee) => {
        this.enrollee = enrollee;
        const { enrolleeCareSettings, enrolleeHealthAuthorities } = enrollee;
        const careSettings = enrolleeCareSettings;
        this.formState.patchValue({
          // TODO renamed to match Enrolment model, but should be refactored to use enrolleeCareSettings to match HttpEnrollee
          careSettings,
          enrolleeHealthAuthorities
        });
      });
  }

  // TODO refactor logic this is quite awkward to understand, and NoContent return type
  protected performSubmission(): NoContent {
    this.formState.form.markAsPristine();
    let oboSites = this.enrollee.oboSites;
    const payload = this.formState.json;

    // Remove health authorities if health authority care setting not chosen
    if (!payload.careSettings.some(code => code === CareSettingEnum.HEALTH_AUTHORITY)) {
      payload.healthAuthorities = [];
    }

    // Remove any oboSites belonging to careSetting which is no longer selected
    this.careSettingTypes.forEach(type => {
      if (!payload.careSettings.some(code => code === type.code)) {
        oboSites = oboSites.filter((site: OboSite) => site.careSettingCode !== type.code);
      }
    });

    // When an individual health authority is deselected the OBO Sites should be removed
    oboSites = this.removeUnselectedHealthAuthOboSites(payload.healthAuthorities, oboSites);

    return this.paperEnrolmentResource.updateCareSettings(this.enrollee.id, payload)
      .pipe(
        exhaustMap(() => {
          if (this.enrollee.oboSites.length !== oboSites.length) {
            this.enrollee.oboSites = oboSites;
            return this.paperEnrolmentResource.updateOboSites(this.enrollee.id, oboSites);
          } else {
            return of(null);
          }
        }
        )
      );
  }

  protected afterSubmitIsSuccessful(): void {
    const routePath = (this.enrollee.oboSites?.length)
      ? PaperEnrolmentRoutes.OBO_SITES
      : PaperEnrolmentRoutes.REGULATORY;
    this.routeUtils.routeRelativeTo([routePath]);
  }

  private removeUnselectedHealthAuthOboSites(healthAuthorities: number[], oboSites: OboSite[]): OboSite[] {
    // TODO refactor and return directly this is inefficient and hard to read
    this.configService.healthAuthorities.forEach((healthAuthority, index) => {
      if (!healthAuthorities[index]) {
        for (let i = oboSites.length - 1; i >= 0; i--) {
          const oboSiteForm = oboSites[i];
          if (oboSiteForm.healthAuthorityCode === healthAuthority.code) {
            oboSites.splice(i, 1);
          }
        }
      }
    });

    return oboSites;
  }

}
