import { Component, OnInit, ViewChild, Input, Output } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { EventEmitter } from '@angular/core';

import { FilePondOptions, FilePondServerConfigProps } from 'filepond';
import { FilePondPluginFileValidateTypeProps } from 'filepond-plugin-file-validate-type';
import { FilePondPluginFileValidateSizeProps } from 'filepond-plugin-file-validate-size';
import { FilePondComponent } from 'ngx-filepond/filepond.component';
import tus from 'tus-js-client';

import { environment } from '@env/environment';

import { ToastService } from '@core/services/toast.service';
import { LoggerService } from '@core/services/logger.service';
import { FormUtilsService } from '@core/services/form-utils.service';
import { SiteResource } from '@core/resources/site-resource.service';

import { KeycloakTokenService } from '@auth/shared/services/keycloak-token.service';
import { SiteService } from '@registration/shared/services/site.service';
import { from } from 'rxjs';

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
  @Input() public additionalApiSuffix: string;
  @Output() public completed: EventEmitter<BaseDocument> = new EventEmitter();
  @ViewChild('filePond') public filePondComponent: FilePondComponent;
  public filePondOptions: FilePondOptions & FilePondPluginFileValidateSizeProps & FilePondPluginFileValidateTypeProps;
  public filePondFiles = [];
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
      className: `prime-filepond-${this.componentName}`,
      labelIdle: 'Click to Browse or Drop files here',
      acceptedFileTypes: ['image/jpeg', 'image/png'],
      allowFileTypeValidation: true,
      allowMultiple: !!this.multiple,
      maxFileSize: null,// '3MB',
      maxTotalFileSize: null,
      server: this.constructServer()
    };
    if (this.additionalApiSuffix) {
      this.apiSuffix = `${this.apiSuffix}/${this.additionalApiSuffix}`;
    }
  }

  public onFilePondInit() {
    this.logger.info('FilePond has initialized', this.filePondComponent);
  }

  private constructServer() {
    return {
      process: (fieldName, file, metadata, load, error, progress, abort) => {
        from(this.keycloakTokenService.token())
          .subscribe(token => {

            const { name: filename, type: filetype } = file;

            const upload = new tus.Upload(file, {
              endpoint: `${environment.apiEndpoint}${this.apiSuffix}`,
              retryDelays: [100, 3000],
              metadata: { filename, filetype },
              chunkSize: 1048576, // 1 MB
              headers: this.createHeaders(),
              onError: (err: Error) => {
                this.logger.error('DocumentUpload::onFilePondAddFile', err);
                error(err);
                // TODO throws an error intermittently so commented out for release
                // this.toastService.openErrorToast(err.message),
              },
              onProgress: (bytesUploaded: number, bytesTotal: number) => progress(true, bytesUploaded, bytesTotal),
              onSuccess: () => {
                const documentGuid = upload.url.split('/').pop();
                load(documentGuid);
                //this.completed.emit(new BaseDocument(filename, documentGuid));
                this.toastService.openSuccessToast('File(s) have been uploaded');
              }
            });

            upload.start();
            return {
              abort: () => {
                upload.abort();
                abort();
              },
            };
          });
      },

      // No-Op for revert to prevent the default DELETE request that FilePond does when removing a file.
      revert: (seource: any, load: () => void, error: (errorText: string) => void) => load(),
    };
  }

  private createHeaders() {
    const token = ""; //await this.keycloakTokenService.token();

    return {
      'Access-Control-Allow-Origin': '*',
      Authorization: `Bearer ${token}`,
    };
  }
}
