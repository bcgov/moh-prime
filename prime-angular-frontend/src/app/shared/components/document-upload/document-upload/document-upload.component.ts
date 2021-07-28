import { Component, OnInit, ViewChild, Input, Output, Inject } from '@angular/core';
import { EventEmitter } from '@angular/core';

import { FilePondErrorDescription, FilePondFile, FilePondOptions, ProcessServerConfigFunction } from 'filepond';
import { FilePondPluginFileValidateTypeProps } from 'filepond-plugin-file-validate-type';
import { FilePondPluginFileValidateSizeProps } from 'filepond-plugin-file-validate-size';
import { FilePondComponent } from 'ngx-filepond/filepond.component';
import tus from 'tus-js-client';

import { environment } from '@env/environment';

import { ConsoleLoggerService } from '@core/services/console-logger.service';
import { AccessTokenService } from '@auth/shared/services/access-token.service';
import { APP_CONFIG, AppConfig } from '../../../../app-config.module';

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
  @Input() public labelMessage: string;
  @Output() public completed: EventEmitter<BaseDocument>;
  @Output() public remove: EventEmitter<string>;
  @ViewChild('filePond') public filePondComponent: FilePondComponent;
  public filePondFiles: FilePondFile[];
  public filePondOptions: FilePondOptions & FilePondPluginFileValidateSizeProps & FilePondPluginFileValidateTypeProps;

  private jwt: string;

  constructor(
    @Inject(APP_CONFIG) private config: AppConfig,
    private accessTokenService: AccessTokenService,
    private logger: ConsoleLoggerService
  ) {
    this.labelMessage = 'Click to Browse or Drop files here';
    this.filePondFiles = [];
    this.completed = new EventEmitter();
    this.remove = new EventEmitter<string>();
  }

  public ngOnInit(): void {
    // Keys are the excepted mime types, values are the human-readable expected type labels.
    const fileValidateTypeLabelExpectedTypesMap = {
      'image/jpeg': '.jpeg, .jpg',
      'image/png': '.png',
      'application/pdf': '.pdf'
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
  }

  public disable() {
    // TODO temporary fix to get around type definitions not be up to date for filepond
    const attribute = 'pond';
    this.filePondComponent[attribute].setOptions({ disabled: true });
  }

  public enable() {
    // TODO temporary fix to get around type definitions not be up to date for filepond
    const attribute = 'pond';
    this.filePondComponent[attribute].setOptions({ disabled: false });
  }

  public removeFiles() {
    // TODO temporary fix to get around type definitions not be up to date for filepond
    const method = 'removeFiles';
    this.filePondComponent[method]({ revert: false });
  }

  public onFilePondInit() {
    this.logger.info('FilePond has initialized', this.filePondComponent);
  }

  public async onFilePondAddFile() {
    // Can't get token synchronously inside server.process(), so refresh token on file add
    this.jwt = await this.accessTokenService.token();
  }

  public onFilePondRemoveFile({ file }: { file: FilePondFile, error: FilePondErrorDescription }) {
    this.remove.emit(file.serverId);
  }

  private getIdleText(allowedFileTypesMap: { [key: string]: string }): string {
    if (!allowedFileTypesMap) {
      return this.labelMessage;
    }

    const [initialFileType, ...fileTypes] = Object.values(allowedFileTypesMap);
    const allowedFileTypes = fileTypes.reduce((concat, fileType, index) =>
        (index === fileTypes.length - 1)
          ? `${concat}, or ${fileType}`
          : `${concat}, ${fileType}`
      , initialFileType);

    return `${this.labelMessage}. Files must be ${allowedFileTypes}`;
  }

  private constructServer() {
    const process: ProcessServerConfigFunction = (fieldName, file, metadata, load, error, progress, abort) => {
      const { name: filename, type: filetype } = file;

      const upload = new tus.Upload(file, {
        endpoint: `${this.config.apiEndpoint}/document`,
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
      url: `${environment.documentManagerUrl}/documents/uploads`,
      process,
    };
  }
}
