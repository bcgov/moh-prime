import { Component, OnInit, OnDestroy } from '@angular/core';
import { FormGroup, FormArray } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';

import { map } from 'rxjs/operators';

import { ToastService } from '@core/services/toast.service';
import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { UtilsService } from '@core/services/utils.service';
import { FormUtilsService } from '@core/services/form-utils.service';

import { EnrolmentRoutes } from '@enrolment/enrolment.routes';
import { EnrolmentService } from '@enrolment/shared/services/enrolment.service';
import { EnrolmentResource } from '@enrolment/shared/services/enrolment-resource.service';
import { BaseEnrolmentProfilePage } from '@enrolment/shared/classes/enrolment-profile-page.class';
import { EnrolmentFormStateService } from '@enrolment/shared/services/enrolment-form-state.service';
import { CollegeCertification } from '@enrolment/shared/models/college-certification.model';
import { CareSetting } from '@enrolment/shared/models/care-setting.model';

import { RegulatoryFormState } from './regulatory-form-state';
import { AuthService } from '@auth/shared/services/auth.service';

@Component({
  selector: 'app-regulatory',
  templateUrl: './regulatory.component.html',
  styleUrls: ['./regulatory.component.scss']
})
export class RegulatoryComponent extends BaseEnrolmentProfilePage implements OnInit, OnDestroy {
  public formState: RegulatoryFormState;
  public cannotRequestRemoteAccess: boolean;

  constructor(
    protected route: ActivatedRoute,
    protected router: Router,
    protected dialog: MatDialog,
    protected enrolmentService: EnrolmentService,
    protected enrolmentResource: EnrolmentResource,
    protected enrolmentFormStateService: EnrolmentFormStateService,
    protected toastService: ToastService,
    protected logger: ConsoleLoggerService,
    protected utilService: UtilsService,
    protected formUtilsService: FormUtilsService,
    protected authService: AuthService
  ) {
    super(
      route,
      router,
      dialog,
      enrolmentService,
      enrolmentResource,
      enrolmentFormStateService,
      toastService,
      logger,
      utilService,
      formUtilsService,
      authService
    );

    this.cannotRequestRemoteAccess = false;
  }

  public get certifications(): FormArray {
    return this.formState.certifications as FormArray;
  }

  public get selectedCollegeCodes(): number[] {
    return this.certifications.value
      .map((certification: CollegeCertification) => +certification.collegeCode);
  }

  public addEmptyCollegeCertification() {
    this.formState.addCollegeCertification();
  }

  /**
   * @description
   * Removes a certification from the list in response to an
   * emitted event from college certifications. Does not allow
   * the list of certifications to empty.
   *
   * @param index to be removed
   */
  public removeCertification(index: number) {
    this.certifications.removeAt(index);
  }

  public routeBackTo() {
    this.routeTo(EnrolmentRoutes.CARE_SETTING);
  }

  public ngOnInit() {
    this.createFormInstance();
    this.patchForm().subscribe(() => this.initForm());
  }

  public ngOnDestroy() {
    this.removeIncompleteCertifications(true);
  }

  protected createFormInstance() {
    this.formState = this.enrolmentFormStateService.regulatoryFormState;
    this.form = this.formState.form;
  }

  protected initForm() {
    // Always have at least one certification ready for
    // the enrollee to fill out
    if (!this.certifications.length) {
      this.addEmptyCollegeCertification();
    }

    const initialRemoteAccess = this.canRequestRemoteAccess();

    this.form.valueChanges
      .pipe(map((_) => initialRemoteAccess && !this.isInitialEnrolment))
      .subscribe((couldRequestRemoteAccess: boolean) =>
        this.cannotRequestRemoteAccess = couldRequestRemoteAccess && !this.canRequestRemoteAccess()
      );
  }

  protected onSubmitFormIsValid() {
    // Enrollees can not have certifications and jobs
    this.removeJobs();
    // Remove remote access data when enrollee is no longer elegible, e.g. licence type changes
    if (this.cannotRequestRemoteAccess) {
      this.removeRemoteAccessData();
    }
  }

  protected afterSubmitIsSuccessful() {
    this.removeIncompleteCertifications(true);
  }

  protected nextRouteAfterSubmit() {
    const certifications = this.formState.collegeCertifications;
    const careSettings = this.enrolmentFormStateService.careSettingsForm.get('careSettings').value as CareSetting[];

    let nextRoutePath: string;
    if (!this.isProfileComplete) {
      nextRoutePath = (!this.certifications.length)
        ? EnrolmentRoutes.OBO_SITES
        : (this.enrolmentService.canRequestRemoteAccess(certifications, careSettings))
          ? EnrolmentRoutes.REMOTE_ACCESS
          : EnrolmentRoutes.SELF_DECLARATION;
    }

    super.nextRouteAfterSubmit(nextRoutePath);
  }

  /**
   * @description
   * Removes incomplete certifications from the list in preparation
   * for submission, and allows for an empty list of certifications.
   */
  private removeIncompleteCertifications(noEmptyCert: boolean = false) {
    this.certifications.controls
      .forEach((control: FormGroup, index: number) => {
        // Remove if college code is "None" or the group is invalid
        if (!control.get('collegeCode').value || control.invalid) {
          this.removeCertification(index);
        }
      });

    // Always have a single cerfication available, and it prevents
    // the page from jumping too much when routing
    if (!noEmptyCert && !this.certifications.controls.length) {
      this.addEmptyCollegeCertification();
    }
  }

  /**
   * @description
   * Remove obo sites/jobs from the enrolment as enrollees can not have
   * certificate(s), as well as, job(s).
   */
  private removeJobs() {
    this.removeIncompleteCertifications(true);

    if (this.certifications.length) {
      const form = this.enrolmentFormStateService.jobsForm;
      const oboSites = form.get('oboSites') as FormArray;
      oboSites.clear();
    }
  }

  private canRequestRemoteAccess(): boolean {
    const certifications = this.enrolmentFormStateService.regulatoryFormState.collegeCertifications;
    const careSettings = this.enrolmentFormStateService.careSettingsForm.get('careSettings').value;

    return this.enrolmentService
      .canRequestRemoteAccess(certifications, careSettings);
  }

  private removeRemoteAccessData(): void {
    const remoteAccessForm = this.enrolmentFormStateService.remoteAccessForm;
    const remoteAccessSites = remoteAccessForm.get('remoteAccessSites') as FormArray;
    const enrolleeRemoteUsers = remoteAccessForm.get('enrolleeRemoteUsers') as FormArray;
    const remoteLocations = this.enrolmentFormStateService.remoteAccessLocationsForm.get('remoteAccessLocations') as FormArray;
    [remoteAccessSites, enrolleeRemoteUsers, remoteLocations].forEach(f => f.clear());
  }
}
