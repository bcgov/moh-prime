import { Component, OnInit, ViewChild } from '@angular/core';
import { Subscription, Observable } from 'rxjs';
import { FormGroup, FormControl } from '@angular/forms';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { SiteRoutes } from '@registration/site-registration.routes';
import { ActivatedRoute, Router } from '@angular/router';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import tus from 'tus-js-client';
import { ToastService } from '@core/services/toast.service';

@Component({
  selector: 'app-business-licence',
  templateUrl: './business-licence.component.html',
  styleUrls: ['./business-licence.component.scss']
})
export class BusinessLicenceComponent implements OnInit {

  @ViewChild('myPond') myPond: any;

  public busy: Subscription;
  public form: FormGroup;
  public routeUtils: RouteUtils;
  public isCompleted: boolean;
  public SiteRoutes = SiteRoutes;

  public uploadProgress = 0;

  public pondOptions = {
    class: 'my-filepond',
    multiple: true,
    labelIdle: 'Drop files here',
    acceptedFileTypes: 'image/jpeg, image/png'
  };

  public pondFiles = [];

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteRegistrationResource: SiteRegistrationResource,
    private siteRegistrationService: SiteRegistrationService,
    private siteRegistrationStateService: SiteRegistrationStateService,
    private formUtilsService: FormUtilsService,
    private dialog: MatDialog,
    private toastService: ToastService
  ) {
    this.routeUtils = new RouteUtils(route, router, SiteRoutes.MODULE_PATH);
  }

  public get name(): FormControl {
    return this.form.get('name') as FormControl;
  }

  public get doingBusinessAs(): FormControl {
    return this.form.get('doingBusinessAs') as FormControl;
  }

  public pondHandleInit() {
    console.log('FilePond has initialised', this.myPond);
  }

  public pondHandleAddFile(event: any) {
    // Get the selected file from the input element
    const file = event.file.file;

    // Create a new tus upload
    const upload = new tus.Upload(file, {
      endpoint: 'http://localhost:5000/api/document',
      retryDelays: [0, 3000, 5000, 10000, 20000],
      metadata: {
        filename: file.name,
        filetype: file.type
      },
      headers: {
        "Access-Control-Allow-Origin": "*",
        "Authorization": "Bearer eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJmRW9CaFBWeERlSTI4ZGx2OWZTNWI4OHFWSWlibm83SXJpbkhJWTNlcXQwIn0.eyJqdGkiOiI0MWI3ZWZhMC1kYzZkLTQ0YzEtOGRiMy0wMmE0N2E0ZDUzYWMiLCJleHAiOjE1ODg2NzQ1OTksIm5iZiI6MCwiaWF0IjoxNTg4NjM4NjAwLCJpc3MiOiJodHRwczovL3Nzby1kZXYucGF0aGZpbmRlci5nb3YuYmMuY2EvYXV0aC9yZWFsbXMvdjRtYnFxYXMiLCJhdWQiOiJhY2NvdW50Iiwic3ViIjoiZjQzYmMwMDktMTg3MS00MWMwLWIzNmQtODBkZDNiYzViYTZkIiwidHlwIjoiQmVhcmVyIiwiYXpwIjoicHJpbWUtYXBwbGljYXRpb24tZGV2Iiwibm9uY2UiOiI5OWZiNDI0NS0xZGM0LTQ1NjktYjg4MS0wNGMwOTk0OWNmY2QiLCJhdXRoX3RpbWUiOjE1ODg2Mzg1OTksInNlc3Npb25fc3RhdGUiOiIwZDU1OWQyMS05MzQ4LTRiYWYtYjA5OC1lZWM5MmI0MmI0ZWYiLCJhY3IiOiIxIiwiYWxsb3dlZC1vcmlnaW5zIjpbIioiXSwicmVhbG1fYWNjZXNzIjp7InJvbGVzIjpbInByaW1lX3VzZXIiLCJvZmZsaW5lX2FjY2VzcyIsInVtYV9hdXRob3JpemF0aW9uIiwiZmVhdHVyZV9zaXRlX3JlZ2lzdHJhdGlvbiJdfSwicmVzb3VyY2VfYWNjZXNzIjp7ImFjY291bnQiOnsicm9sZXMiOlsibWFuYWdlLWFjY291bnQiLCJtYW5hZ2UtYWNjb3VudC1saW5rcyIsInZpZXctcHJvZmlsZSJdfX0sInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgYWRkcmVzcyBlbWFpbCIsImF1ZCI6InByaW1lLXdlYi1hcGkiLCJpZGVudGl0eV9wcm92aWRlciI6ImJjc2MiLCJiaXJ0aGRhdGUiOiIyMDAwLTA2LTA3IiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJhZGRyZXNzIjp7InN0cmVldF9hZGRyZXNzIjoiNzcyNCBQTEVBU0FOVCBDSVJDVVMiLCJsb2NhbGl0eSI6IkhJR0hMQU5EUyIsInJlZ2lvbiI6IkJDIiwicG9zdGFsX2NvZGUiOiJWOUIgNVk4IiwiY291bnRyeSI6IkNBIn0sIm5hbWUiOiJQUklNRVQgRUxFVkVOIiwiaWRlbnRpdHlfYXNzdXJhbmNlX2xldmVsIjozLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJidWlvM25hbGFzZXNkcmxmcW5rdmM0NG9iY2pzbms2YiIsImdpdmVuX25hbWVzIjoiUFJJTUVUIElkZWxsYSIsImdpdmVuX25hbWUiOiJQUklNRVQiLCJmYW1pbHlfbmFtZSI6IkVMRVZFTiJ9.EFIPTJuAtyHdmE8IcUGQCOHWnBXR_F__jPUlcKnXbawDEgiclLoarefpfU5whYH_3Ex0MJs3vJEjaxidfpBSSpzlx-LzYD2PzaHsb4N_RetP8NYiQYb3-RaqUUUqYPi40q10BnTEFHbRvwrGQvrTdE3VNL0ncgo3hAR6IcQPcZxqSaDQr8yHas2MWZ8IbrxP91DdE623Hti6w3oSpWFiGl_Q2YCOAxn06vcdoXapnsd1crfKQWIIGoPm4dhGKODDKfipqQeWp8fUH1-_vQ6L-7OH3p8gkgciVW8REDiIYqmh5LShpgVy3H6zuB8hOT_yU6C5phCI0RVaE2JzRiYAZA",
      },
      filename: file.name,
      onError: async (error: Error) => {
        this.toastService.openErrorToast(error.message);
      },
      onProgress: async (bytesUploaded: number, bytesTotal: number) => {
        this.uploadProgress = (bytesUploaded / bytesTotal * 100);
      },
      onSuccess: async () => {
        this.uploadProgress = 100;
        this.toastService.openSuccessToast('Upload Successful');
      }
    });

    // Start the upload
    upload.start();
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
  }

  private createFormInstance() {
    this.form = this.siteRegistrationStateService.organizationInformationForm;
  }

  private initForm() {
    const site = this.siteRegistrationService.site;
    this.isCompleted = site.completed;
    this.siteRegistrationStateService.setSite(site, true);
  }


}
