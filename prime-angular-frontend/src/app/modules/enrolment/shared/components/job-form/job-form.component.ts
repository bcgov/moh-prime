import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

import { map, startWith, switchMap, mergeMap, combineAll, scan, pairwise } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { Job } from '@enrolment/shared/models/job.model';
import { BehaviorSubject, merge, Observable, combineLatest, of } from 'rxjs';

@Component({
  selector: 'app-job-form',
  templateUrl: './job-form.component.html',
  styleUrls: ['./job-form.component.scss']
})
export class JobFormComponent implements OnInit {
  @Input() public form: FormGroup;
  @Input() public index: number;
  @Input() public total: number;
  @Input() public jobNames: BehaviorSubject<Config<number>[]>;
  @Output() public remove: EventEmitter<number>;

  public defaultJobOptionLabel: string;
  public filteredJobNames: Observable<Config<number>[]>;

  constructor() {
    this.remove = new EventEmitter<number>();
    this.defaultJobOptionLabel = 'None';
  }

  public get title(): FormControl {
    return this.form.get('title') as FormControl;
  }

  public removeJob(index: number) {
    this.remove.emit(index);
  }

  public ngOnInit() {
    this.filteredJobNames = combineLatest(
      this.jobNames // Initial jobs passed through bindings
        .asObservable() // Prevent accidentally affecting parent observable
        .pipe(switchMap((jobNames: Config<number>[]) => {
          const copy = [...jobNames];
          // Add the default option  when it doesn't exist
          // to the list so it can be filtered out
          if (!copy.some((jobName: Config<number>) => jobName.name === this.defaultJobOptionLabel)) {
            // Default option code is not used since it's a free-form field
            copy.unshift(new Config<number>(null, this.defaultJobOptionLabel));
          }
          return of(copy);
        })),
      this.form.valueChanges.pipe(
        startWith(''), // Trigger emission immediately!
      )
    ).pipe(
      map(([availableJobNames, currentJob]: [Config<number>[], Job]) => this.filterJobNames(availableJobNames, currentJob))
    );
  }

  /**
   * @description
   * Auto-complete filtering of the available jobs.
   *
   * @param jobNames to be filtered
   * @param currentJob predicate for filtering
   */
  private filterJobNames(jobNames: Config<number>[], currentJob: Job): Config<number>[] {
    // Default provide the entire list of jobs
    let filteredJobNames = jobNames;
    const currentJobTitle = (currentJob) ? currentJob.title.toLowerCase().trim() : null;

    if (jobNames.length && currentJobTitle) {
      // Apply auto-complete filtering
      filteredJobNames = filteredJobNames
        .filter((jobName: Config<number>) => jobName.name.toLowerCase().includes(currentJobTitle));
    }

    return filteredJobNames;
  }
}
