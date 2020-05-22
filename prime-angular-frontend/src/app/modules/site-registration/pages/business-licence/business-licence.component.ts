import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { FormGroup, FormControl } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';

import { Subscription, Observable } from 'rxjs';

import tus from 'tus-js-client';
import { FilePondComponent } from 'ngx-filepond/filepond.component';

import { environment } from '@env/environment';
import { ToastService } from '@core/services/toast.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import { LoggerService } from '@core/services/logger.service';
import { KeycloakTokenService } from '@auth/shared/services/keycloak-token.service';

import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { SiteRoutes } from '@registration/site-registration.routes';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';
import { BusinessLicence } from '@registration/shared/models/business-licence.model';
import { exhaustMap, map } from 'rxjs/operators';


@Component({
  selector: 'app-business-licence',
  templateUrl: './business-licence.component.html',
  styleUrls: ['./business-licence.component.scss']
})
export class BusinessLicenceComponent implements OnInit {
  public busy: Subscription;
  public form: FormGroup;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;
  public businessLicences: BusinessLicence[];

  @ViewChild('filePond') public filePondComponent: FilePondComponent;
  public filePondOptions: { [key: string]: any };
  public filePondUploadProgress = 0;
  public filePondFiles = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private dialog: MatDialog,
    private siteRegistrationResource: SiteRegistrationResource,
    private siteRegistrationService: SiteRegistrationService,
    private siteRegistrationStateService: SiteRegistrationStateService,
    private keycloakTokenService: KeycloakTokenService,
    private formUtilsService: FormUtilsService,
    private toastService: ToastService,
    private logger: LoggerService
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.filePondOptions = {
      class: 'prime-filepond',
      multiple: true,
      labelIdle: 'Drop files here',
      acceptedFileTypes: 'image/jpeg, image/png'
    };
  }

  public onSubmit() {
    if (this.formUtilsService.checkValidity(this.form)) {
      const payload = this.siteRegistrationStateService.site;
      this.siteRegistrationResource
        .updateSite(payload)
        .subscribe(() => {
          this.form.markAsPristine();
          this.nextRoute();
        });
    }
  }

  public onFilePondInit() {
    this.logger.info('FilePond has initialised', this.filePondComponent);
  }

  public async onFilePondAddFile(event: any) {
    const token = await this.keycloakTokenService.token();
    const file = event.file.file; // File for uploading
    const { name: filename, type: filetype } = file;
    const upload = new tus.Upload(file, {
      endpoint: `${environment.apiEndpoint}/document`,
      retryDelays: [0, 3000, 5000, 10000, 20000],
      metadata: { filename, filetype },
      headers: {
        'Access-Control-Allow-Origin': '*',
        Authorization: `Bearer ${token}`,
      },
      onError: async (error: Error) =>
        this.toastService.openErrorToast(error.message),
      onProgress: async (bytesUploaded: number, bytesTotal: number) =>
        this.filePondUploadProgress = (bytesUploaded / bytesTotal * 100),
      onSuccess: async () => {
        this.filePondUploadProgress = 100;
        this.toastService.openSuccessToast('File(s) have been uploaded');

        const documentGuid = upload.url.split('/').pop();

        this.siteRegistrationResource
          .createBusinessLicence(this.siteRegistrationStateService.site.id, documentGuid, filename)
          .subscribe(() => {
            this.toastService.openSuccessToast('Business Licence has been added');
          });
      }
    });
    upload.start();
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.ORGANIZATION_INFORMATION);
  }

  public nextRoute() {
    if (this.isCompleted) {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
    } else {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_ADDRESS);
    }
  }

  public canDeactivate(): Observable<boolean> | boolean {
    const data = 'unsaved';
    return (this.form.dirty)
      ? this.dialog.open(ConfirmDialogComponent, { data }).afterClosed()
      : true;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
    // this.getBusinessLicences();
  }

  private createFormInstance() {
    this.form = this.siteRegistrationStateService.organizationInformationForm;
  }

  private initForm() {
    const site = this.siteRegistrationService.site;
    this.isCompleted = site.completed;
    this.siteRegistrationStateService.setSite(site, true);
  }

  private getBusinessLicences() {
    const siteId = this.siteRegistrationService.site.id;
    return this.siteRegistrationResource.getBusinesssLicences(siteId).subscribe(
      (bl: BusinessLicence[]) => this.businessLicences = bl
    );
  }
}
