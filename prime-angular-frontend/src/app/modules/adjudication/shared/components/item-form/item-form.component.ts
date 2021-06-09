import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

import { BehaviorSubject, Observable, combineLatest, of } from 'rxjs';
import { map, startWith, switchMap } from 'rxjs/operators';

import { Config } from '@config/config.model';
import { Job } from '@enrolment/shared/models/job.model';

@Component({
  selector: 'app-item-form',
  templateUrl: './item-form.component.html',
  styleUrls: ['./item-form.component.scss']
})
export class ItemFormComponent implements OnInit {
  @Input() public form: FormGroup;
  @Input() public index: number;
  @Input() public total: number;
  @Input() public options: BehaviorSubject<Config<number>[]>;
  @Input() public inputLabel: string;
  @Input() public allowDefaultOption: boolean;
  @Input() public defaultOptionLabel: string;
  @Output() public remove: EventEmitter<number>;

  public allowRemoveNone: boolean;
  public filteredJobNames: Observable<Config<number>[]>;

  constructor() {
    this.remove = new EventEmitter<number>();
    this.defaultOptionLabel = '';
    this.allowRemoveNone = true;
  }

  public get title(): FormControl {
    return this.form.get('title') as FormControl;
  }

  public removeJob(index: number) {
    this.remove.emit(index);
  }

  public ngOnInit() {
    this.filteredJobNames = this.initAutoComplete();
  }

  public removeNone(input: HTMLFormElement) {
    if (this.allowRemoveNone && input.value === this.defaultOptionLabel) {
      input.value = '';
      this.allowRemoveNone = false;
    }
  }

  private initAutoComplete() {
    return combineLatest([
      this.options // Initial jobs passed through bindings
        .asObservable() // Prevent accidentally affecting parent observable
        .pipe(switchMap((jobNames: Config<number>[]) => {
          const copy = [...jobNames]; // Prevent changes by reference
          // Add the default option when it doesn't exist
          // to the list so it can be filtered out
          if (
            this.allowDefaultOption &&
            !copy.some((jobName: Config<number>) => jobName.name === this.defaultOptionLabel)
          ) {
            // Default option code is not used since it's a free-form field
            copy.unshift(new Config<number>(null, this.defaultOptionLabel));
          }
          return of(copy);
        })),
      this.form.valueChanges.pipe(
        startWith(''), // Trigger emission immediately!
      )
    ]).pipe(
      map(([availableJobNames, currentJob]: [Config<number>[], Job]) => this.filterOptions(availableJobNames, currentJob))
    );
  }

  /**
   * @description
   * Auto-complete filtering of the available options.
   *
   * @param availableOptions to be filtered
   * @param currentOption predicate for filtering
   */
  private filterOptions(availableOptions: Config<number>[], currentOption: Job): Config<number>[] {
    // Default provide the entire list of items
    let filteredOptions = availableOptions;
    const currentJobTitle = (currentOption) ? currentOption.title.toLowerCase().trim() : '';

    // Apply auto-complete filtering
    if (availableOptions.length && currentJobTitle && currentJobTitle !== this.defaultOptionLabel.toLocaleLowerCase()) {
      filteredOptions = filteredOptions
        .filter((jobName: Config<number>) => jobName.name.toLowerCase().includes(currentJobTitle));
    }

    return filteredOptions;
  }
}
