import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { debounceTime } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { LocalStorageService } from '@core/services/local-storage.service';
import { EnrolmentStatusFilterEnum, PaperStatusEnum } from '@shared/enums/status-filter.enum';
import { SiteStatusType } from '@lib/enums/site-status.enum';
import { SearchFormStatusType } from '@adjudication/shared/enums/search-form-status-type.enum';

@Component({
  selector: 'app-search-form',
  templateUrl: './search-form.component.html',
  styleUrls: ['./search-form.component.scss']
})
export class SearchFormComponent implements OnInit {
  @Input() public hideStatus: boolean;
  @Input() public statusType: SearchFormStatusType;
  @Input() public localStoragePrefix: string;
  @Output() public search: EventEmitter<string>;
  @Output() public filter: EventEmitter<number>;
  @Output() public refresh: EventEmitter<void>;

  public form: FormGroup;
  public enrolleeStatuses: Config<number>[];
  public siteStatuses: Config<number>[];

  private textSearchKey: string;
  private enrolleeStatusCodeKey: string;
  private siteStatusCodeKey: string;

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private configService: ConfigService,
    private localStorage: LocalStorageService
  ) {
    this.enrolleeStatuses = this.configService.statuses;
    this.siteStatuses = new Array<Config<number>>();

    // MacGyver paper enrollee filter into the status filter
    const linkedPaperStatus = new Config<number>(PaperStatusEnum.LINKED_PAPER_ENROLMENT, 'Claimed Manual (Paper) Enrollees');
    const unlinkedPaperStatus = new Config<number>(PaperStatusEnum.UNLINKED_PAPER_ENROLMENT, 'Unclaimed Manual (Paper) Enrollees');
    const renewalEnrolmentStatus = new Config<number>(EnrolmentStatusFilterEnum.RENEWED_ENROLMENT, 'Previous Manual Review');
    this.enrolleeStatuses.push(linkedPaperStatus, unlinkedPaperStatus, renewalEnrolmentStatus);

    const inReviewStatus = new Config<number>(SiteStatusType.IN_REVIEW, 'In Review');
    const editableStatus = new Config<number>(SiteStatusType.EDITABLE, 'Editable');
    const lockedStatus = new Config<number>(SiteStatusType.LOCKED, 'Locked');
    this.siteStatuses.push(inReviewStatus, editableStatus, lockedStatus);

    this.search = new EventEmitter<string>();
    this.filter = new EventEmitter<number>();
    this.refresh = new EventEmitter<void>();
  }

  public get textSearch(): FormControl {
    return this.form.get('textSearch') as FormControl;
  }

  public get enrolleeStatusCode(): FormControl {
    return this.form.get('enrolleeStatusCode') as FormControl;
  }

  public get siteStatusCode(): FormControl {
    return this.form.get('siteStatusCode') as FormControl;
  }

  public get useSiteStatuses(): boolean {
    return this.statusType === SearchFormStatusType.SiteStatuses;
  }

  public get useEnrolleeStatuses(): boolean {
    return this.statusType === SearchFormStatusType.EnrolleeStatuses;
  }

  public onRefresh() {
    this.refresh.emit();
  }

  public ngOnInit() {
    this.textSearchKey = `${this.localStoragePrefix}-search-form-textSearch`;
    this.enrolleeStatusCodeKey = `${this.localStoragePrefix}-search-form-enrollee-statusCode`;
    this.siteStatusCodeKey = `${this.localStoragePrefix}-search-form-site-statusCode`;

    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.fb.group({
      textSearch: [null, []],
      enrolleeStatusCode: [{ value: '', disabled: this.hideStatus }, []],
      siteStatusCode: [{ value: '', disabled: this.hideStatus }, []],
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

    this.enrolleeStatusCode.valueChanges
      .pipe(debounceTime(500))
      // Passing `null` removes the query parameter from the URL
      .subscribe((enrolmentStatus: number) => {
        this.localStorage.set(this.enrolleeStatusCodeKey, enrolmentStatus?.toString());
        this.filter.emit(enrolmentStatus || null);
      });

    this.siteStatusCode.valueChanges
      .pipe(debounceTime(500))
      // Passing `null` removes the query parameter from the URL
      .subscribe((siteStatus: number) => {
        this.localStorage.set(this.siteStatusCodeKey, siteStatus?.toString());
        this.filter.emit(siteStatus || null);
      });

    if (this.statusType === SearchFormStatusType.SiteStatuses) {
      this.form.patchValue({
        textSearch: this.localStorage.get(this.textSearchKey),
        siteStatusCode: this.localStorage.getInteger(this.siteStatusCodeKey) || null,
      });
    } else if (this.statusType === SearchFormStatusType.EnrolleeStatuses) {
      this.form.patchValue({
        textSearch: this.localStorage.get(this.textSearchKey),
        enrolleeStatusCode: this.localStorage.getInteger(this.enrolleeStatusCodeKey) || null
      });
    } else {
      this.form.patchValue({
        textSearch: this.localStorage.get(this.textSearchKey)
      });
    }
  }
}
