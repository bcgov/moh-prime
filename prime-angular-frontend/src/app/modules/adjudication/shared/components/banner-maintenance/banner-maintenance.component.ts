import { ContentObserver } from '@angular/cdk/observers';
import { ThrowStmt } from '@angular/compiler';
import { Component, Input, OnInit } from '@angular/core';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Role } from '@auth/shared/enum/role.enum';
import { BannerLocationCode } from '@shared/enums/banner-location-code.enum';
import { BannerType } from '@shared/enums/banner-type.enum';
import { Banner } from '@shared/models/banner.model';
import { BannerResourceService } from '@shared/services/banner-resource.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-banner-maintenance',
  templateUrl: './banner-maintenance.component.html',
  styleUrls: ['./banner-maintenance.component.scss']
})
export class BannerMaintenanceComponent implements OnInit {
  @Input() public locationCode: BannerLocationCode;

  public banner: Banner;
  public busy: Subscription;
  public form: FormGroup;

  public hasActions: boolean;
  public editorConfig: Record<string, string>;

  public Role = Role;
  public BannerType = BannerType;
  public BannerLocationCode = BannerLocationCode;

  constructor(
    private fb: FormBuilder,
    private bannerResource: BannerResourceService,
  ) {
    this.hasActions = false;
    this.editorConfig = {
      height: '25rem',
      base_url: '/tinymce',
      suffix: '.min',
      plugins: 'lists advlist',
      toolbar: 'undo redo | bold italic underline | bullist numlist outdent indent | removeformat',
      menubar: 'false'
    };
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

  public get startDate(): FormControl {
    return this.form.get('startDate') as FormControl;
  }

  public get endDate(): FormControl {
    return this.form.get('endDate') as FormControl;
  }

  public onSubmit() {
    if (this.form.valid) {
      this.banner = this.json;
      if (this.banner?.id) {
        this.busy = this.bannerResource.updateBanner(this.banner.id, this.banner)
          .subscribe();
      } else {
        this.busy = this.bannerResource.createBanner(this.banner)
          .subscribe();
      }
    }
  }

  public onUpdate(event: { editor: any }) {
    if (!event.editor) { return; }
    this.banner = this.json;
  }

  ngOnInit(): void {
    this.createFormInstance();
    this.getBanner();
    this.initForm();
  }

  private getBanner(): void {
    this.busy = this.bannerResource.getActiveBannerByLocationCode(this.locationCode)
      .subscribe((banner: Banner) => {
        this.banner = banner;
        if (banner) {
          this.form.patchValue(banner);
        }
      })
  }

  private initForm(): void {
    this.form.valueChanges.subscribe(() => this.banner = this.json);
  }

  private get json(): Banner {
    return {
      id: this.banner?.id,
      adminId: this.banner?.adminId,
      bannerType: this.bannerType.value,
      bannerLocationCode: this.bannerLocationCode.value,
      title: this.title.value,
      content: this.content.value,
      startDate: this.startDate.value,
      endDate: this.endDate.value,
    }
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
          value: BannerType.INFO,
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
      startDate: [
        {
          value: '',
          disabled: false
        },
        [Validators.required]
      ],
      endDate: [
        {
          value: '',
          disabled: false
        },
        [Validators.required]
      ],
    });
  }

}
