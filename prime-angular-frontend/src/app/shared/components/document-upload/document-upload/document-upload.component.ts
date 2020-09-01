import { Component, OnInit, ViewChild, Input, Output } from '@angular/core';
import { EventEmitter } from '@angular/core';

import { FilePondOptions, ProcessServerConfigFunction } from 'filepond';
import { FilePondPluginFileValidateTypeProps } from 'filepond-plugin-file-validate-type';
import { FilePondPluginFileValidateSizeProps } from 'filepond-plugin-file-validate-size';
import { FilePondComponent } from 'ngx-filepond/filepond.component';
import tus from 'tus-js-client';

import { environment } from '@env/environment';

import { LoggerService } from '@core/services/logger.service';
import { KeycloakTokenService } from '@auth/shared/services/keycloak-token.service';

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

  private apiSuffix = 'document';
  private jwt: string;

  constructor(
    private keycloakTokenService: KeycloakTokenService,
    private logger: LoggerService,
  ) { }

  public ngOnInit(): void {
    // Keys are the excepted mime types, values are the human-readable expected type labels.
    const fileValidateTypeLabelExpectedTypesMap = {
      'image/jpeg': '.jpeg, .jpg',
      'image/png': '.png'
    };

    this.filePondOptions = {
      className: `prime-filepond-${this.componentName}`,
      labelIdle: this.getIdleText(fileValidateTypeLabelExpectedTypesMap),
      fileValidateTypeLabelExpectedTypesMap,
      acceptedFileTypes: Object.keys(fileValidateTypeLabelExpectedTypesMap),
      allowFileTypeValidation: true,
      allowMultiple: !!this.multiple,
      maxFileSize: '3MB',
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

  public async onFilePondAddFile() {
    // Can't get token synchronously inside server.process(), so refresh token on file add.
    this.jwt = await this.keycloakTokenService.token();
  }

  private getIdleText(allowedFileTypesMap: { [key: string]: string }): string {
    const baseText = 'Click to Browse or Drop files here.';
    if (allowedFileTypesMap == null) {
      return baseText;
    }

    const types = Object.values(allowedFileTypesMap);
    let typeText = types.pop();

    if (types.length > 0) {
      const allButLast = types.join(', ');
      typeText = `${allButLast} or ${typeText}`;
    }

    return `${baseText} Files must be ${typeText}`;
  }

  private constructServer() {
    const process: ProcessServerConfigFunction = (fieldName, file, metadata, load, error, progress, abort) => {
      const { name: filename, type: filetype } = file;

      const upload = new tus.Upload(file, {
        endpoint: `${environment.apiEndpoint}/${this.apiSuffix}`,
        metadata: { filename, filetype },
        chunkSize: 1048576, // 1 MB
        removeFingerprintOnSuccess: true,
        retryDelays: [100, 3000],
        headers: { Authorization: `Bearer ${this.jwt}` },
        onError: (err: Error) => {
          this.logger.error('DocumentUpload::onFilePondAddFile', err);
          error(err.message);
        },
        onProgress: (bytesUploaded: number, bytesTotal: number) => progress(true, bytesUploaded, bytesTotal),
        onSuccess: () => {
          const documentGuid = upload.url.split('/').pop();
          load(documentGuid);
          this.completed.emit(new BaseDocument(filename, documentGuid));
        }
      });

      upload.start();
      return {
        abort: () => {
          upload.abort();
          abort();
        },
      };
    };

    return {
      process,
      // No-Op for revert to prevent the default DELETE request that FilePond does when removing a file.
      revert: (source: any, load: () => void, error: (errorText: string) => void) => load(),
    };
  }
}
