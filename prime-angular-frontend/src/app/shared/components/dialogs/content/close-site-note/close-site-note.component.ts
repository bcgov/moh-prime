import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../../confirm-dialog/confirm-dialog.component';
import { ConfigService } from '@config/config.service';
import { Config } from '@config/config.model';
import { DialogOptions } from '../../dialog-options.model';
import { SiteResource } from '@core/resources/site-resource.service';
import { FormUtilsService } from '@core/services/form-utils.service';

@Component({
  selector: 'app-close-site',
  templateUrl: './close-site-note.component.html',
  styleUrls: ['./close-site-note.component.scss']
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
    protected formUtilsService: FormUtilsService,
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

  public ngOnInit(): void {
    this.createFormInstance();

    this.siteCloseReasons = this.configService.siteCloseReasons;

    this.siteCloseReason.valueChanges.subscribe((value) => {
      if (this.siteCloseReasons.find((s) => s.code === value).name === "Other") {
        this.formUtilsService.setValidators(this.note, [Validators.required]);
      } else {
        this.formUtilsService.resetAndClearValidators(this.note);
        this.note.markAsUntouched();
      }
    });
  }

  public onCancel(): void {
    this.dialogRef.close();
  }

  public onCloseSite(): void {
    if (this.form.valid) {
      this.siteResource.closeSite(this.siteId, this.siteCloseReason.value, this.note.value)
        .subscribe(() => {
          if (this.note.value) {
            this.siteResource.createSiteRegistrationNote(this.siteId, this.note.value)
              .subscribe();
          }
          this.dialogRef.close({ reload: true });
        });
    } else {
      this.siteCloseReason.markAsTouched();
    }
  }

  private createFormInstance(): void {
    this.form = this.fb.group({
      siteCloseReason: [null, [Validators.required]],
      note: []
    })

  }
}