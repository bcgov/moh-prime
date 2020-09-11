import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormArray, FormControl } from '@angular/forms';

import { Subscription, of, noop } from 'rxjs';
import { exhaustMap } from 'rxjs/operators';

import { FormArrayValidators } from '@lib/validators/form-array.validators';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';
import { AddressPipe } from '@shared/pipes/address.pipe';

import { SiteRoutes } from '@registration/site-registration.routes';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { SiteFormStateService } from '@registration/shared/services/site-form-state.service';
import { SiteService } from '@registration/shared/services/site.service';
import { RemoteUser } from '@registration/shared/models/remote-user.model';
import { Site } from '@registration/shared/models/site.model';

@Component({
  selector: 'app-remote-users',
  templateUrl: './remote-users.component.html',
  styleUrls: ['./remote-users.component.scss']
})
export class RemoteUsersComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public title: string;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;
  public hasNoRemoteUserError: boolean;
  public hasNoEmailError: boolean;
  public submitButtonText: string;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteService: SiteService,
    private siteResource: SiteResource,
    private siteFormStateService: SiteFormStateService,
    private formUtilsService: FormUtilsService,
    private addressPipe: AddressPipe
  ) {
    this.title = this.route.snapshot.data.title;
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.submitButtonText = 'Save and Continue';
  }

  public get remoteUsers(): FormArray {
    return this.form.get('remoteUsers') as FormArray;
  }

  public get hasRemoteUsers(): FormControl {
    return this.form.get('hasRemoteUsers') as FormControl;
  }

  public getRemoteUserProperties(remoteUser: FormGroup) {
    const remoteUserCertifications = remoteUser.controls?.remoteUserCertifications as FormArray;
    const remoteUserLocations = remoteUser.controls?.remoteUserLocations as FormArray;

    const firstLocation = remoteUserLocations.value[0].physicalAddress;
    firstLocation.provinceCode = 'BC';

    const collegeLicence = remoteUserCertifications.length > 1
      ? 'More than one college licence'
      : remoteUserCertifications.length === 0
        ? 'No college licence'
        : remoteUserCertifications.value[0].licenseNumber;

    const remoteAddress = remoteUserLocations.controls?.length > 1
      ? 'More than one remote address'
      : this.addressPipe.transform(firstLocation);

    return [
      {
        key: 'College Licence',
        value: collegeLicence
      },
      {
        key: 'Remote Address',
        value: remoteAddress
      },
    ];
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      this.hasNoRemoteUserError = false;
      const payload = this.siteFormStateService.json;
      const site = this.siteService.site;
      const newRemoteUsers = this.siteFormStateService.remoteUsersForm.value.remoteUsers.reduce((
        newRemoteUsersAcc: RemoteUser[], updated: RemoteUser) => {
        if (!site.remoteUsers.find((current: RemoteUser) =>
          current.firstName === updated.firstName &&
          current.lastName === updated.lastName &&
          current.email === updated.email
        )) {
          newRemoteUsersAcc.push(updated);
        }
        return newRemoteUsersAcc;
      }, []);

      this.busy = this.siteResource
        .updateSite(payload)
        .pipe(
          exhaustMap(() =>
            (site.submittedDate)
              ? this.siteResource.sendRemoteUsersEmailAdmin(site.id)
              : of(noop)
          ),
          exhaustMap(() =>
            (site.submittedDate && newRemoteUsers)
              ? this.siteResource.sendRemoteUsersEmailUser(site.id, newRemoteUsers)
              : of(noop)
          )
        )
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    } else {
      this.hasNoRemoteUserError = true;
    }
  }

  public onRemove(index: number) {
    this.remoteUsers.removeAt(index);
  }

  public onEdit(index: number) {
    this.routeUtils.routeRelativeTo(['../', SiteRoutes.REMOTE_USERS, index]);
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(['../', SiteRoutes.HOURS_OPERATION]);
  }

  public nextRoute() {
    if (this.isCompleted) {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
    } else {
      this.routeUtils.routeRelativeTo(SiteRoutes.ADMINISTRATOR);
    }
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
    if (this.siteService.site.submittedDate) {
      this.submitButtonText = 'Save and Submit';
    }
  }

  private createFormInstance() {
    this.form = this.siteFormStateService.remoteUsersForm;
  }

  private initForm() {
    this.remoteUsers.valueChanges
      .subscribe((remoteUsers: RemoteUser[]) => {
        (remoteUsers.length)
          ? this.hasRemoteUsers.disable({ emitEvent: false })
          : this.hasRemoteUsers.enable({ emitEvent: false });
      });

    this.hasRemoteUsers.valueChanges
      .subscribe((hasRemoteUsers: boolean) => {
        (hasRemoteUsers)
          ? this.remoteUsers.setValidators(FormArrayValidators.atLeast(1))
          : this.remoteUsers.clearValidators();

        this.hasNoRemoteUserError = false;
        this.remoteUsers.updateValueAndValidity({ emitEvent: false });
      });

    const site = this.siteService.site;
    this.isCompleted = site?.completed;
    // Inform the parent not to patch the form as there are outstanding changes
    // to the remote users that need to be persisted
    const fromRemoteUser = this.route.snapshot.queryParams.fromRemoteUser === 'true';
    // Remove query param from URL without refreshing
    this.router.navigate([], { queryParams: { fromRemoteUser: null } });
    this.siteFormStateService.setForm(site, !fromRemoteUser);
    this.form.markAsPristine();
  }
}
