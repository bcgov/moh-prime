import { Component, OnInit, ViewChild } from '@angular/core';
import { Subscription, Observable } from 'rxjs';
import { FormGroup, FormControl } from '@angular/forms';
import { RouteUtils } from '@registration/shared/classes/route-utils.class';
import { SiteRoutes } from '@registration/site-registration.routes';
import { ActivatedRoute, Router } from '@angular/router';
import { SiteRegistrationResource } from '@registration/shared/services/site-registration-resource.service';
import { SiteRegistrationService } from '@registration/shared/services/site-registration.service';
import { SiteRegistrationStateService } from '@registration/shared/services/site-registration-state.service';
import { FormUtilsService } from '@common/services/form-utils.service';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';
import tus from 'tus-js-client';
import { FilePond } from 'filepond';
import { FilePondComponent } from 'ngx-filepond/filepond.component';

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
    private dialog: MatDialog
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
    console.log('A file was added', event);
    console.log('What si this event type', typeof (event));

    // Get the selected file from the input element
    const file = event;

    // const file = this.myPond.getFile();
    const filename = file.filename;
    const filetype = file.fileType;


    console.log('THE FILE: ', file);

    console.log('IS THIS A FILE', file instanceof File);

    console.log('What si this', typeof (file));


    // Create a new tus upload
    const upload = new tus.Upload(file, {
      endpoint: 'http://localhost:1080/files/',
      retryDelays: [0, 3000, 5000, 10000, 20000],
      metadata: {
        filename,
        filetype
      },
      onError: async (error: Error) => {
        console.log('Failed because: ' + error);
      },
      onProgress: async (bytesUploaded, bytesTotal) => {
        const percentage = (bytesUploaded / bytesTotal * 100).toFixed(2);
        console.log(bytesUploaded, bytesTotal, percentage + '%');
      },
      onSuccess: async () => {
        console.log('Download %s from %s', upload.file.name, upload.url);
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
