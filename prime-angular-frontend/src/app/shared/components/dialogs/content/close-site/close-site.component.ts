import { Component, EventEmitter, Inject, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatSelectChange } from '@angular/material/select';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { ConfigService } from '@config/config.service';
import { Config } from '@config/config.model';
import { DialogOptions } from '../../dialog-options.model';
import { SiteResource } from '@core/resources/site-resource.service';
import { exhaustMap } from 'rxjs';

@Component({
  selector: 'app-close-site',
  templateUrl: './close-site.component.html',
  styleUrls: ['./close-site.component.scss']
})
export class CloseSiteComponent implements OnInit {

  public form: FormGroup;
  public siteCloseReasons: Config<number>[];
  public siteId: number;


  constructor(
    private fb: FormBuilder,
    private dialogRef: MatDialogRef<ConfirmDialogComponent>,
    private configService: ConfigService,
    private siteResource: SiteResource,
    @Inject(MAT_DIALOG_DATA) public data: DialogOptions,
  ) {
    this.siteId = data.data.siteId;
  }

  public get siteCloseReason(): FormControl {
    return this.form.get('siteCloseReason') as FormControl;
  }

  public get note(): FormControl {
    return this.form.get('note') as FormControl;
  }

  ngOnInit(): void {
    this.siteCloseReasons = this.configService.siteCloseReasons;
    this.form = this.fb.group({
      siteCloseReason: [null, [Validators.required]],
      note: []
    })
  }

  onCancel(): void {
    this.dialogRef.close();
  }

  onCloseSite(): void {
    if (this.form.valid) {
      this.siteResource.closeSite(this.siteId, this.siteCloseReason.value)
        .subscribe(() => {
          if (this.note.value) {
            this.siteResource.createSiteRegistrationNote(this.siteId, this.note.value)
              .subscribe();
          }
          this.dialogRef.close({ reload: true })
        });
    } else {
      this.siteCloseReason.markAsTouched();
    }
  }
}
