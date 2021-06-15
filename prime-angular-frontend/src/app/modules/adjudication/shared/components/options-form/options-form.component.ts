import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { BehaviorSubject, Observable, combineLatest, of } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

import { VendorConfig } from '@config/config.model';

@UntilDestroy()
@Component({
  selector: 'app-options-form',
  templateUrl: './options-form.component.html',
  styleUrls: ['./options-form.component.scss']
})
export class OptionsFormComponent implements OnInit {
  /**
   * @description
   * Instance of form.
   */
  @Input() public form: FormGroup;
  /**
   * @description
   * Index of the current FormGroup within the FormArray.
   */
  @Input() public index: number;
  /**
   * @description
   * Form input label.
   */
  @Input() public selectLabel: string;
  /**
   * @description
   * FormGroup control name.
   */
  @Input() public controlName: string;
  /**
   * @description
   * Set of available select options.
   */
  @Input() public availableOptions: BehaviorSubject<any[]>;
  /**
   * @description
   * Key for accessing the option display value.
   */
  @Input() public optionLabel: string;
  /**
   * @description
   * List of options that includes the selected option.
   */
  public filteredOptions: Observable<any[]>;

  constructor() {}

  public ngOnInit() {
    this.filteredOptions = this.valueChanges();
  }

  private valueChanges(): Observable<any[]> {
    return combineLatest([
      // Prevent accidentally affecting parent observable
      this.availableOptions.asObservable(),
      // Trigger emission immediately!
      this.form.valueChanges.pipe(startWith((this.form.value ?? null) as object))
    ]).pipe(
      untilDestroyed(this),
      map(([availableOptions, currentOption]: [any[], any | null]) => {
        availableOptions = (Array.isArray(availableOptions))
          ? availableOptions
          : [];
        // Add in the currently selected option that would be
        // filtered out of the available options
        return (currentOption[this.controlName])
          ? [currentOption[this.controlName], ...availableOptions]
          : availableOptions;
      })
    );
  }
}
