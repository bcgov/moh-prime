import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { debounceTime } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { LocalStorageService } from '@core/services/local-storage.service';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { CareSettingEnum } from '@shared/enums/care-setting.enum';

@Component({
  selector: 'app-search-ha-form',
  templateUrl: './search-ha-form.component.html',
  styleUrls: ['./search-ha-form.component.scss']
})
export class SearchHAFormComponent implements OnInit {

  @Output() public search: EventEmitter<string>;
  @Output() public siteStatus: EventEmitter<number>;
  @Output() public vendor: EventEmitter<number>;
  @Output() public careType: EventEmitter<number>;
  @Output() public assignToMe: EventEmitter<boolean>;
  @Output() public refresh: EventEmitter<void>;


  public form: FormGroup;

  public siteStatuses: Config<number>[];
  public vendors: Config<number>[];
  public careTypes: Config<number>[];

  private textSearchKey: string = "ha-search-form-textSearch";
  private siteStatusCodeKey: string = "ha-search-form-siteStatusCode";
  private vendorCodeKey: string = "ha-search-form-vendorCode";
  private careTypeCodeKey: string = "ha-search-form-careTypeCode";
  private assignToMeCodeKey: string = "ha-search-form-assignToMeCode";

  constructor(
    private fb: FormBuilder,
    private configService: ConfigService,
    private localStorage: LocalStorageService
  ) {
    this.siteStatuses = new Array<Config<number>>();

    const inReviewStatus = new Config<number>(SiteStatusType.IN_REVIEW, 'In Review');
    const editableStatus = new Config<number>(SiteStatusType.EDITABLE, 'Editable');
    const lockedStatus = new Config<number>(SiteStatusType.LOCKED, 'Locked');

    this.siteStatuses.push(inReviewStatus, editableStatus, lockedStatus);

    this.careTypes = this.configService.careTypes;
    this.vendors = this.configService.vendors
      .filter(v => v.careSettingCode === CareSettingEnum.HEALTH_AUTHORITY)
      .sort((a, b) => a.name.localeCompare(b.name));

    this.search = new EventEmitter<string>();
    this.siteStatus = new EventEmitter<number>();
    this.vendor = new EventEmitter<number>();
    this.careType = new EventEmitter<number>();
    this.assignToMe = new EventEmitter<boolean>();
    this.refresh = new EventEmitter<void>();
  }

  public get textSearch(): FormControl {
    return this.form.get('textSearch') as FormControl;
  }

  public get siteStatusCode(): FormControl {
    return this.form.get('siteStatusCode') as FormControl;
  }

  public get vendorCode(): FormControl {
    return this.form.get('vendorCode') as FormControl;
  }

  public get careTypeCode(): FormControl {
    return this.form.get('careTypeCode') as FormControl;
  }

  public get assignToMeCkbx(): FormControl {
    return this.form.get('assignToMe') as FormControl;
  }

  public onRefresh() {
    this.refresh.emit();
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.fb.group({
      textSearch: [null, []],
      assignToMe: [{ value: false }, []],
      siteStatusCode: [{ value: '' }, []],
      vendorCode: [{ value: '' }, []],
      careTypeCode: [{ value: '' }, []],
    });
  }

  private initForm() {

    this.textSearch.valueChanges
      .pipe(debounceTime(500))
      // Passing `null` removes the query parameter from the URL
      .subscribe((search: string) => {
        this.localStorage.set(this.textSearchKey, search || '');
        this.search.emit(search || null);
      });

    this.siteStatusCode.valueChanges
      .pipe(debounceTime(500))
      // Passing `null` removes the query parameter from the URL
      .subscribe((siteStatus: number) => {
        this.localStorage.set(this.siteStatusCodeKey, siteStatus?.toString());
        this.siteStatus.emit(siteStatus || null);
      });

    this.vendorCode.valueChanges
      .pipe(debounceTime(500))
      // Passing `null` removes the query parameter from the URL
      .subscribe((vendor: number) => {
        this.localStorage.set(this.vendorCodeKey, vendor?.toString());
        this.vendor.emit(vendor || null);
      });

    this.careTypeCode.valueChanges
      .pipe(debounceTime(500))
      // Passing `null` removes the query parameter from the URL
      .subscribe((careType: number) => {
        this.localStorage.set(this.careTypeCodeKey, careType?.toString());
        this.careType.emit(careType || null);
      });

    this.assignToMeCkbx.valueChanges
      .pipe(debounceTime(500))
      // Passing `null` removes the query parameter from the URL
      .subscribe((assignToMe: boolean) => {
        this.localStorage.set(this.assignToMeCodeKey, assignToMe.toString());
        this.assignToMe.emit(assignToMe);
      });

    this.form.patchValue({
      textSearch: this.localStorage.get(this.textSearchKey),
      siteStatusCode: this.localStorage.getInteger(this.siteStatusCodeKey) || null,
      vendorCode: this.localStorage.getInteger(this.vendorCodeKey) || null,
      careTypeCode: this.localStorage.getInteger(this.careTypeCodeKey) || null,
      assignToMe: Boolean(JSON.parse(this.localStorage.get(this.assignToMeCodeKey) || "false")),
    });
  }
}
