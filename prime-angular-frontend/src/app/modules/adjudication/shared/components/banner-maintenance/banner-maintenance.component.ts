import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, FormGroupDirective, ValidationErrors, Validators } from '@angular/forms';
import { ShowOnDirtyErrorStateMatcher } from '@angular/material/core';
import { MatDialog } from '@angular/material/dialog';

import { FormGroupValidators } from '@lib/validators/form-group.validators';

import { FormUtilsService } from '@core/services/form-utils.service';

import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import { BannerType } from '@shared/enums/banner-type.enum';
import { Banner } from '@shared/models/banner.model';
import { DialogOptions } from '@shared/components/dialogs/dialog-options.model';
import { ConfirmDialogComponent } from '@shared/components/dialogs/confirm-dialog/confirm-dialog.component';

import { Role } from '@auth/shared/enum/role.enum';
import moment from 'moment';

export class IsSameOrBeforeErrorStateMatcher extends ShowOnDirtyErrorStateMatcher {
  public isErrorState(control: FormControl | null, form: FormGroupDirective | null): boolean {
    const invalidCtrl = super.isErrorState(control, form);
    // Apply custom validation from parent form group
    const dirtyOrSubmitted = (control?.dirty || form?.submitted || control?.touched);
    const requiredControl = (!!(control?.hasError('required')) && dirtyOrSubmitted);
    const invalidParent = !!(control?.parent && control?.parent.hasError('isSameOrBefore') && dirtyOrSubmitted);
    return (invalidCtrl || invalidParent || requiredControl);
  }
}

@Component({
  selector: 'app-banner-maintenance',
  templateUrl: './banner-maintenance.component.html',
  styleUrls: ['./banner-maintenance.component.scss']
})
export class BannerMaintenanceComponent implements OnInit {
  @Input() public locationCode: BannerLocationCode;
  @Output() public save: EventEmitter<Banner>;
  @Output() public delete: EventEmitter<null>;

  public internalBanner: Banner;

  public form: FormGroup;
  public isSameOrBeforeErrorStateMatcher: IsSameOrBeforeErrorStateMatcher;

  public hasActions: boolean;
  public editorConfig: Record<string, string>;

  public Role = Role;
  public BannerType = BannerType;
  public BannerLocationCode = BannerLocationCode;

  public readonly hoursTimePattern = {
    A: { pattern: /[0-2]/ },
    B: { pattern: /[0-9]/ },
    C: { pattern: /[0-5]/ }
  };

  constructor(
    private fb: FormBuilder,
    private formUtils: FormUtilsService,
    private dialog: MatDialog,
  ) {
    this.hasActions = false;
    this.editorConfig = {
      height: '18rem',
      base_url: '/tinymce',
      suffix: '.min',
      plugins: 'lists advlist',
      toolbar: 'undo redo | bold italic underline | bullist numlist outdent indent | removeformat',
      menubar: 'false'
    };
    this.save = new EventEmitter<Banner>();
    this.delete = new EventEmitter();
  }

  @Input() set banner(banner: Banner) {
    this.internalBanner = banner;
    if (banner) {
      this.patchForm(banner);
    } else if (this.form) {
      this.bannerLocationCode.setValue(this.locationCode);
    }
  }

  public get content(): FormControl {
    return this.form.get('content') as FormControl;
  }

  public get title(): FormControl {
    return this.form.get('title') as FormControl;
  }

  public get bannerType(): FormControl {
    return this.form.get('bannerType') as FormControl;
  }

  public get bannerLocationCode(): FormControl {
    return this.form.get('bannerLocationCode') as FormControl;
  }

  public get dateRange(): FormGroup {
    return this.form.get('dateRange') as FormGroup;
  }

  public get startDate(): FormControl {
    return this.dateRange.get('startDate') as FormControl;
  }

  public get startTime(): FormControl {
    return this.dateRange.get('startTime') as FormControl;
  }

  public get endDate(): FormControl {
    return this.dateRange.get('endDate') as FormControl;
  }

  public get endTime(): FormControl {
    return this.dateRange.get('endTime') as FormControl;
  }

  public onSubmit() {
    if (this.formUtils.checkValidity(this.form)) {
      this.internalBanner = this.json;
      this.save.emit(this.internalBanner);
    }
  }

  public onDelete() {
    const data: DialogOptions = {
      title: 'Delete Banner',
      message: `Are you sure you want to delete this banner?`,
      actionText: 'Delete Banner'
    };

    this.dialog.open(ConfirmDialogComponent, { data })
      .afterClosed()
      .subscribe((result: boolean) => {
        if (result) {
          this.delete.emit();
          this.form.reset();
          this.bannerLocationCode.setValue(this.locationCode);
        }
      });
  }

  public onUpdate(event: { editor: any }) {
    if (!event.editor) { return; }
    this.internalBanner = this.json;
  }

  public ngOnInit(): void {
    this.createFormInstance();
    this.initForm();
  }

  private initForm(): void {
    this.form.valueChanges.subscribe(() => this.internalBanner = this.json);
  }

  private patchForm(banner: Banner): void {
    this.form.patchValue(banner);
    this.startDate.setValue(banner.startDate);
    this.startTime.setValue(banner.startTime);
    this.endDate.setValue(banner.endDate);
    this.endTime.setValue(banner.endTime);
  }

  private get json(): Banner {
    const banner = this.form.getRawValue();
    return {
      ...banner,
      startDate: banner.dateRange.startDate,
      startTime: banner.dateRange.startTime,
      endDate: banner.dateRange.endDate,
      endTime: banner.dateRange.endTime,
    };
  }

  private createFormInstance() {
    this.form = this.fb.group({
      content: [
        {
          value: '',
          disabled: false
        },
        [Validators.required]
      ],
      bannerType: [
        {
          value: '',
          disabled: false
        },
        [Validators.required]
      ],
      bannerLocationCode: [
        {
          value: this.locationCode,
          disabled: true
        },
        [Validators.required]
      ],
      title: [
        {
          value: '',
          disabled: false
        },
        [Validators.required]
      ],
      dateRange: this.fb.group(
        {
          startDate: ['', [Validators.required]],
          startTime: ['', [Validators.required]],
          endDate: ['', [Validators.required]],
          endTime: ['', [Validators.required]],
        },
        { validator: this.isTimeValid })
    });
    this.isSameOrBeforeErrorStateMatcher = new IsSameOrBeforeErrorStateMatcher();
  }

  private isTimeValid(group: FormGroup): ValidationErrors | null {
    const startDate = moment(group.get('startDate').value);
    const startTime = moment(group.get('startTime').value, 'HHmm');
    const endDate = moment(group.get('endDate').value);
    const endTime = moment(group.get('endTime').value, 'HHmm');

    const start = startDate.set({
      hour: startTime.get('hour'),
      minute: startTime.get('minute')
    });
    const end = endDate.set({
      hour: endTime.get('hour'),
      minute: endTime.get('minute')
    });

    if (!start || !end) {
      return null;
    }

    return start.isSameOrBefore(end)
      ? null
      : { isSameOrBefore: true };
  }
}
