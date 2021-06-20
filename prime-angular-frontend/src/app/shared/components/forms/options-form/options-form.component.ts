import { Component, OnInit, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { BehaviorSubject, Observable, combineLatest } from 'rxjs';
import { map, startWith } from 'rxjs/operators';

import { UntilDestroy, untilDestroyed } from '@ngneat/until-destroy';

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
   * Form field label.
   */
  @Input() public fieldLabel: string;
  /**
   * @description
   * Form field hint.
   */
  @Input() public fieldHint: string;
  /**
   * @description
   * FormGroup control name.
   */
  @Input() public controlName: string;
  /**
   * @description
   * Key for accessing the option display value.
   *
   * NOTE: Only used with autocomplete, and if omitted the
   * value is considered to be a primitive data type.
   */
  @Input() public optionLabel: string;
  /**
   * @description
   * Set of available select options.
   */
  @Input() public availableOptions: BehaviorSubject<any[]>;
  /**
   * @description
   * Whether to use select (default) or autocomplete.
   */
  @Input() public selectOrAutocomplete: 'select' | 'autocomplete';
  /**
   * @description
   * List of options that includes the selected option.
   */
  public filteredOptions: Observable<any[]>;

  constructor() {
    this.selectOrAutocomplete = 'select';
  }

  /**
   * @description
   * Show the appropriate selected value in the autocomplete
   * input field.
   */
  public displayWith(option: any) {
    if (!option) {
      return '';
    }

    return (this.optionLabel && option[this.optionLabel])
      ? option[this.optionLabel]
      : option;
  }

  public ngOnInit() {
    this.filteredOptions = this.valueChanges();
  }

  /**
   * @description
   * Listen to changes in available options and the form field, and
   * provide the resulting set of options.
   */
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

        if (this.selectOrAutocomplete === 'select') {
          // Add in the currently selected option that would be filtered
          // out of the available options so it can be displayed
          return (currentOption[this.controlName])
            ? [currentOption[this.controlName], ...availableOptions]
            : availableOptions;
        }

        // Otherwise, autocomplete will already have the value
        // filtered out of the available options
        return availableOptions;
      })
    );
  }
}
