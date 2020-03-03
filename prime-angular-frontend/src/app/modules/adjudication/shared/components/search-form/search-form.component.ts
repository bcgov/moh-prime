import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, FormBuilder } from '@angular/forms';
import { debounceTime, map } from 'rxjs/operators';
import { Config } from '@config/config.model';
import { ConfigService } from '@config/config.service';
import { EnrolmentStatus } from '@shared/enums/enrolment-status.enum';

@Component({
  selector: 'app-search-form',
  templateUrl: './search-form.component.html',
  styleUrls: ['./search-form.component.scss']
})
export class SearchFormComponent implements OnInit {
  public form: FormGroup;
  public statuses: Config<number>[];
  @Output() public search: EventEmitter<string>;
  @Output() public filter: EventEmitter<EnrolmentStatus>;

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

  ngOnInit() {
    this.createFormInstance();
    this.initForm();
  }

  protected createFormInstance() {
    this.form = this.fb.group({
      textSearch: [null, []],
      statusCode: ['', []],
    });
  }

  protected initForm() {
    this.textSearch.valueChanges.pipe(
      debounceTime(500),
    ).subscribe((t: string) => this.search.emit(t));

    this.statusCode.valueChanges.pipe(
      debounceTime(500),
    ).subscribe((es: EnrolmentStatus) => this.filter.emit(es));
  }

}
