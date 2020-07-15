import { Component, OnInit, ViewChild, Input, Output } from '@angular/core';
import { FilePondComponent } from 'ngx-filepond/filepond.component';
import { ActivatedRoute, Router } from '@angular/router';
import { SiteService } from '@registration/shared/services/site.service';
import { SiteResource } from '@registration/shared/services/site-resource.service';
import { KeycloakTokenService } from '@auth/shared/services/keycloak-token.service';
import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { environment } from '@env/environment';
import tus from 'tus-js-client';
import { EventEmitter } from '@angular/core';

export class BaseDocument {
  id: number;
  filename: string;
  documentGuid: string;

  constructor(filename: string, documentGuid: string) {
    this.filename = filename;
    this.documentGuid = documentGuid;
  }
}

@Component({
  selector: 'app-document-upload',
  templateUrl: './document-upload.component.html',
  styleUrls: ['./document-upload.component.scss']
})
export class DocumentUploadComponent implements OnInit {
  @Input() public componentName: string;
  @Input() public multiple: boolean;
  @Output() public completed: EventEmitter<BaseDocument> = new EventEmitter();
  @ViewChild('filePond') public filePondComponent: FilePondComponent;
  public filePondOptions: { [key: string]: any };
  public filePondUploadProgress = 0;
  public filePondFiles = [];
  @Input() public additionalApiSuffix: string;
  public apiSuffix = '/document';

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private siteService: SiteService,
    private siteResource: SiteResource,
    private keycloakTokenService: KeycloakTokenService,
    private toastService: ToastService,
    private logger: LoggerService,
    private formUtilsService: FormUtilsService
  ) { }

  public ngOnInit(): void {
    this.filePondOptions = {
      class: `prime-filepond-${this.componentName}`,
      labelIdle: 'Click to Browse or Drop files here',
      acceptedFileTypes: ['image/jpeg', 'image/png'],
      fileValidateTypeDetectType: (source: any, type: string) => new Promise((resolve, reject) => {
        if (!type.includes('image')) {
          this.toastService.openSuccessToast('File must be image');
          reject(type);
        }
        resolve(type);
      }),
      allowFileTypeValidation: true,
      multiple: !!this.multiple
    };
    if (this.additionalApiSuffix) {
      this.apiSuffix = `${this.apiSuffix}/${this.additionalApiSuffix}`;
    }
  }

  public onFilePondInit() {
    this.logger.info('FilePond has initialized', this.filePondComponent);
  }

  public async onFilePondAddFile(event: any) {
    const token = await this.keycloakTokenService.token();
    const file = event.file.file; // File for uploading
    const { name: filename, type: filetype } = file;
    if (this.filePondOptions.acceptedFileTypes.includes(filetype)) {
      const upload = new tus.Upload(file, {
        endpoint: `${environment.apiEndpoint}${this.apiSuffix}`,
        retryDelays: [0, 3000, 5000, 10000, 20000],
        metadata: { filename, filetype },
        headers: {
          'Access-Control-Allow-Origin': '*',
          Authorization: `Bearer ${token}`,
        },
        onError: async (error: Error) => this.logger.error('DocumentUpload::onFilePondAddFile', error),
        // TODO throws an error intermittently so commented out for release
        // this.toastService.openErrorToast(error.message),
        onProgress: async (bytesUploaded: number, bytesTotal: number) =>
          this.filePondUploadProgress = (bytesUploaded / bytesTotal * 100),
        onSuccess: async () => {
          this.filePondUploadProgress = 100;
          this.toastService.openSuccessToast('File(s) have been uploaded');

          const documentGuid = upload.url.split('/').pop();

          // Emit event to trigger parent saving ralationship in database
          this.completed.emit(new BaseDocument(filename, documentGuid));
        }
      });
      upload.start();
    }
  }
}
