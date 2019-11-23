import { Component, OnInit, Input, Output, EventEmitter, SimpleChanges, OnChanges } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { map, startWith } from 'rxjs/operators';
import { Observable } from 'rxjs';

import { Config } from '@config/config.model';
import { Job } from '@enrolment/shared/models/job.model';

@Component({
  selector: 'app-job-form',
  templateUrl: './job-form.component.html',
  styleUrls: ['./job-form.component.scss']
})
export class JobFormComponent implements OnInit {
  @Input() public form: FormGroup;
  @Input() public index: number;
  @Input() public total: number;
  @Input() public jobNames: Config<number>[];
  @Output() public remove: EventEmitter<number>;

  public filteredJobNames: Config<number>[];

  constructor() {
    this.remove = new EventEmitter<number>();
  }

  public removeJob(index: number) {
    this.remove.emit(index);
  }

  public ngOnInit() {
    this.form.valueChanges
      .pipe(
        startWith(''),
        map((value: Job) => this.filterJobNames(value.title || ''))
      )
      .subscribe((filteredJobNames: Config<number>[]) => this.filteredJobNames = filteredJobNames);
  }

  private filterJobNames(value: string): Config<number>[] {
    const filterValue = value.toLowerCase();
    return this.jobNames.filter((jobName: Config<number>) => jobName.name.toLowerCase().includes(filterValue));
  }
}
