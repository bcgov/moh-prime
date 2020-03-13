import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';

import { debounceTime } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';

@Component({
  selector: 'app-search-form',
  templateUrl: './search-form.component.html',
  styleUrls: ['./search-form.component.scss']
})
export class SearchFormComponent implements OnInit {
  @Output() public search: EventEmitter<string>;
  @Output() public filter: EventEmitter<EnrolmentStatus>;

  public form: FormGroup;
  public statuses: Config<number>[];

  constructor(
    private fb: FormBuilder,
    private configService: ConfigService,
  ) {
    this.statuses = this.configService.statuses;
    this.search = new EventEmitter<string>();
    this.filter = new EventEmitter<EnrolmentStatus>();
  }

  public get textSearch(): FormControl {
    return this.form.get('textSearch') as FormControl;
  }

  public get statusCode(): FormControl {
    return this.form.get('statusCode') as FormControl;
  }

  public ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  private createFormInstance() {
    this.form = this.fb.group({
      textSearch: [null, []],
      statusCode: ['', []],
    });
  }

  private initForm() {
    this.textSearch.valueChanges
      .pipe(debounceTime(500))
      // Passing `null` removes the query parameter from the URL
      .subscribe((search: string) => this.search.emit(search || null));

    this.statusCode.valueChanges
      .pipe(debounceTime(500))
      // Passing `null` removes the query parameter from the URL
      .subscribe((enrolmentStatus: EnrolmentStatus) => this.filter.emit(enrolmentStatus || null));
  }
}
