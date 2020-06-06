import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { Subscription } from 'rxjs';

import tus from 'tus-js-client';
import { FilePondComponent } from 'ngx-filepond/filepond.component';

import { environment } from '@env/environment';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { KeycloakTokenService } from '@auth/shared/services/keycloak-token.service';

import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { SiteRoutes } from '@registration/site-registration.routes';
import { SiteResource } from '@registration/shared/services/site-resource.service';
import { SiteService } from '@registration/shared/services/site.service';
import { BusinessLicence } from '@registration/shared/models/business-licence.model';
import { FormUtilsService } from '@core/services/form-utils.service';
import { FormGroup } from '@angular/forms';

@Component({
  selector: 'app-business-licence',
  templateUrl: './business-licence.component.html',
  styleUrls: ['./business-licence.component.scss']
})
export class BusinessLicenceComponent implements OnInit {
  public busy: Subscription;
  public title: string;
  public routeUtils: RouteUtils;
  public businessLicences: BusinessLicence[];
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;
  public hasNoLicenceError: boolean;
  public uploadedFile: boolean;

  @ViewChild('filePond') public filePondComponent: FilePondComponent;
  public filePondOptions: { [key: string]: any };
  public filePondUploadProgress = 0;
  public filePondFiles = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteService: SiteService,
    private siteResource: SiteResource,
    private keycloakTokenService: KeycloakTokenService,
    private toastService: ToastService,
    private logger: LoggerService,
    private formUtilsService: FormUtilsService
  ) {
    this.title = 'Submit Your Business Licence';
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
    this.filePondOptions = {
      class: 'prime-filepond',
      multiple: true,
      labelIdle: 'Click to Browse or Drop files here',
      acceptedFileTypes: ['image/jpeg', 'image/png'],
      allowFileTypeValidation: true,
    };
  }

  public onSubmit() {
    // TODO has to have at least one document uploaded

    if (this.siteService.site.businessLicences.length > 0 || this.uploadedFile) {
      this.nextRoute();
    } else {
      this.hasNoLicenceError = true;
    }
  }

  public onFilePondInit() {
    this.logger.info('FilePond has initialised', this.filePondComponent);
  }

  public async onFilePondAddFile(event: any) {
    const token = await this.keycloakTokenService.token();
    const file = event.file.file; // File for uploading
    const { name: filename, type: filetype } = file;
    if (this.filePondOptions.acceptedFileTypes.includes(filetype)) {
      const upload = new tus.Upload(file, {
        endpoint: `${environment.apiEndpoint}/document`,
        retryDelays: [0, 3000, 5000, 10000, 20000],
        metadata: { filename, filetype },
        headers: {
          'Access-Control-Allow-Origin': '*',
          Authorization: `Bearer ${token}`,
        },
        onError: async (error: Error) => this.logger.error('BusinessLicence::onFilePondAddFile', error),
        // TODO throws an error intermittently so commented out for release
        // this.toastService.openErrorToast(error.message),
        onProgress: async (bytesUploaded: number, bytesTotal: number) =>
          this.filePondUploadProgress = (bytesUploaded / bytesTotal * 100),
        onSuccess: async () => {
          this.filePondUploadProgress = 100;
          this.toastService.openSuccessToast('File(s) have been uploaded');

          const documentGuid = upload.url.split('/').pop();
          const siteId = this.siteService.site.id;

          this.siteResource
            .createBusinessLicence(siteId, documentGuid, filename)
            .subscribe();

          this.uploadedFile = true;
          this.hasNoLicenceError = false;
        }
      });
      upload.start();
    }
  }

  public onBack() {
    this.routeUtils.routeRelativeTo(SiteRoutes.SITE_ADDRESS);
  }

  public nextRoute() {
    if (this.isCompleted) {
      this.routeUtils.routeRelativeTo(SiteRoutes.SITE_REVIEW);
    } else {
      this.routeUtils.routeRelativeTo(SiteRoutes.HOURS_OPERATION);
    }
  }

  public ngOnInit() {
    const site = this.siteService.site;
    this.isCompleted = site?.completed;

    this.uploadedFile = false;
  }

  private getBusinessLicences() {
    const siteId = this.siteService.site.id;
    return this.siteResource.getBusinessLicences(siteId)
      .subscribe((businessLicenses: BusinessLicence[]) =>
        this.businessLicences = businessLicenses
      );
  }
}
