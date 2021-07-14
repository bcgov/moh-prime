import { Component, OnInit, Output, EventEmitter, Input } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';

import { debounceTime } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { EnrolmentStatusEnum } from '@shared/enums/enrolment-status.enum';

@Component({
  selector: 'app-search-form',
  templateUrl: './search-form.component.html',
  styleUrls: ['./search-form.component.scss']
})
export class SearchFormComponent implements OnInit {
  @Input() public hideStatus: boolean;
  @Output() public search: EventEmitter<string>;
  @Output() public filter: EventEmitter<EnrolmentStatusEnum>;
  @Output() public refresh: EventEmitter<void>;

  public form: FormGroup;
  public statuses: Config<number>[];

  constructor(
    private route: ActivatedRoute,
    private fb: FormBuilder,
    private configService: ConfigService,
  ) {
    this.statuses = this.configService.statuses;
    this.search = new EventEmitter<string>();
    this.filter = new EventEmitter<EnrolmentStatusEnum>();
    this.refresh = new EventEmitter<void>();
  }

  public get textSearch(): FormControl {
    return this.form.get('textSearch') as FormControl;
  }

  public get statusCode(): FormControl {
    return this.form.get('statusCode') as FormControl;
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
      statusCode: [{ value: '', disabled: this.hideStatus }, []]
    });
  }

  private initForm() {
    const queryParams = this.route.snapshot.queryParams;
    this.form.patchValue(queryParams);

    this.textSearch.valueChanges
      .pipe(debounceTime(500))
      // Passing `null` removes the query parameter from the URL
      .subscribe((search: string) => this.search.emit(search || null));

    this.statusCode.valueChanges
      .pipe(debounceTime(500))
      // Passing `null` removes the query parameter from the URL
      .subscribe((enrolmentStatus: EnrolmentStatusEnum) => this.filter.emit(enrolmentStatus || null));
  }
}
